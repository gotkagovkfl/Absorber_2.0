using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
// using System;


[RequireComponent(typeof(Rigidbody2D))]
//===========================================
// 플레이어 능력치 관리 
//============================================
public class Player : MonoBehaviour
{
    private static Player _player;
    public static Player player => _player;

    public static bool initialized;

    //
    // public PlayerUI playerUI;
    public List<string> chooseList = new List<string>();
    private void Awake()
    {
        if (_player == null)
        {
            _player = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

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


        // playerUI = GetComponent<PlayerUI>();

        t_player = transform;

    }

    public Transform t_player;
    public Transform center;
    
    #region 기초스텟
    public static PlayerStatus playerStatus;

    public int level => playerStatus.level; // 레벨
    public int atk => playerStatus.atk; // 공격력
    public int hp_curr => playerStatus.hp_curr;  // 체력
    public int hp_max => playerStatus.hp_max; // 최대체력
    public float exp_max => playerStatus.exp_max; // 레벨업 필요 경험치
    public float exp_curr => playerStatus.exp_curr; // 현제 경험치
    // public float range ; // 공격 사거리 & 아이템 획득 범위
    public int range_plus => playerStatus.range_plus;
    public float movementSpeed => playerStatus.movementSpeed; // 이동속도
    public int movementSpeed_plus => playerStatus.movementSpeed_plus;
    // public float attackSpeed => playerStatus.attackSpeed; // 공격속도
    public int attackSpeed_plus => playerStatus.attackSpeed_plus;
    public int def => playerStatus.def; // 방어력
    // public bool invincible => playerStatus.invincible; // 무적
    // public int drain_amount => playerStatus.drain_amount; // 흡혈
    // public int drain_prob => playerStatus.drain_prob; // 흡혈확률
    public int penetration => playerStatus.penetration; // 총알 관통
    public int avoid_prob => playerStatus.avoid_prob; // 회피확률
    // public float hp_item_up => playerStatus.hp_item_up; // 고기 회복량 증가
    
    public int crit_prob => playerStatus.crit_prob; // 치명타 확률
    // public int life => playerStatus.life; // 목숨
    public float avoid_time => playerStatus.avoid_time; // 회피시간
    // public int avoid_select_count => playerStatus.avoid_select_count;
    // public int avoid_atk => playerStatus.avoid_atk;
    public int reinforce_prob => playerStatus.reinforce_prob; // 강화공격 확률
    // public bool protection => playerStatus.prot; // 보호막 생성하기
    // public int stop_def => playerStatus.stop_def;
    // public bool attack_exp => playerStatus.attack_exp;
    // public int exp_amaount => playerStatus.exp_amaount;
    // public float projScale => playerStatus.projScale;
    // public float projLifeTime => playerStatus.projLifeTime;
    // public float projSpeed => playerStatus.projSpeed;
    // public Image Dash_UI;
    //
    public int projNum => playerStatus.projNum;
    public int split => playerStatus.split;
    public int bleedingLevel => playerStatus.bleedingLevel;

    public int sanctuaryLevel => playerStatus.sanctuaryLevel;
    public int explosionLevel => playerStatus.explosionLevel;

    public int luck  => playerStatus.luck; // 행운







    public List<Coroutine> RunningCoroutines = new List<Coroutine>();

    public int[,] Luk_Table = new int[,] { { 60, 30, 9, 1}, { 50, 35, 14, 1 }, { 35, 45, 15, 5 }, { 30, 45, 20, 5 }, { 25, 40, 30, 5 } };
    #endregion
    public Vector2 inputVector;
    public GameObject LevelUpManager;
    public Rigidbody2D rb;
    public bool alive = true;

    public bool canMove = true;
    public bool canAttack = true;

    public bool isMoving => rb?.velocity != Vector2.zero;

    public bool autoAim = true;

    
    //===================================
    // public GameObject pauseUI;
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

    //=====================================================
    public Dictionary<KeyCode, PlayerSkill> skills = new();


    //====================================
    // 플레이어의 경험치에 변동이 생겼을 때 
    //====================================

    // public void Hp_Up()
    // {
    //     Max_Hp += 12;
    //     ChangeHp(12);
    //     Speed_Plus -= 8;
    // }
    // public void RHp_Up()
    // {
    //     Max_Hp = Mathf.Max(1, hp_max - 6);
    //     ChangeHp(Mathf.Max(-6, 1-hp_curr));
    //     Speed_Plus += 16;

    // }
    // public void Atk_Up()
    // {
    //     Atk += 4;
    //     Attack_Speed_Plus -= 10;
    // }
    // public void RAtk_Up()
    // {
    //     Atk = Mathf.Max(1, atk - 2);
    //     Attack_Speed_Plus += 20;
    // }
    // public void RRange_Up()
    // {
    //     Exp_up += 50;
    //     Range_Plus -= 20;
    // }
    // public void Range_Up()
    // {
    //     Exp_up -= 20;
    //     Range_Plus += 50;

    // }
    // public void Avoid_Up()
    // {
    //     Avoid_prob = Mathf.Min(100, avoid_prob + 15);
    //     Def -= 20;
    // }
    // public void RAvoid_Up()
    // {
    //     Avoid_prob = Mathf.Max(0, avoid_prob - 6);
    //     Def += 50;
    // }
    // public void Defense_Up()
    // {
    //     Stop_Def += 100;
    // }
    // public void Random_Stat()
    // {
    //     InitPlayer();
    //     for(int i = 1; i < level; i++)
    //     {
    //         LevelUpManager.GetComponent<LevelUpManager>().AutoLevelUp();
    //     }
    // }
    // public void Random_Statup()
    // {
    //     chooseList.RemoveAt(chooseList.Count - 1);
    //     LevelUpManager.GetComponent<LevelUpManager>().AutoLevelUp();
    // }
    // public void Bless()
    // {
    //     sanctuaryLevel++;
    // }
    // public void Dot()
    // {
    //     bleedingLevel++;
    // }
    // public void Explosion()
    // {
    //     is_explosion = true;
    //     explosionLevel++;
    // }
    // public void Protect()
    // {
    //     RunningCoroutines.Add(StartCoroutine(Protect20()));
    // }
    // IEnumerator Protect20()
    // {
    //     while (true)
    //     {
    //         playerStatus.Get_shield();
            
    //         yield return new WaitForSeconds(20f);

    //     }
    // } 
    // public void Exchange()
    // {
    //     RunningCoroutines.Add(StartCoroutine(exchange()));
    // }
    // IEnumerator exchange()
    // {
    //     while(true)
    //     {
    //         Atk++;
    //         Def = Mathf.Max(0, def - 5);
    //         yield return new WaitForSeconds(20f);
    //     }
    // }
    // public void Avoiding()
    // {
    //     Avoid_Select_Count++;
    //     Avoid_Time = Time.time;
    //     RunningCoroutines.Add(StartCoroutine(avoiding()));
    // }
    // IEnumerator avoiding()
    // {
    //     while(true)
    //     {
    //         yield return new WaitForSeconds(1f);
    //         Avoid_Atk = avoid_select_count * (int)((Time.time - avoid_time)/10);
    //     }
    // }

        // public void Attacked_Exp()
    // {
    //     Attack_exp = true;
    //     exp_amaount++;
    // }

	// public void Life_Up()
    // {
    //     Life++;
    // }

    public void ChangeExp(float value)
    {
        // Cur_Exp += value + value * exp_up / 100f;
        playerStatus.Increase_currExp(value);

        #region 레벨업
        if(exp_curr >= exp_max)
        {
            playerStatus.LevelUp();
        
            LevelUpManager.GetComponent<LevelUpManager>().LevelUp();    //수정해야함.
            GameEvent.ge.onChange_level.Invoke();
        }
        #endregion


        GameEvent.ge.onChange_exp.Invoke();
    }

    public void GetInvincible(float duration)
    {
        StartCoroutine(Invincible(duration));
    }

    IEnumerator Invincible(float time)
    {
        playerStatus.Get_invincible();
        yield return new WaitForSeconds(time);
        playerStatus.Lose_invincible();
    }



    // 스텟 초기화
    public void InitPlayer()
    {
        // 능력치 초기화
        playerStatus = new();
        
        //
        t_player = transform;
        center = t_player.Find("Center");

        //
        canMove = true;
        canAttack = true;
		//
        autoAim = true;

		// obj_directAim =GameObject.Find("Aim");
        // obj_autoAim = obj_directAim.transform.GetChild(0).gameObject;

        // ChangeAimImage();
        GameEvent.ge.onChange_aimMode.Invoke(autoAim);
        
        // 이것도 나중에 손보자
        foreach (Coroutine C in RunningCoroutines)
            StopCoroutine(C);
        RunningCoroutines.Clear();
        chooseList.Clear();


        // 스킬 초기화.
        skills = new()
        {
            {KeyCode.Space, new PlayerSkill_Dash()}
        };


        initialized =true;        

        Debug.LogWarning("플레이어 초기화 완료");
    }
    //====================================
    // 플레이어의 체력에 변동이 생겼을 때 
    //====================================
    public void ChangeHp(int value)
    {
        
        
        if (value >= 0)
        {
            playerStatus.Increase_currHp(value);
        }
        else
        {
            playerStatus.Decrease_currHp(value);
        }
        

        GameEvent.ge.onChange_hp.Invoke(value);
    }

    public void OnGet_healingItem()
    {
        ChangeHp((int) playerStatus.amount_healingItem );
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
        
        // Avoid_Time = Time.time;

        // 피해 무효화 가능 ,
        if (playerStatus.isInvincible)
        {
            return;	
        }
        if (!knockBackFlag)
        {
            StartCoroutine(Invincible(0.5f));        // 무적처리 (0.5초간 무적)
        }

        // 회피
		int prob = UnityEngine.Random.Range(1, 101);
        if (prob <= avoid_prob)
        {
            GameEvent.ge.onPlayerAvoid.Invoke(hitPoint);
            return;
        }

        // 방어 계산
        int finalDamage = 0;
        // 방어 계산
        if (isMoving)
        {
            finalDamage = (int)Mathf.Max(dmg * (100f / (100 + def)), 0);      // 공격의 경우 방어력 계산하여 피해량이 0보다 작아지면 방어하기 위해 0으로 설정 
        }
        else
        {
            finalDamage = (int)Mathf.Max(dmg * (100f / (100 + def + playerStatus.def_onStop)), 0);   // 공격의 경우 방어력 계산하여 피해량이 0보다 작아지면 방어하기 위해 0으로 설정 
        }

        // 쉴드있으면 데미지를 0으로 바꾸고 실드 잃음. 
        if(playerStatus.haveSheild)
        {
            playerStatus.Lose_shield();
            finalDamage = 0;
        }

        // 데미지 처리     
        if (finalDamage <= 0)
        {
            GameEvent.ge.onPlayerGuard.Invoke(hitPoint);
            return;
        }


        // 데미지 처리 
        if (knockBackFlag)
        {
            KnockBack(10f, hitPoint);
        }

        ChangeHp(-finalDamage);      
        //
        OnDamageEffect(hitPoint);
        //
        audioSource.PlayOneShot(sound_playerHit);

        // 피격시 경험치 획득 
		if (playerStatus.exp_amount_onHit> 0 )
            ChangeExp( playerStatus.exp_amount_onHit * exp_max *0.15f);

        


        #region 죽음
        if(hp_curr <= 0)
        {
            playerStatus.Decrease_life(1);
            if (playerStatus.life <=0)
            {
                alive = false;
                Die();
            }
            else
            {
                ChangeHp(hp_max);
            }
                
        }
        #endregion    
    }
    //==========================
    // 피격 효과
    //=========================
    public void OnDamageEffect(Vector3 hitPoint)
    {
        //effect
        Effect effect =  EffectPoolManager.instance.GetFromPool("011");
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
        // StageManager.sm.currStage.audioSource.Stop();
        audioSource.PlayOneShot( Resources.Load<AudioClip>("Sound/14_defeat"));

        //die 
        
        rb.simulated = false;
        rb.velocity = Vector3.zero;
        canMove = false;
        canAttack = false;

        GetComponent<PlayerWeapon>().hands[0].gameObject.SetActive(false);

        //게임 실패 호출
        StartCoroutine(WaitAndFinish());
    }

    public IEnumerator WaitAndFinish()
    {
        animator.SetTrigger("die");
        yield return new WaitForSeconds(2f);    // wait for death animation
        
        GameManager.gm.FinishGame(false);
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
            Effect effect = EffectPoolManager.instance.GetFromPool("101");
            effect.InitEffect(randPos);
            effect.ActionEffect();

            yield return new WaitForSeconds(30f);
        }
    }


    //================================================

    //
    //
    //
    // public void ChangeAimImage()
    // {
        // obj_directAim.SetActive(!autoAim);
        // obj_autoAim.SetActive(autoAim);
    // }


    

    //=========================================================================================================================================
  

    // Start is called before the first frame update
    void Start()
    {
        LevelUpManager = GameObject.Find("LevelupManager");
        //
        // StartCoroutine(CreateSanctuary());

        // 스테이지 시작시 이벤트 
        GameEvent.ge.onStageStart.AddListener( ()=> t_player.position = StageManager.sm.currStage.startPoint );   // 플레이어 위치 초기화
    }

    void Update()
    {
        if (!initialized)
        {
            return;
        }
            
        
        // for aim set
        if (Input.GetKeyDown(KeyCode.C))
        {
            audioSource.PlayOneShot(sound_changeAuto);

            autoAim = !autoAim;

            GameEvent.ge.onChange_aimMode.Invoke(autoAim);
        }

        // pause   
        if (Input.GetKeyDown(KeyCode.Escape) && !OnSelecting) 
        {
            GameManager.gm.PauseGame(!GameManager.gm.isPaused);   
        }
        
        
        // if (DirectingManager.dm.onDirecting )
        // {
        //     rb.velocity = Vector3.zero;
        //     animator.SetFloat("speed", 0f);
        //     return;
        // }
        
        if ( canMove )     // 움직일 수 있을 때에만 
        {
            animator.SetFloat("speed", inputVector.magnitude);
            if (inputVector.x != 0) // 스프라이트 뒤집기(입력에 따라) 
            {
                spriter.flipX = inputVector.x < 0;
            }
            

            inputVector.x = Input.GetAxisRaw("Horizontal");
            inputVector.y = Input.GetAxisRaw("Vertical");
            float final_speed = movementSpeed + movementSpeed * movementSpeed_plus *0.01f;
            rb.velocity = new Vector2(inputVector.x * final_speed, inputVector.y * final_speed);

        }

        // 스킬 사용 여부
        foreach( var kv in skills )
        {
            KeyCode keyCode = kv.Key;
            PlayerSkill skill = kv.Value;

            //
            if (Input.GetKeyDown(keyCode) )
            
                if (skill.IsAvailable()) 
                {
                    
                    Debug.LogWarning("스킬 사용! " + skill.skillName);                    
                    skill.UseSkill();
                    GameEvent.ge.onUseSkill.Invoke(keyCode);
                }
                else
                {
                    Debug.LogError("스킬 사용 불가!! " + skill.skillName);
                }
            }

        }
    
}
