using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;



public  enum BattleType { none, melee, range, special };      // 움직임 제어를 위한 타입 설정

public abstract class Enemy : MonoBehaviour , IPoolObject
{
    protected bool isBoss;     // 보스몬스터인지
    public bool deathAnimationEnd;
    public bool ready;
    // public float deltaScale;

    public bool oneShot = false;
    public bool shot = false;
    
    //
    protected float movementSpeed; // �̵��ӵ�

    float moveDelay => 1/movementSpeed;

    Action moveAction; 

    protected BattleType battleType;

    //
    
    public float hp_curr; // ü��

    public float hp_max;     // ****************

    // public int def;

    public int damage; // ���ݷ�
    protected float range;

    protected bool strongAttack = false;

    protected float attackSpeed;   // ���ݼӵ�(�ʴ� ���ݼӵ�)

    float attackDelay;
    // protected bool state = true; // ���� ���� true : �⺻����, false : �����̻� 
    float lastAttackTime;

    protected bool canKnockBack = true;
    float knockBack_time = 0.2f;
    bool onKnockBack;
    protected bool hasAttackCustom;

    float lastMoveTime;
    bool stunned;
    protected bool canMove = true;
    // public bool canAttack_ = true;      // can damage player 
    bool canAttack       // ���� ���� ����
    {
        get
        {
            if (stunned || attackSpeed == 0)                
            {
                return false;
            }            
            // ���� ���� �� �����̻�(cc�� ) �Ǻ�
            if (oneShot && shot)
            {
                return false;
            }

            if (lastAttackTime + attackDelay <= Time.time)
            {
                return true;
            }
            return false;
        }
    }
    bool canMoving
    {
        get
        {
            if (stunned || movementSpeed == 0)
            {
                rb.velocity = Vector2.zero;
                return false;
            }
            if (!canMove)
            {
                return false;
            }

            if (lastMoveTime + moveDelay <= Time.time)
            {
                return true;
            }
            return false;
        }
    }


    // Item
    public float itemProb;  // probability of drop other items (not mana)
    public float manaValue; // mana acquisition amount



    protected Rigidbody2D rb;
    public Transform myTransform;
    public Transform center;

    public Transform target; // ���� ���
    public Vector3 target_proj;

    public string id_enemy;

    //==========
    //애니메이션 관련                   //  *******************************************************************************
    protected Animator animator;

    public bool isDead = false;

    public Vector3 originScale;

    // sound
    protected AudioSource audioSource;

    public AudioClip sound_death;


    // dot damage Q ==============================
    public bool onBleeding;
    public Queue<float> dotQ = new Queue<float>();
    // public int currDotNum;
    //=====================================================================================
    
    
    //===========================
    // IPoolObject : resourceManager에 로드시에 id 초기화 
    //===========================
    public void InitEssentialInfo()
    {
        InitEssentialInfo_enemy();
    }
    //
    protected abstract void InitEssentialInfo_enemy();


    //================
    // GetID
    //==============
    public string GetId()
    {
        return id_enemy;
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

    //===============================================================


    // 초기화작업 (공통)                         
    public void InitEnemyStatus()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = true;
        animator = GetComponent<Animator>();
        

        target = Player.player.transform; // ���� ��� = �÷��̾�

        

        myTransform.localScale = originScale;
        myTransform.rotation = Quaternion.identity;


        InitEnemyStatusCustom();                    // 개별 능력치 먼저 초기화
        SetMoveAction();        // 움직임 함수 설정. 



        hp_curr = hp_max;

        canMove = false;


        StartCoroutine( Grow() );
        
        StartCoroutine( GetDotDamage() );       // detect bleeding dmg continuously 

        // ready 되면 실행됨.
        
        StartCoroutine( AttackFlow());
        StartCoroutine( MoveFlow());
    }

    // 개별 능력치 초기화
    public abstract void InitEnemyStatusCustom();

    // public abstract void InitEssentialEnemyInfo();


    //====================================================================================================================

    //===================================
    // ������ ���� �÷ο�
    //===================================
    IEnumerator AttackFlow()
    {
        yield return new WaitUntil( ()=>ready);
        
        
        while (!isDead)     // 죽은 상태가 아닐때 전투플로우 
        {
            if (canAttack)
            {
                lastAttackTime = Time.time;   // ������ ���� �ð� ����
                
                StartCoroutine(AttackAnimation());
                AttackCustom();
            }

            yield return new WaitForSeconds(attackDelay);
        }
    }

    IEnumerator MoveFlow()
    {
        yield return new WaitUntil( ()=>ready);
        StartCoroutine( PlayAnim_move()); 

        canMove = true;
        while (!isDead)
        {
            if (canMoving)
            {
                lastMoveTime = Time.time;
                // MoveCustom();

                if (moveAction !=null)
                    moveAction();
            }

            yield return new WaitForSeconds(moveDelay);
        }
    }

    protected void SetMoveAction()
    {
        switch(battleType)
        {
            case BattleType.melee:
                moveAction = MoveAction_melee;
                break;

            case BattleType.range:
                moveAction = MoveAction_range;
                break;

            case BattleType.special:
                moveAction = MoveAction_special;
                break;

            default:
                moveAction = ()=>{};
                break;
        }
    }


    void MoveAction_melee()
    {
        Vector3 dirVec = target.position + new Vector3(UnityEngine.Random.Range(-2f, 2f),UnityEngine.Random.Range(-2f, 2f)) - myTransform.position;  // 위치에 약간의 오차를 준다. 
        
        rb.velocity = Vector2.zero; // 물리적 속도 0으로 고정
        rb.velocity = dirVec.normalized * movementSpeed;
    }

    void MoveAction_range()
    {
        float  distance = Vector3.Distance(myTransform.position, target.position);

        Vector3 dirVec = target.transform.position + new Vector3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-2f, 2f)) - myTransform.position;  // ���� = Ÿ�� ��ġ - �� ��ġ
        rb.velocity = Vector2.zero; // 물리적 속도 0으로 고정


        // 사거리 안으로 이동 
        if (distance >= range *0.9f)
        {
            rb.velocity = dirVec.normalized * movementSpeed;
        }
        // 도망 ( 거리벌리기 )
        else
        {
            rb.velocity = dirVec.normalized * -movementSpeed;
        }
    }

    void MoveAction_special()
    {

    }



    // ����
    protected abstract void AttackCustom();
    //protected abstract void MoveCustom();

    //protected abstract void DieCustom(); //********************************


    //
    public IEnumerator AttackAnimation()
    {
        if (hasAttackCustom)
        {
            Vector3 thisScale = myTransform.localScale;
            for (int i=0;i<10;i++)  //50%
            {
                thisScale.y = originScale.y*(1 - 0.05f * i);
                myTransform.localScale = thisScale;
                yield return new WaitForSeconds(0.02f);
            }
            yield return new WaitForSeconds(0.6f);
            for (int i=0;i<5;i++)
            {
                thisScale.y = originScale.y* (0.5f + 0.14f * i);
                myTransform.localScale = thisScale;
                yield return new WaitForSeconds(0.02f);
            }
            for (int i=0;i<5;i++)
            {
                thisScale.y = originScale.y* (1.2f - 0.04f * i);
                myTransform.localScale = thisScale;
                yield return new WaitForSeconds(0.02f);
            }
        }
    }










    // 죽을 때 아이템 드랍 ( 마나 포함)
    protected virtual void DropItem()
    {
        // default : drop mana.
        DropItem item = ItemPoolManager.instance.SpawnItem("000", manaValue, transform.position);

        if (UnityEngine.Random.Range(0,100) < itemProb)
        {
            // drop heal item if you are 'lucky'
            DropItem luckyItem = ItemPoolManager.instance.SpawnItem("001", 0, transform.position);
        }
    }

    // ����
    protected void Die()                                  //********************************
    {             
        isDead = true;
        ready = false;

        dotQ.Clear();

        rb.simulated = false;



        StartCoroutine( DeathAnimation());





        if (!isBoss)
        {
            GameEvent.ge.onEnemyDie.Invoke(this);
            if(audioSource !=null)
            {
                audioSource.PlayOneShot(sound_death);
            }
            
        }
        DropItem();
        
        GameManager.gm.KillCount += 1;
        GameManager.gm.Score += (int)hp_max;
    }

    // ===============================
    // Clean Death : not drop item, not increase score 
    // ================================
    public void CleanDeath()
    {
        isDead = true;
        ready = false;

        dotQ.Clear();

        rb.simulated = false;

        
        StartCoroutine( DeathAnimation());        
    }



    // 죽는 애니메이션 (빙글빙글 돌면서 크기가 작아짐)
    public IEnumerator DeathAnimation()
    {
        if (!isBoss)
        {


            //            
            int tickNum = 10;

            float angle= 36;
            for (int i=tickNum-1;i>0;i--)
            {
                myTransform.localScale = originScale * 0.1f * i;
                myTransform.rotation = Quaternion.Euler(0,0,angle);

                angle+=36;
                yield return new WaitForSeconds(0.05f);
            }
            deathAnimationEnd = true;
        }
        // wait until animation end 
        yield return new WaitUntil(() => deathAnimationEnd);    

        EnemyPoolManager.instance.TakeToPool(this);
    }
    
    //==============================================
    // 생성 직후 점차적으로 커지도록 하여 자연스러운 연출
    //==============================================
    public IEnumerator Grow()
    {
        if (!isBoss)
        {
            float animationPlayTime = 0.5f;
            PlayAnim_grow(animationPlayTime);
            yield return new WaitForSeconds( animationPlayTime  + 0.2f);
        }

        ready = true;   // ㄹㄷ 되면 전투 시작됨.
    }


    public void PlayAnim_grow(float time)
    {
        myTransform.localScale = Vector2.zero;
        Sequence seq = DOTween.Sequence()
        .Append( myTransform.DOScale( originScale, time))
        .Play();
    }


    //========================================================================
    // 평상시 이동 애니메이션 (두근두근거림)
    //========================================================================
    public IEnumerator PlayAnim_move()
    {
        yield return new WaitUntil(()=>ready);

        float targetScale = (isBoss)?1.02f:1.06f;

        Sequence seq = DOTween.Sequence()
        .Append( myTransform.DOScale(originScale* targetScale, 0.4f))
        .SetLoops(-1, LoopType.Yoyo)
        .Play();
    }


    // get dmg for direct attack ( knockback & bleed ) 
    public void Damaged(int damage, Vector3 hitPoint, float knockbackPower)
    {
        hp_curr -= damage;

        //drain
        // int prob = Random.Range(1, 101);
        // if (prob <= Player.Instance.Drain_prob)
        //     Player.Instance.ChangeHp(Player.Instance.Drain);


        //knockback
        if (canKnockBack && knockbackPower>0 && !onKnockBack)
        {
            StartCoroutine(KnockBack(knockbackPower, hitPoint));
        }

        //
        Damaged_custom();

        //bleed
        Bleed( (int)(damage * 0.2f));


        // detect death
        if (hp_curr <= 0)
        {
            hp_curr = 0;
            Die();
        }
    }

    // get dmg for indirect attack ( not knockback)
    public void Damaged(float damage)
    {
        hp_curr -= damage;

        //drain
        // int prob = Random.Range(1, 101);
        // if (prob <= Player.Instance.Drain_prob)
        //     Player.Instance.ChangeHp(Player.Instance.Drain);
        
        //
        Damaged_custom();

        // detect death
        if (hp_curr <= 0)
        {
            Die();
        }
    }

    public virtual void Damaged_custom()
    {

    }

    // knockBack 
    public IEnumerator KnockBack(float power, Vector3 pos)
    {
        // int weight = (int)(power/5);
        float duration = 0.2f;
        Stunned(duration, false);
        onKnockBack =true;
        
        Vector3 dir = (center.position - pos).normalized;
        rb.velocity = dir * power;
        // rb.AddForce(dir * power );

        yield return new WaitForSeconds(duration);

        rb.velocity *= 0.7f;
        yield return null;
        rb.velocity *= 0.7f;
        yield return null;
        rb.velocity *= 0.7f;
        yield return null;

        onKnockBack =false;
        rb.velocity = Vector3.zero;
        

    }
    //===================================================
    // stun : can't move and attack
    //===================================================
    public void Stunned(float time, bool isStrongAttack)
    {
        rb.velocity = Vector3.zero;
        stunned = true;
        // canMove = false;
        // canAttack_ = false;
        
        StartCoroutine( SetDuration_stun(time));


        if (isStrongAttack)
        {
            GameEvent.ge.onEnemyStunned.Invoke(this);
        }
    }

    //===================================================
    // release stun
    //===================================================
    public IEnumerator SetDuration_stun(float time)
    {
        yield return new WaitForSeconds(time);
        // canMove = true;
        // canAttack_ = true;
        stunned = false;
    }


    //===================================================================================================
    // dot damage
    //=========================================
    public void Bleed(float dpt)
    {
        if(isDead)
        {
            return;
        }
        
        int num = UnityEngine.Random.Range(0, 100);
        if ( num < Player.player.bleedingLevel * 25)
        {
            // Debug.Log("출혈");
            int tickNum = 6;
            Dot_Init(dpt, tickNum);
        }
    }

    //========================================
    // dot init : set waitQ /  send dot from waitQ to dotQ continuously  (dmg per tick, tickNum)
    //=======================================
    public void Dot_Init(float dpt, int tickNum)
    {
        Queue<float> waitQ = new Queue<float>();
        for(int i=0;i<tickNum;i++)
        {
            waitQ.Enqueue(dpt);
        }

        StartCoroutine(Dot_sendQ( waitQ ) );
    }

    //========================================
    // send dmg from waitQ to dotQ 
    //=======================================
    public IEnumerator Dot_sendQ( Queue<float> waitQ )
    {
        while(waitQ.Count>0)
        {
            if (isDead)
            {
                waitQ.Clear();
                break;
            }
            
            dotQ.Enqueue( waitQ.Dequeue() );

            yield return new WaitForSeconds(0.5f);
        }

    }
    //========================================
    // get dot dmg
    //=======================================
    public IEnumerator GetDotDamage()
    {
        yield return null;  // delay an frame to wait for every waitQ 

        if (!onBleeding)
        {
            onBleeding = true;
            while(true)
            {
                Damaged_dot();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
    
    //=================
    // get dot dmg
    //=================
    public void Damaged_dot()
    {
        if (isDead)
        {
            return;
        }
        
        if (dotQ.Count==0)
        {
            onBleeding = false;
            return;
        }
        // 현재 dotQ에 있는 dmg들을 계산해서 피해입음 
        float totalTickDmg = 0f;
        int num = 0;
        while(dotQ.Count>0)
        {
            totalTickDmg += dotQ.Dequeue();
            num++;
        }
        

        //
        Damaged(totalTickDmg);

        GameEvent.ge.onEnemyBleeding.Invoke(center.position,(int)totalTickDmg);
    }

    //===================================================================================

    public void Healing(float heal)
    {
        // 1. 마지막 공격받은 시간에서 일정시간 지나면 스스로 힐
        // 2. 버프몬스터 근처에 있다가 힐 받음
        hp_curr += heal;
        if (id_enemy.Equals("b_001"))
        {
            Damaged_custom();   // To Set Hp Bar
        }

        if (hp_curr > hp_max)
        {
            hp_curr = hp_max;
        }

        GameEvent.ge.onEnemyheal.Invoke(center.position,(int)heal);
    }


    //=========================================================================================



    //==============================================
    void Awake()
    {
        originScale = transform.localScale;         // 가장 처음
        myTransform = transform;
        center = transform.Find("Center");

        audioSource = GetComponent<AudioSource>();
        sound_death = Resources.Load<AudioClip>("Sound/19_Mondead");
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
    }



    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player") && canAttack_)    // player get dmg when canAttack_
    //     { 
    //         Vector3 hitPoint = center.position;
            
    //         int dmg = damage;

    //         if (dmg != 0 )
    //         { 
    //             // Player.player.OnDamage(dmg);
    //             Player.Instance.OnDamage(damage, hitPoint, strongAttack);
    //         }
    //     }
    // }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )    // player get dmg when canAttack_
        { 
            Vector3 hitPoint = center.position;
            
            int dmg = damage;

            if (dmg != 0 )
            { 
                // Player.player.OnDamage(dmg);
                Player.player.OnDamage(damage, hitPoint, strongAttack);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") )    // player get dmg when canAttack_
        { 
            Vector3 hitPoint = center.position;
            
            int dmg = damage;

            if (dmg != 0 )
            { 
                // Player.player.OnDamage(dmg);
                Player.player.OnDamage(damage, hitPoint, strongAttack);
            }
        }
    }

    //// 넉백
    ///넉백 힘 무기별 차이점
    //1. 넉백 지속시간 동안 이동불가
    //2. velocity = 
    // 3. or adforce impulse rigidbody 모드

}
