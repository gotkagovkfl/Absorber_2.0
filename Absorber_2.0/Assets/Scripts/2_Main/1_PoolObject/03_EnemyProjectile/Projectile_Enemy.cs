using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//=======================부모클래스===================================
// 공격시 발생하며, 적에게 피해를 입힐 수 있는 투사체나 공격효과 등에 관한 클래스 - 자식 클래스의 이름은 "Proj_(이름)"으로 작명 
//  ex )  총알, 광선, 검흔 등 
//===================================================================
public abstract class Projectile_Enemy : MonoBehaviour , IPoolObject
{
    public string id_proj;
    
    
    // 필요 변수
    protected Rigidbody2D rb;      // 물리 조정을 위한 리지드바디

    public Transform myTransform;

    public enum ProjDir { left, right, up };
    public ProjDir proDir;
    
    // 필수 능력치
    public int damage;               // 공격력 ( 무기에서 받아오자 )
    public float speed;                // 탄속
    public float scale;                 // 크기 (공격범위)
    public int projNum;                  //투사체수
    public int penetration;            // 관통 횟수    (-1이면 무한)
    
    public float lifeTime;               // 탄환 지속시간

    // ---
    public bool strongAttack = false;   // 강한공격 : 플레이어를 넉백시킴 


    // 선택 능력치
    public Vector3 originalScale;
    public Transform caster;
    public Transform target;             // 적의 위치 
    public Vector3 direction;            // 발사방향 

    //분열 관련 
    public GameObject prefab_parent;
    public GameObject prefab_proj;

    //public string id_enemyProj;

    public bool isAlive;
    public bool active;

    //======sound============
    public AudioSource audioSource;

    //==============================================================================
    
    //===========================
    // IPoolObject : resourceManager에 로드시에 id 초기화 
    //===========================
    public void InitEssentialInfo()
    {
        InitEssentialInfo_enemyProj();
    }
    //
    protected abstract void InitEssentialInfo_enemyProj();
    //================
    // GetID
    //==============
    public string GetId()
    {
        return id_proj;
    }
    //===========================
    // IPoolObject : 처음 생성될때 
    //===========================
    public void OnCreatedInPool()
    {

    }

    //===========================
    // IPoolObject : 다시 사용될때, 
    //===========================
    public void OnGettingFromPool()
    {

    }
    
    
    //==========================================================
    
    
    void Awake()
    {
        originalScale = transform.localScale;
    }


    // public abstract void InitEssentialProjInfo();

    //public void InitProj(Vector3 firePoint, Transform target)
    //{
    //    myTtransform.position = firePoint;
    //    SetTarget(target);
    //    SetDirection(target);
    //}

    //public void InitProj_Common()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    myTtransform = transform;
    //}

    //=============================================
    // projectile의 셋업
    // 데미지, 탄속, 크기, 관통, 분열횟수, 수명
    // =============================================
    public void SetUp(int dmg, float spd, float sc, int pn, int sn, float lt)
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource =GetComponent<AudioSource>();
        if (audioSource!=null)
        {
            audioSource.loop = false;
            audioSource.playOnAwake = false;
        }

        
        myTransform = transform;
        

        damage = dmg;
        speed = spd;
        scale = sc;
        penetration = pn;
        //penetration = pen;
        
        lifeTime = lt;

        // lifeTime 이 -1인 경우는 무기가 영구지속
        SetLifeTime();

        transform.localScale =  originalScale * scale;
        rb.simulated = true;
	}
    public void SetUp_special(bool strongAttack)
    {
        this.strongAttack = strongAttack;
    }

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
        
        Vector3 dir = target.position - transform.position;
        this.direction = dir.normalized;
    }

    public void SetDirection(Vector3 dir)
    {
        this.direction =dir.normalized;
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
        transform.rotation = Quaternion.FromToRotation(Vector3.up,direction);
    }
    public void RotateProj()
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }



    //============================================
    // projectile 액션 (모션, 애니메이션)
    //============================================
    public abstract void Action();

    //============================================
    // 투사체 수명 설정 : 셋업된 lifeTime 값에 따라 수명 결정
    //============================================
    public void SetLifeTime()
    {
        // 설
        if (lifeTime != -1)
        {
            StartCoroutine(DestroyProj(lifeTime));
        }   
    }

    //============================================
    // 투사체 사라짐 : 투사체 풀에 반납 
    //============================================    
    public IEnumerator DestroyProj(float time)
    {
        yield return new WaitForSeconds(time);

        StartCoroutine(Shrink());
    }

    public abstract void EnemyProjDestroy_custom();

    //====================
    // 아이템 쪼그라들고 아이템 풀 반납 : 자연스러운 연출을 위함 
    //=====================
    IEnumerator Shrink()
    {
        rb.simulated = false;
        if (!(id_proj.Equals("210") || id_proj.Equals("211") || id_proj.Equals("212") || id_proj.Equals("213") ||
            id_proj.Equals("220") || id_proj.Equals("221") || id_proj.Equals("222") || id_proj.Equals("223")))
        {
            for (int i = 9; i > 0; i--)
            {
                myTransform.localScale = originalScale * 0.1f * i;
                yield return new WaitForSeconds(0.05f);
            }
        }
        /*for (int i=9;i>0;i--)
        {
            myTransform.localScale = originalScale * 0.1f * i;
            yield return new WaitForSeconds(0.05f);
        }*/
        EnemyProjPoolManager.instance.TakeToPool(this);         // 풀링 - 중복 반납이 일어나면 에러발생
        EnemyProjDestroy_custom();
    }




    //==============================================
    // 충돌시 - 적 충돌을 감지 
    //==============================================
    void OnTriggerEnter2D(Collider2D other)
    {
        if(!isAlive || !active)
        {
            return;
        }

        if (other.gameObject.CompareTag("Player") && (id_proj.Equals("201") || id_proj.Equals("220") || id_proj.Equals("221") || id_proj.Equals("222") || id_proj.Equals("223")))
        {

            Vector3 hitPoint = other.ClosestPoint(myTransform.position);     // 타격지점

            Player.player.OnDamage(damage, hitPoint, strongAttack);

            OnHit();        // 히트시 효과

        }else if (other.gameObject.CompareTag("Player") && !(id_proj.Equals("102"))) { 

            Vector3 hitPoint = other.ClosestPoint(myTransform.position);     // 타격지점
            
            Player.player.OnDamage(damage, hitPoint, strongAttack);

            OnHit();        // 히트시 효과

            Penetrate();

        }


        if (other.gameObject.CompareTag("Enemy") && id_proj.Equals("002"))
        {

            float heal_hp = 4f;
            Enemy e = other.GetComponent<Enemy>();
            //EffectPoolManager.epm.CreateText();

            if ( !DirectingManager.dm.onDirecting)
            {
                e.Healing(heal_hp );
            }
            

        }

    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (!isAlive || !active)
        {
            return;
        }
        if(other.gameObject.CompareTag("Player") && id_proj.Equals("102"))
        {
            Vector3 hitPoint = other.ClosestPoint(myTransform.position);     // 타격지점

            Player.player.OnDamage(damage, hitPoint, strongAttack);

            OnHit();
        }
    }


    public virtual void OnHit()
    {

    }




    //=================================y=============
    // 관통처리 
    //==============================================
    void Penetrate()
    {
        if (penetration == 0)    // 관통 후 파괴
        {
            StartCoroutine( DestroyProj( 0f ) );
        }
    }

        
    //    // 관통횟수 감소 후 삭제
    //    if (penetration--<=0)
    //    {
    //        Destroy(gameObject);
    //    }
    //}



}
