using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//===================== 부모의 부모 클래스 ========================
// 무기 클래스의 최상단에 위치하는 클래스 : 필수적인 무기 정보를 관리  (이름, 데미지 등 )
//===============================================================
public abstract class Weapon : MonoBehaviour , IPoolObject
{
    //====================================================
    // 변수
    //====================================================
    public Animator animator;
    public float animationLength;
    
    public SpriteRenderer spriter;
    public Transform handTransform;
    public Vector3 originalHandPosition;

    public Transform mouseTarget;

    
    // 기본 능력치
    public string weaponName = "";         // 무기 이름
    public string id_weapon;
    public bool isSingleTarget = false;
    public bool isRotatable = false;

    public bool targetOnLeft= false;
    // public bool isRotatable  = false;       // 무기가 타겟쪽으로 회전하는지 여부  - 싱글타겟이면 회전함.
    // public bool isTargetingWeapon;        // 타겟팅 무기 여부 

    //========================
    float damage;        // 공격력
    float range;     // 사거리
    float attackSpeed;   // 공격속도(초당 공격속도)
    float asMagnification;     // 
    //
    float scale = 1f;        //투사체 크기 
    int penetration;     // 관통횟수 
    float projLifeTime;
    float projSpeed = 10;    // 투사체(이펙트) 속도
    float knockBackPower;          // 넉백파워
    int projNum = 1;     // 투사체 수 (기본)
    int splitNum = 0;
    int explosionLevel = 0;
    //========================
    // 계수
    public float weight_damage;
    public float weight_range;
    public float weight_attackSpeed;
    public float weight_critDamage;

    //==========================================================
    // 프로퍼티 - 각 속성들은 플레이어 정보와 합산된다. 무기 특성별로  계수가 붙는다. (롤 스킬 공격력 시스템이라 보면 됨)
    // 주능력치 (계수 영향)
    public float damageT    // 총 무기 공격력
    {
        get
        {               
            return  damage + Player.player.Atk * weight_damage + Player.player.Avoid_Atk;
        }
        set
        {
            damage = value;
        }
    }
    public float rangeT     // 총 무기 사거리 ( 범위 )
    {
        get
        {
            return range * (100 + Player.player.Range_Plus * weight_range ) *0.01f;
        }
        set
        {
            range = value;
        }
    }
    public float attackSpeedT // 총 무기 공격 속도
    {
        get
        {
            return attackSpeed * ( 100 + Player.player.Attack_Speed_Plus* weight_attackSpeed )  * 0.01f;
        }
        set
        {
            attackSpeed = value;
        }
    }
    // 부능력치
    public int penetrationT // 총 무기 관통력
    {
        get
        {
            return penetration + Player.player.penetration;
        }
        set
        {
            penetration = value;
        }
    }
    public float scaleT // 총 투사체 크기 : 범위 영향
    {
        get 
        {
            return scale  *(100 + Player.player.Range_Plus * weight_range ) * 0.01f;
        }
        set
        {
            scale = value;
        }
    }
    public float projSpeedT // 총 투사체 속도
    {
        get
        {
            return projSpeed;
        }
        set
        {
            projSpeed = value;
        }
    }
    public float projLifeTimeT      // 총 투사체 수명 
    {
        get
        {
            return projLifeTime;
        }
        set
        {
            projLifeTime = value;
        }
    }
    public float knockBackPowerT
    {
        get
        {
            return knockBackPower;
        }
        set
        {
            knockBackPower = value;
        }   
    }


    // 특수 능력치
    public int splitNumT    // 분열횟수
    {
        get
        {
            return splitNum + Player.player.splitNum;
        }
        set
        {
            splitNum = value;
        }
    }
    public int projNumT     // 투사체수
    {
        get
        {
            return projNum + Player.player.projNum;
        }
        set
        {
            projNum = value;
        }
    }

    public int explosionLevelT
    {
        get
        {
            return explosionLevel + Player.player.explosionLevel;
        }
        set
        {
            explosionLevel = value;
        }
    }
    //======================================================

    public float lastAttackTime;
    public bool notAvailable = false;

    public bool canAttack       // 공격 가능 상태
    {
        get
        {
            if (notAvailable || DirectingManager.dm.onDirecting || !Player.player.canAttack)
            {
                return false;
            }
            // 공격 간격 과 상태이상(cc기 ) 판별
            float attackDelay = 1/attackSpeedT;
            if (lastAttackTime + attackDelay <= GameManager.gm.totalGameTime)
            {
                return true;
            }
            return false;
        }
    }
    
    // 타겟 관련
    LayerMask targetLayer = 1<<11;           // 적 레이어
    public RaycastHit2D[] targets;          // 사거리 내의 적 표시

    public Transform target;                // 타겟의 위치
    public List<Transform> list_targets;    // 타겟의 위치 정보 리스트 ( 단일 타겟의 경우 1개, 아닐경우 여러개 (투사체 수의 영향을 받음))
    public bool hasTarget                   // 타겟이 있는지, 그리고 그 타겟이 살아있는지 
    {
        get
        {
            int requiredTargetNum = (isSingleTarget)? 1:projNumT;
            if (list_targets.Count >= requiredTargetNum )
            {
                return true;
            }
            return false;

        }
    }

    Coroutine battleflow_c;

    //
    public AudioSource audioSource;


    //===================================================================================================================
    //================================ 풀링 관련 ==================================================================================
    public void InitEssentialInfo()
    {
        InitEssentialInfo_weapon();
    }

    protected abstract void InitEssentialInfo_weapon();
    
    //================
    // GetID
    //==============
    public string GetId()
    {
        return id_weapon;
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

    //===================================
    // 필수 정보 초기화 - 무기 도감이 초기화 될때 '반드시' 호출되어야함. ( 현재 초기화 장소 - 무기사전 )
    //===================================
    // public abstract void InitEssentialWeaponInfo();


    //===================================
    // 공통 무기 초기화 
    //===================================
    public void InitWeapon()
    {
        animator = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        
        mouseTarget = MouseTarget.mt.myTransform;
        handTransform = transform.parent;

        originalHandPosition = Player.player.t_player.position + new Vector3(1,5);

        handTransform.position = originalHandPosition;
        handTransform.rotation = Quaternion.identity;


        if (this.gameObject.activeSelf)
        {
            ResetBattleFlow();
        }
            
        InitWeapon_custom();    //개별초기화
    }

    //===================================
    // 개별 초기화 -  // 공속, 사거리, 공격력 등 
    //===================================
    public abstract void InitWeapon_custom();
    


    //======================================================================================================================================
    //===================================
    // 무기의 전투 플로우
    //===================================
    public IEnumerator BattleFlow()
    {
        while (true)
        {
            SearchTarget(projNumT);     //타겟세팅

            // 타겟에게 회전
            if(isRotatable)
            {
                StartCoroutine(RotateWeapon());
            }

            // 공격가능한 상황일때 공격
            if (canAttack && list_targets.Count !=0 )
            {
                Attack();                   //타겟공격
            }
            

            float attackDelay = 1/attackSpeedT;
            yield return new WaitForSeconds(attackDelay);  
        }
    }
    
    //==========================================
    // 전투플로우 초기화
    //==========================================
    public void ResetBattleFlow()
    {
        lastAttackTime = -500;
        if (battleflow_c != null)
        {
            StopCoroutine(battleflow_c);
        }
        battleflow_c = StartCoroutine(BattleFlow());
    }



    //===================================
    // 타겟 탐색 함수 - searchNum == 탐색할 타겟의 개수 ( default = projNumT )
    // 타겟은 매 탐색마다 가장 가까운 적들을 찾는다 (단일 타겟 무기일 경우) 
    // 혹은 범위내 투사체 수만큼의 임의의 적을 찾는다. 
    //===================================
    public void SearchTarget(int searchNum)
    {
        list_targets.Clear();  // 일단 현재 타겟 리스트 초기화 
        targets = Physics2D.CircleCastAll(Player.player.transform.position, rangeT, Vector2.zero, 0, targetLayer);

        
        // autoAim일때 
        if (!Player.player.autoAim)
        {
            target = mouseTarget;
            list_targets.Add(target);
            // searchNum--;
        }
        //------------------------------------

        // 단일 타겟 무기일 경우 가장 가까운 적을 공격
        if (isSingleTarget)
        {
            Transform result = null;
            float minDiff = 987654321;

            foreach(var t in targets)
            {
                Vector3 myPos = transform.position;
                Vector3 targetPos = t.transform.position;

                float currDiff = Vector3.Distance(myPos, targetPos);

                if (currDiff < minDiff) 
                {
                    minDiff = currDiff;
                    result = t.transform;
                }
            }
            // 가장 가까운 적을 리스트에 추가 
            if (result !=null)
            {
                list_targets.Add(result.GetComponent<Enemy>().center);
            }
        }

        // 단일 타겟 무기가 아닐 경우, 범위 안의 탐색수(투사체 수) 만큼 적을 탐색 (랜덤으로) - 같은 적을 두번 추가할 수 있음
        // 타겟리스트가 비었다면( 사거리 내 적이 없다면) 반환
        if (targets.Length ==0)
        {
            return;
        }
        int startIdx = (Player.player.autoAim)?0:1;
        
        for (int i=startIdx;i<searchNum;i++)
        {
            int idx = Random.Range(0,targets.Length);
            list_targets.Add(targets[idx].transform.GetComponent<Enemy>().center);
        }
    } 

    //===================================
    // 대상이 범위 안인지
    //==================================
    public bool InRange(Vector3 v)
    {
        float distSqr = Vector3.SqrMagnitude(v - Player.player.t_player.position);

        if (distSqr <= rangeT * rangeT)
        {
            return true;
        }
        return false;
    }
    //===================================
    // 최대 사거리 지점 get
    //==================================
    public Vector3 GetMaximumRangePos(Vector3 v)
    {
        Vector3 dir = (v - Player.player.t_player.position).normalized;

        Vector3 retPos = Player.player.t_player.position + dir * rangeT;
        return retPos;
    }


    //=======================================
    // 무기 회전_공통 : 단일 대상 공격 무기일 경우 회전함. 
    //=======================================
    IEnumerator RotateWeapon()
    {
        while(list_targets.Count!=0)
        {
            // 타겟이 무기보다 왼쪽에 있는 경우 스프라이트 뒤집기
            if (list_targets[0] != null)
            {
                Vector3 targetPos = (Player.player.autoAim)? list_targets[0].position : mouseTarget.position;
                Vector3 dir = targetPos - handTransform.position;

                bool targetOnLeft = dir.x<0;    // 타겟이 왼쪽에 있는지 
                spriter.flipX = targetOnLeft; // 타겟이 왼쪽에 있으면 플립 

                // 그리고 회전 (총구가 타겟을 향하게)
                // Vector3 rotDir =targetOnLeft? Vector3.left: Vector3.right;
                handTransform.rotation = Quaternion.FromToRotation(Vector3.up, dir);

                               
            }
            yield return null;

        }
    }



    //=======================================
    // 공격_공통
    //=======================================
    public void Attack()
    {

        // 공격 모션 실행 (애니메이터 있는 것에 한해 )
        if (animator != null)
        {             
            StartCoroutine(SetAttackAnimation());
        }
        
        lastAttackTime = GameManager.gm.totalGameTime;   // 마지막 공격 시간 갱신
        Attack_custom();
    }

    //
    public IEnumerator SetAttackAnimation()
    {
        animator.SetTrigger("attack");
        float asMul = ( 100 + Player.player.Attack_Speed_Plus* weight_attackSpeed )  * 0.01f; ///  *********************
        animator.SetFloat("asMul",asMul ); 

        yield return new WaitForFixedUpdate();      // 애니메이션 전환 속도때문에 딜레이 줘야함.
        // animator.speed = attackSpeedT;
        animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

    }



    //=======================================
    // 공격_개별 ( 무기 특성 반영 코드)
    //=======================================
    public abstract void Attack_custom();

    //======================================================================================================================================



    //====================================================
    // 무기 교체시 발생하는 효과
    //====================================================
    public abstract void onDestroyWeapon();



    //==================================================================================


    // 생성되면, 
    void Start()
    {        
        // 초기화 하고 전투 플로우 시작 ( 타겟 찾고, 공격 )
        InitWeapon();
    }


    void Destroy()
    {
        onDestroyWeapon();
    }

}
