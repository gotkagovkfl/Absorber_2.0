using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using System;

//===========================================
// 플레이어 능력치 관리 
//============================================
public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance { get { return instance; } }

    public PlayerUI playerUI;
    public List<string> chooseList = new List<string>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        penetration = 0;
        projScale = 0f;
        projLifeTime = 0f;
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>(); 
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        sound_playerHit = Resources.Load<AudioClip>("Sound/6_playerhit");
        sound_playerDash = Resources.Load<AudioClip>("Sound/17_dash");      
        sound_createSanctuary = Resources.Load<AudioClip>("Sound/16_healcamp");
        sound_changeAuto = Resources.Load<AudioClip>("Sound/21_targetchange");


        playerUI = GetComponent<PlayerUI>();

        myTransform = transform;
        init();
    }

    public Transform myTransform;
    public Transform center;
    
    #region 기초스텟
    public int Level = 1; // 레벨
    public int Atk; // 공격력
    public int Hp;  // 체력
    public int Max_Hp; // 최대체력
    public float Exp; // 레벨업 필요 경험치
    public float Cur_Exp; // 현제 경험치
    public float Range; // 공격 사거리 & 아이템 획득 범위
    public int Range_Plus;
    public float Speed; // 이동속도
    public int Speed_Plus;
    public float Attack_Speed; // 공격속도
    public int Attack_Speed_Plus;
    public int Def; // 방어력
    public bool inv = false; // 무적
    public int Drain; // 흡혈
    public int Drain_prob; // 흡혈확률
    public int penetration; // 총알 관통
    public int Avoid_prob; // 회피확률
    public float Hp_item_up; // 고기 회복량 증가
    public int Exp_up; //경험치 획득량 증가
    public int Crit; // 치명타 확률
    public int Life; // 목숨
    public float Avoid_Time; // 회피시간
    public int Avoid_Select_Count;
    public int Avoid_Atk;
    public int Reinforce_Prob; // 강화공격 확률
    public bool protection = false; // 보호막 생성하기
    public int Stop_Def;
    public bool Attack_exp;
    public int exp_amaount;
    public float projScale;
    public float projLifeTime;
    public float projSpeed;
    // public Image Dash_UI;
    //
    public int projNum;
    public int splitNum;
    public bool is_explosion;
    public int explosionLevel;
    public int bleedingLevel;

    public int sanctuaryLevel;
    // 대쉬 //
    public bool canDash = true;
    private bool isDashing;
    private float dashingPower = 4f;
    private float dashingTime = 0.2f;
    public float dashingCooldown = 3f;
    // 대쉬 //
    public int Luk; // 행운
    public List<Coroutine> RunningCoroutines = new List<Coroutine>();

    public int[,] Luk_Table = new int[,] { { 60, 30, 9, 1}, { 50, 35, 14, 1 }, { 35, 45, 15, 5 }, { 30, 45, 20, 5 }, { 25, 40, 30, 5 } };
    #endregion
    public Vector2 inputVector;
    public GameObject LevelUpManager;
    public Rigidbody2D rb;
    public bool alive = true;

    public bool canMove = true;
    public bool canAttack = true;

    public bool autoAim = true;
    public GameObject obj_directAim;
    public GameObject obj_autoAim;
    
    //===================================
    public GameObject pauseUI;
    public bool OnSelecting;        //true when levelup popup set active
 
    // 애니메이션을 위한 스프라이트 렌더러 
    SpriteRenderer spriter;
    Animator animator;

    public Coroutine damagedCoroutine;

    //================================================
    public AudioSource audioSource;
    public AudioClip sound_playerHit;
    public AudioClip sound_playerDash;

    public AudioClip sound_createSanctuary;

    public AudioClip sound_changeAuto;


    //====================================
    // 플레이어의 경험치에 변동이 생겼을 때 
    //====================================
    public void Reinforce()
    {
        Reinforce_Prob = Mathf.Min(100, Reinforce_Prob + 10);
    }
    public void Hp_Up()
    {
        Max_Hp += 12;
        ChangeHp(12);
        Speed_Plus -= 8;
    }
    public void RHp_Up()
    {
        Max_Hp = Mathf.Max(1, Max_Hp - 6);
        ChangeHp(Mathf.Max(-6, 1-Hp));
        Speed_Plus += 16;

    }
    public void Atk_Up()
    {
        Atk += 4;
        Attack_Speed_Plus -= 10;
    }
    public void RAtk_Up()
    {
        Atk = Mathf.Max(1, Atk - 2);
        Attack_Speed_Plus += 20;
    }
    public void RRange_Up()
    {
        Exp_up += 50;
        Range_Plus -= 20;
    }
    public void Range_Up()
    {
        Exp_up -= 20;
        Range_Plus += 50;

    }
    public void Avoid_Up()
    {
        Avoid_prob = Mathf.Min(100, Avoid_prob + 15);
        Def -= 20;
    }
    public void RAvoid_Up()
    {
        Avoid_prob = Mathf.Max(0, Avoid_prob - 6);
        Def += 50;
    }
    public void Defense_Up()
    {
        Stop_Def += 100;
    }
    public void Random_Stat()
    {
        init();
        for(int i = 1; i < Level; i++)
        {
            LevelUpManager.GetComponent<LevelUpManager>().AutoLevelUp();
        }
    }
    public void Random_Statup()
    {
        chooseList.RemoveAt(chooseList.Count - 1);
        LevelUpManager.GetComponent<LevelUpManager>().AutoLevelUp();
    }
    public void Bless()
    {
        sanctuaryLevel++;
    }
    public void Dot()
    {
        bleedingLevel++;
    }
    public void Explosion()
    {
        is_explosion = true;
        explosionLevel++;
    }
    public void Protect()
    {
        protection = true;
        RunningCoroutines.Add(StartCoroutine(Protect20()));
    }
    IEnumerator Protect20()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);
            if (!protection)
                protection = true;
        }
    } 
    public void Exchange()
    {
        RunningCoroutines.Add(StartCoroutine(exchange()));
    }
    IEnumerator exchange()
    {
        while(true)
        {
            Atk++;
            Def = Mathf.Max(0, Def - 5);
            yield return new WaitForSeconds(20f);
        }
    }
    public void Avoiding()
    {
        Avoid_Select_Count++;
        Avoid_Time = Time.time;
        RunningCoroutines.Add(StartCoroutine(avoiding()));
    }
    IEnumerator avoiding()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            Avoid_Atk = Avoid_Select_Count * (int)((Time.time - Avoid_Time)/10);
        }
    }
    public void ChangeExp(float value)
    {
        Cur_Exp += value + value * Exp_up / 100f;

        #region 레벨업
        if(Cur_Exp >= Exp)
        {
            Cur_Exp = Cur_Exp - Exp;
            Exp = (int)(Exp * 1.18f);
            Level++;

            
            Effect effect = EffectPoolManager.epm.GetFromPool("006");
            effect.InitEffect(center.position);
            effect.ActionEffect();



            LevelUpManager.GetComponent<LevelUpManager>().LevelUp();
            playerUI.SetLevelText();
        }
        #endregion

        playerUI.SetMpBar();
    }

    public void StartInvincible()
    {
        StartCoroutine(Invincible(30f));
    }
    IEnumerator Invincible(float time)
    {
        inv = true;
        yield return new WaitForSeconds(time);
        inv = false;
    }
    private IEnumerator Dash()
    {
        StartCoroutine(Invincible(dashingTime));
        StartCoroutine(OnDashEffect());

        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 dir = (worldPosition - transform.position).normalized;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        canDash = false;
        isDashing = true;
        rb.gravityScale = 0f;
        rb.velocity = rb.velocity * dashingPower;
        
        playerUI.OnUseDash();

        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        //StartCoroutine(Dash_CoolTime(dashingCooldown + 1f));
        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }

    public IEnumerator OnDashEffect()
    {
        audioSource.PlayOneShot(sound_playerDash);
        for (int i=0; i< 5; i++)
        {
            Effect effect = EffectPoolManager.epm.GetFromPool("012");
            effect.InitEffect(myTransform.position);
            effect.ActionEffect();
            effect.GetComponent<SpriteRenderer>().flipX = spriter.flipX;

            yield return null;
            yield return null;
            yield return null;
        }
    }


    // 스텟 초기화
    public void init()
    {
        Atk = 0;
        Max_Hp = 100;
        Hp = Max_Hp;
        Exp = 20;
        Cur_Exp = 0;
        Range = 0.5f;
        Range_Plus = 0;
        Speed = 4f;
        Speed_Plus = 0;
        Attack_Speed = 0.5f;
        Attack_Speed_Plus = 0;
        Avoid_Select_Count = 0;
        Avoid_Atk = 0;
        Luk = 0;
        Def = 0;
        Drain = 0;
        Drain_prob = 0;
        Avoid_prob = 0;
        Hp_item_up = 5;
        Exp_up = 0;
        Life = 1;
        Reinforce_Prob = 0;
        Attack_exp = false;
        exp_amaount = 0;
        protection = false;
        Avoid_Time = 0;
        splitNum = 0;
        projNum = 0;
        Crit = 0;
        Reinforce_Prob = 0;

        autoAim = true;


        Stop_Def = 0;
        explosionLevel = 0;
        is_explosion = false;
        myTransform = transform;
        center = myTransform.Find("Center");

        sanctuaryLevel = 0;

        canMove = true;
        canAttack = true;
		//
        autoAim = true;

		obj_directAim =GameObject.Find("Aim");
        obj_autoAim = obj_directAim.transform.GetChild(0).gameObject;

        ChangeAimImage();
        //
        pauseUI = GameObject.Find("Canvas").transform.Find("PauseUI").gameObject;
        pauseUI.SetActive(false);		
		//
        foreach (Coroutine C in RunningCoroutines)
            StopCoroutine(C);
        RunningCoroutines.Clear();
        chooseList.Clear();
    }
    //====================================
    // 플레이어의 체력에 변동이 생겼을 때 
    //====================================
    public void ChangeHp(int value)
    {
        Hp += value;

        if (Hp > Max_Hp)
        {
            Hp = Max_Hp;
        }


        playerUI.SetHpBar();
        playerUI.SetBloodOverlay();

        string absValue = ((value >= 0) ? value : -value).ToString();
        Color color = ((value >= 0) ? Color.green : Color.red);
        
        EffectPoolManager.epm.CreateText(center.position, absValue, color,2);

        if (value >0)
        {
            Effect effect =EffectPoolManager.epm.GetFromPool("003");
            effect.InitEffect(myTransform.position);
            effect.ActionEffect();
        }
    }
    public void Attacked_Exp()
    {
        Attack_exp = true;
        exp_amaount++;
    }
    //====================================
    // 플레이어가 피해 받을 때,
    //====================================
    public void OnDamage(int dmg,  Vector3 hitPoint, bool knockBackFlag)
    {
        if (DirectingManager.dm.onDirecting == true)
        {
            return;
        }
        
        Avoid_Time = Time.time;
        //무적
        if (inv)
            return;	
        else if(protection)
        {
            protection = false;
            return;
        }

        if (!knockBackFlag)
        {
            StartCoroutine(Invincible(0.5f));        // 무적처리 (0.5초간 무적)
        }

        // 회피
		int prob = Random.Range(1, 101);
        if (prob <= Avoid_prob)
        {
            EffectPoolManager.epm.CreateText(hitPoint, "MISS", Color.gray,0);
            return;
        }
        // 방어 계산
        int finalDamage = 0;
        // 방어 계산
        if (rb.velocity.magnitude < Speed + Speed * Speed_Plus / 100f)

            finalDamage = (int)Mathf.Max(dmg * (100f / (100 + Def + Stop_Def)), 0);   // 공격의 경우 방어력 계산하여 피해량이 0보다 작아지면 방어하기 위해 0으로 설정 
        else
            finalDamage = (int)Mathf.Max(dmg * (100f / (100 + Def)), 0);      // 공격의 경우 방어력 계산하여 피해량이 0보다 작아지면 방어하기 위해 0으로 설정 
        if (finalDamage <= 0)
        {
            EffectPoolManager.epm.CreateText(hitPoint, "GUARD", Color.gray,0);
            return;
        }
        // 데미지 처리 
        if (knockBackFlag)
        {
            KnockBack(10f, hitPoint);
        }

        ChangeHp(-(finalDamage));      
        //
        OnDamageEffect(hitPoint);
        //
        audioSource.PlayOneShot(sound_playerHit);

        //
		if (Attack_exp)
            ChangeExp(exp_amaount * Exp *0.15f);
        #region 죽음
        if(Hp <= 0)
        {
            Life -= 1;
            if (Life == 0)
            {
                alive = false;
                Die();
            }
            else
                ChangeHp(Max_Hp);
        }
        #endregion    
    }
    //==========================
    // 피격 효과
    //=========================
    public void OnDamageEffect(Vector3 hitPoint)
    {
        //effect
        Effect effect =  EffectPoolManager.epm.GetFromPool("011");
        effect.InitEffect(hitPoint);
        effect.ActionEffect();
        
        // change Color
        damagedCoroutine = StartCoroutine( OnDamageEffect_c() );
    }
    
    //==========================
    // 피격 효과
    //=========================
    public IEnumerator OnDamageEffect_c()
    {
        Color color = new Color(1.0f,1.0f,1.0f,1.0f);
        for (int i=0; i<4;i++)
        {
            color.g = 0.65f;
            color.b = 0.65f;
            spriter.material.color = color;
            yield return null;
            yield return null;

            color.g = 0.3f;
            color.b = 0.3f;
            spriter.material.color = color;
            yield return null;
            yield return null;

            color.g = 0.65f;
            color.b = 0.65f;
            spriter.material.color = color;
            yield return null;
            yield return null;

            color.g = 1.0f;
            color.b = 1.0f;
            spriter.material.color = color;
            yield return null;
            yield return null;
        }     
    }



    //====================================
    //
    //====================================
    public void Die()
    {
        // BackendRank.Instance.RankInsert(GameManager.gm.KillCount);
        StageManager.sm.currStage.audioSource.Stop();
        audioSource.PlayOneShot( Resources.Load<AudioClip>("Sound/14_defeat"));

        //die 
        
        rb.simulated = false;
        rb.velocity = Vector3.zero;
        canMove = false;
        canAttack = false;

        GetComponent<PlayerWeapon>().h[0].gameObject.SetActive(false);

        //게임 실패 호출
        StartCoroutine(WaitAndFinish());
    }

    public IEnumerator WaitAndFinish()
    {
        animator.SetTrigger("die");
        yield return new WaitForSeconds(2f);    // wait for death animation

        Fade.fade.FadeOut( ()=>StageManager.sm.FinishStage(false)  );


        // yield return new WaitForSeconds(1f);    // wait for fade out

        // StageManager.sm.FinishStage(false) ;
    }
	
	public void Life_Up()
    {
        Life++;
    }

    //========================================
    // knockBack 
    //=========================================
    public void KnockBack(float power, Vector3 pos)
    {
        StartCoroutine(KnockBac_c(power , pos));
    }

    public IEnumerator KnockBac_c(float power, Vector3 pos)
    {
        float knockBackDuration = 0.3f;
        PlayerStateManager.psm.Stunned( knockBackDuration + 0.1f );

        Vector3 dir = (center.position - pos).normalized;
        rb.velocity = dir * power;

        yield return new WaitForSeconds(knockBackDuration);
        rb.velocity = Vector3.zero;
    }



    //======================================== special ============================

    //=============
    // create sanctuary on random spawn range
    //=============
    public IEnumerator CreateSanctuary()
    {
        yield return new WaitUntil(()=> sanctuaryLevel>0);
        while(alive)
        {
            audioSource.PlayOneShot(sound_createSanctuary);
            
            Vector3 randPos = StageManager.sm.currStage.GetRandomSpawnPos_spawnRange();
            // 101 is id of sanctuary
            Effect effect = EffectPoolManager.epm.GetFromPool("101");
            effect.InitEffect(randPos);
            effect.ActionEffect();

            yield return new WaitForSeconds(30f);
        }
    }


    //================================================

    //
    //
    //
    public void ChangeAimImage()
    {
        // obj_directAim.SetActive(!autoAim);
        obj_autoAim.SetActive(autoAim);
    }


    //=========================================================================================================================================
  

    // Start is called before the first frame update
    void Start()
    {
        LevelUpManager = GameObject.Find("LevelupManager");
        //
        StartCoroutine(CreateSanctuary());
    }

    void Update()
    {
        // for aim set
        if (Input.GetKeyDown(KeyCode.C))
        {
            
            audioSource.PlayOneShot(sound_changeAuto);

            autoAim = !autoAim;
            ChangeAimImage();
        }

        // pause   
        if (Input.GetKeyDown(KeyCode.Escape) && !OnSelecting) 
        {
            Pause();
        }
        
        
        if (DirectingManager.dm.onDirecting )
        {
            rb.velocity = Vector3.zero;
            animator.SetFloat("speed", 0f);
            return;
        }
        
        if (!DirectingManager.dm.onDirecting && canMove )     // 움직일 수 있을 때에만 
        {
            animator.SetFloat("speed", inputVector.magnitude);
            if (inputVector.x != 0) // 스프라이트 뒤집기(입력에 따라) 
            {
                spriter.flipX = inputVector.x < 0;
            }
            animator.speed = 0.7f;
            
            if (isDashing)
                return;
            inputVector.x = Input.GetAxisRaw("Horizontal");
            inputVector.y = Input.GetAxisRaw("Vertical");
            float final_speed = Speed + Speed * Speed_Plus / 100f;
            rb.velocity = new Vector2(inputVector.x * final_speed, inputVector.y * final_speed);

            if (Input.GetKeyDown(KeyCode.Space) && canDash && Time.timeScale == 1)
            {
                StartCoroutine(Dash());
            }
        }
    }
    

    //
    public void Pause()
    {
        Fade.fade.BtnClickSound();
        
        GameManager.gm.PauseGame(!GameManager.gm.isPaused);
        pauseUI.SetActive(GameManager.gm.isPaused);
        pauseUI.GetComponent<PauseUI>().SetStatus();
    }
}
