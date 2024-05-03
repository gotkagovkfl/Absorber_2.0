using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System;
// using Redcode.Pools;


//=======================부모클래스===================================
// 공격시 발생하며, 적에게 피해를 입힐 수 있는 투사체나 공격효과 등에 관한 클래스 - 자식 클래스의 이름은 "Proj_(이름)"으로 작명 
//  ex )  총알, 광선, 검흔 등 
//===================================================================
public abstract class Projectile : MonoBehaviour , IPoolObject
{
    // 필요 변수
    public SpriteRenderer spriter;
    protected AudioSource audioSource;

    public Transform myTransform;       // 오버헤드 발생감소를 위해 자기 자신의 transform 캐싱
    public Transform mainTransform;     // 첫번째 꽃잎은 사라지지 않으며 그 정보가 분열되는 꽃잎에 전달됨
    public Projectile targetProj;       // 타겟대상이 proj인경우 proj 호출

    public Animator animator;
    public float animationLength;
    public Rigidbody2D rb;      // 물리 조정을 위한 리지드바디
    
    public enum ProjDir {left, right, up};  // 투사체가 향하는 방향
    public ProjDir proDir;

    // 필수 능력치
    public string id_proj;

    public bool shrinkOnDes;

    public Transform followingTarget;
    public Vector3 followingPosOffset;
    public bool isFollowing=false;          // 특정 오브젝트를 따라다니는지 

    public bool splitWhenHit= false;        // 타격시 분열 

    public bool isAlive;
    public bool active;

    public float damage;               // 공격력 ( 무기에서 받아오자 )
    public float weight_critDamage;
    public float speed;                // 탄속
    public float scale;                 // 크기 (공격범위)
    public int projNum;                  //투사체수
    public int penetration;            // 관통 횟수    (-1이면 무한)
    public int splitNum;                 // 남은 분열 횟수
    public int explosionLevel;
    public float lifeTime;               // 탄환 지속시간
    public float knockBackPower;
    
    // 선택 능력치
    public Vector3 hitPoint;                // 타격 지점
    public Transform target_hit;            // 타격 대상 
    public Transform target_unattackable;   // 공격 불가 대상
    
    public Transform target;             // 목표의 위치 
    public Vector3 direction;            // 발사방향 :보통 목표를 향함

    //분열 관련 
    public float rotAngle;               // 회전각도
    public float[] splitAngle;    //분열각도 : 분열 발생시 해당 각도로 투사체가 발사됨
    protected Vector3 splitPoint;         // 분열이 시작될 지점


    //=====================================================================================================================
    //================================ 풀링 관련 ==================================================================================
    public void InitEssentialInfo()
    {
        InitEssentialInfo_proj();
    }

    protected abstract void InitEssentialInfo_proj();
    
    //================
    // GetID
    //==============
    public string GetId()
    {
        return id_proj;
    }
    //============================================
    // 처음 생성될 때 할 일 
    //============================================    
    public void OnCreatedInPool()
    {
        // InitEssentialProjInfo();
    }


    //============================================
    // 풀에서 가져올때 발생할 일 - 근데 초기화작업은 여기서 호출되지 않음
    //============================================    
    public void OnGettingFromPool()
    {
        // InitProj();
    }

    ///==============================================

    //================================
    // 투사체 필수정보 초기화 ( idName 등)
    //================================
    


    //=============================================
    // projectile 초기화 - 능력치 셋업 등 필수적인것만 
    // =============================================    
    public void InitProj(Weapon weapon, Vector3 firePoint, Transform target)
    {
        SetUp(weapon);
        myTransform.position = firePoint;
        SetTarget(target);
        SetDirection(target);
    }

    // 공통 초기화  - 여기서 한번에 하도록 
    public void InitProj_Common()
    {
        animator = GetComponent<Animator>();
        rb =GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.loop = false;
            audioSource.playOnAwake = false;
        }
        
        myTransform = transform;
    }


    //=============================================
    // projectile의 셋업
    // 데미지, 탄속, 크기, 관통, 분열횟수, 수명
    // =============================================
    public void SetUp(Weapon weapon)
    {             
        InitProj_Common();
        damage = weapon.damageT;
        speed= weapon.projSpeedT;
        scale = weapon.scaleT;
        projNum = weapon.projNumT;
        penetration = weapon.penetrationT;
        splitNum = weapon.splitNumT;
        lifeTime = weapon.projLifeTimeT;

        explosionLevel = weapon.explosionLevelT;
        //
        weight_critDamage = weapon.weight_critDamage;

        knockBackPower = weapon.knockBackPowerT;
    }
    
    // =============================================
    // SetUp의 오버로드 - 분열 등 투사체에서 투사체를 생성할 때 호출됨. 
    // =============================================
    public void SetUp(float dmg, float spd, float sc, int pn, int pt, int sn, float lf)
    {        
        InitProj_Common();
        damage = dmg;
        speed= spd;
        scale = sc;
        projNum = pn;
        penetration = pt;
        splitNum = sn;
        lifeTime = lf;        
    }

    //==========================================
    // 특수능력치 설정 - (explosion level, )
    //==========================================
    public void SetSpecialStat(int el, float wc, float np)
    {
        explosionLevel = el;
        weight_critDamage = wc;
        knockBackPower = np;
    }

    //============================================
    // projectile 액션 (모션, 애니메이션, 수명 설정 등 )
    //============================================
    public void Action()
    {
        rb.simulated = true;
        // 투사체 크기는 플레이어 크기에 맞춰 설정됨  - 기본 투사체 정보는 instance의 dic_proj으로부터 가져옴
        transform.localScale = Player.player.t_player.localScale.x * PrefabManager.GetProj(id_proj).transform.lossyScale * scale; 
        
        Action_custom();            // 여기서 애니메이션 등 기타 이유로 수명을 뒤늦게 설정할 수 있기 때문에, 투사체 삭제를 뒤늦게 호출
        SetLifeTime();
    }

    //============================================
    // projectile 액션 (모션, 애니메이션)
    //============================================
    public abstract void Action_custom();


    //============================================
    // 투사체 수명 설정 : 셋업된 lifeTime 값에 따라 수명 결정
    //============================================
    public void SetLifeTime()
    {
        // 설
        if (lifeTime != -1)
        {
            // Invoke("ProjDestroy", lifeTime);
            ProjDestroy(lifeTime);
        }
    }

    //============================================
    // 투사체 사라짐 : 투사체 풀에 반납 
    //============================================    
    public void ProjDestroy(float time)
    {
        // ProjPoolManager.instance.TakeToPool(this);         // 풀링 - 중복 반납이 일어나면 에러발생
        StartCoroutine( ProjDestroy_c(time) );
    }

    public IEnumerator ProjDestroy_c(float time)
    {
        yield return new WaitForSeconds(time);
        rb.simulated = false;
        yield return StartCoroutine(Shrink());
        ProjPoolManager.instance.TakeToPool(this); 
        ProjDestroy_custom(); 
    }

    public abstract void ProjDestroy_custom();


    IEnumerator Shrink()
    {
        if (shrinkOnDes)
        {
            for (int i=0;i<20;i++)
            {
                myTransform.localScale *= 0.95f;
                yield return null;
            }  
        }

    }

//=======================================================================================================================


    //============================================
    // 타겟 세팅 - 
    //============================================
    public void SetTarget(Transform target)
    {
        if (target == null)
        {
            return;
        }

        this.target = target;
    }
    
    //============================================
    // 발사 방향 세팅 - 2종류 있음 : 타겟을 주고 방향을 계산, 방향을 직접 주기
    //============================================
    public void SetDirection(Transform target)      
    {
        if (target == null)
        {
            return;
        }
        // vector3.up은 스프라이트 피봇이 발바닥에 붙어있엇서 중심을 몸통 중앙으로 맞추기 위함 
        Vector3 dir = target.position - myTransform.position;
        direction = dir.normalized;
    }

    public void SetDirection(Vector3 dir)
    {
        direction =dir.normalized;
    }

    //============================================
    // 투사체 회전시키기    : 현재 방향에서 주어진 각도만큼회전
    //============================================
    public void RotateProj(float angle) // 회전각이 직접 주어짐 
    {
        transform.Rotate(new Vector3(0,0,1) * angle);
    }

    public void RotateProj(ProjDir dir)    // 방향이 주어짐 
    {
        if (direction == null)
        {
            return;
        }
        //        
        if (dir == ProjDir.left)
        {
            myTransform.rotation = Quaternion.FromToRotation(Vector3.left,direction);
        }
        else if (dir == ProjDir.up)
        {
            myTransform.rotation = Quaternion.FromToRotation(Vector3.up,direction);
        }
        else if (dir == ProjDir.right)
        {
            myTransform.rotation = Quaternion.FromToRotation(Vector3.right,direction);
        }
    }

    //============================================
    // 분열 각도 배열 세팅
    //============================================
    public void SetSplitAngles(float a0, float a1, float a2,float a3,float a4,float a5,float a6)
    {
        splitAngle = new float[7];
        splitAngle[0] = a0;
        splitAngle[1] = a1;
        splitAngle[2] = a2;
        splitAngle[3] = a3;
        splitAngle[4] = a4;
        splitAngle[5] = a5;
        splitAngle[6] = a6;
    }


    //===============================================================
    public void SetFollowingTarget(Transform t, Vector3 offset)
    {
        followingTarget = t;
        followingPosOffset = offset;
        isFollowing = true;
    }
    
    public void FollowTarget()
    {
        StartCoroutine(StartFollow());
    }

    // 투사체가 타겟을 따라다님
    public IEnumerator StartFollow()
    {
        var wf = new WaitForFixedUpdate();  // 캐싱

        while(isFollowing)
        {
            transform.position = followingTarget.position + followingPosOffset;
            yield return wf;
        }
    }


//================================================================================================================

    //==============================================
    // 충돌시 - 적 충돌을 감지 
    //==============================================
    void OnTriggerEnter2D(Collider2D other)
    {
        // 활성상태가 아니라면 충돌처리안함
        if (!isAlive || !active)
        {
            return;
        }
        
        // 
        if (other.CompareTag("Enemy") )
        {
            hitPoint = other.ClosestPoint(myTransform.position);     // 타격지점
            target_hit = other.transform;  


            // 공격할 수 없는 적과 충돌했다면 (분열 전용)
            if (target_hit == target_unattackable)
            {
                target_unattackable = null;   // 다음 충돌에는 타격가능하게
                return;
            }
            
            // 
            float weight = 1;
            float weight_lvl = 0;
                    
            if (  Random.Range(0, 100) <= Player.player.crit_prob)
            {
                weight *= weight_critDamage;
                weight_lvl++;
            }
     
            if(  Random.Range(0, 100) <= Player.player.reinforce_prob)
            {
                weight *= 2;
                weight_lvl++;
            }


            int finalDamage = (int )(damage * weight);

            Color color = new Color( 1.0f , 1.0f * (1 - weight_lvl*0.25f) , (weight_lvl==0)?1.0f:0.5f , 1.0f );             //255,255,255 / 255, 255, 0 / 255, 200, 0
            int tn = weight_lvl>0?3:0;

            // EffectPoolManager.instance.CreateText(hitPoint, finalDamage.ToString(), color,  tn);
            // EffectPoolManager.instance.CreateHitEffect(hitPoint);       // 총알 위치에 이펙트 생성 


            //
            Enemy e =  other.GetComponent<Enemy>();
            e.Damaged(finalDamage, hitPoint, knockBackPower);


            // 타격시 효과 
            //OnHit(other, finalDamage, hitPoint); - 필요이유 까먹음. 


            //
            // 특수효과 : 폭발
            Explode(hitPoint);

            // 특수효과 : 타격시 분열
            if (splitWhenHit)
            {
                Split();
            }

            //
            Penetrate();
        }
    }

    //================
    // on hit effect 
    //=================
    public virtual void OnHit(Collider2D other, float dmg, Vector3 hitPoint)
    {

    }



    //==============================================
    // 관통처리  ******************************************* 수정해야할거 같음 
    //==============================================
    void Penetrate()
    {
        // 관통횟수 감소 후 삭제
        if (penetration--<=0)
        {
            ProjDestroy(0);
        }
    }


    //
    //                  특수효과 : 엘리트, 보스 등을 잡아 나타나는 특수효과 
    //=================================================================================================================

    //============================================
    // 분열 : 투사체 (효과)가 분열한다. 
    //============================================
    public abstract void Split();


    //============================================
    // 폭발 : 충돌시 일정 확률로 폭발을 일으킨다. 
    //============================================
    public void Explode(Vector3 pos)
    {
        int num = Random.Range(0,100);
        if (num < explosionLevel*25)
        {
            Projectile proj = ProjPoolManager.instance.GetFromPool("100");        
            
            float dmg = 7 + Player.player.atk;

            proj.SetUp( dmg, 0,  scale,  0, 987654321, 0 , 0.25f);           
            proj.myTransform.position = pos;
            SetSpecialStat( 0, 1.5f, 6 ); 
            proj.Action();
        }
    }
}
