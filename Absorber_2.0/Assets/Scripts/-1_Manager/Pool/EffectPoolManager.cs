using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using TMPro;            //텍스트매쉬프로 



public class EffectPoolManager : PoolManager<Effect>
{
    public static EffectPoolManager instance;
    
    

    //========================================================================================
    protected override void Init_custom()
    {
        id_category = PoolType.effect;
        
        instance = this;


        // 스테이지 시작시 이벤트
        GameEvent.ge.onStageStart.AddListener( CreateStageSFX );     // 특수효과 생성
        
        // 스테이지 클리어 시 이벤트
        GameEvent.ge.onStageClear.AddListener( CreatePortal );       // 포탈생성


        //player
        GameEvent.ge.onChange_level.AddListener(OnLevelUp );
        GameEvent.ge.onChange_hp.AddListener(OnPlayerChangeHp);
        GameEvent.ge.onPlayerGuard.AddListener(OnPlayerGuard);
        GameEvent.ge.onPlayerAvoid.AddListener(OnPlayerAvoid);


        //
        GameEvent.ge.onEnemyHit.AddListener( OnEnemyHit );
        GameEvent.ge.onEnemyBleeding.AddListener( OnEnemyBleeding );
        GameEvent.ge.onEnemyheal.AddListener( OnEnemyHeal );
        GameEvent.ge.onEnemyStunned.AddListener( OnEnemyStunned );
    }
    
    
    
    // public override void SetDir()
    // {
    //     dir = "Prefabs/W/Effects";
    // }

    
    // protected override void Awake()
    // {
    //     base.Awake();
    //     epm = this;
    // }

    // void Start()
    // {



    // }
    //============================================================================================

    
    // // 투사체 풀링 사전에 추가 : id 중복되면 안됨 ㅋ
    // public override string GetId(Effect effect)
    // {
    //     return effect.id_effect;
    // }

    //=========================== get & take ==================
    // Pool한 obj 별 초기화    
    public override void GetFromPool_custom(Effect effect)
    {
        // proj.active = true;
        // proj.isAlive = true;
    }

    //=========================================================
    // 풀에 반납
    //========================================================
    public override void TakeToPool_custom(Effect effect)
    {
        
    }

    //============================================================ 생성 부분 ==================================================================================

    //=========================================================
    // 풀레이어 체력 변경시
    //========================================================
    void OnPlayerChangeHp(int value)
    {
        Vector3 pos = Player.player.center.position;
        Color color =  Color.red;        

        // 힐되는 경우엔 치유 이펙트
        if (value >=0)
        {
            Effect healEffect =GetFromPool("003");
            healEffect.InitEffect(pos);
            healEffect.ActionEffect();

            color= Color.green ;
        }

        // -------- 텍스트 ------------------------
        var effect = GetFromPool("7000").GetComponent<Effect_7000_Text>();
        effect.InitEffect(Player.player.center.position);
        effect.SetText(2, Math.Abs(value).ToString(), color);
        effect.ActionEffect();


    }

    //=========================================================
    // 풀레이어 방어시
    //========================================================

    void OnPlayerGuard(Vector3 pos)
    {
        var effect = GetFromPool("7000").GetComponent<Effect_7000_Text>();
        effect.InitEffect(pos);
        effect.SetText(0, "GUARD", Color.gray);
        effect.ActionEffect();
    }
    //=========================================================
    // 풀레이어 회피시
    //========================================================

    void OnPlayerAvoid(Vector3 pos)
    {
        var effect = GetFromPool("7000").GetComponent<Effect_7000_Text>();
        effect.InitEffect(pos);
        effect.SetText(0, "MISS", Color.gray);
        effect.ActionEffect();
    }


    //=========================================================
    // 적 피격시
    //========================================================

    void OnEnemyHit(Vector3 pos, int dmg, int level)
    {
        // ---------텍스트 세팅---------------
        Color color = new Color( 1.0f , 1.0f * (1 - level*0.25f) , (level==0)?1.0f:0.5f , 1.0f );             //255,255,255 / 255, 255, 0 / 255, 200, 0
        int tn = level>0?3:0;
        string value = dmg.ToString();
        
        var effect = GetFromPool("7000").GetComponent<Effect_7000_Text>();
        effect.InitEffect(pos);
        effect.SetText(tn,value, color);
        effect.ActionEffect();


        // -------피격 이펙트 세팅 -----------
        Effect hitEffect = GetFromPool("7010");      
        hitEffect.InitEffect(pos);
        hitEffect.ActionEffect();
    }

    //=========================================================
    // 적 출혈시
    //========================================================

    void OnEnemyBleeding(Vector3 pos, int dmg)
    {
        // ---------텍스트 세팅---------------
        string value = dmg.ToString();
        Color color = new Color(0.9f,0.5f,0.5f,1);


        var effect = GetFromPool("7000").GetComponent<Effect_7000_Text>();
        effect.InitEffect(pos);
        effect.SetText(1,value, color);
        effect.ActionEffect();

       // -------출혈 이펙트 세팅 -----------
        Effect bleedingEffect = GetFromPool("7021");
        bleedingEffect.InitEffect(pos);
        bleedingEffect.ActionEffect();
    }


    //=========================================================
    // 적 회복시
    //========================================================

    void OnEnemyHeal(Vector3 pos, int heal)
    {
        // ---------텍스트 세팅---------------
        string value = heal.ToString();
        Color color = new Color(0.2f, 0.4f, 0.1f, 1.0f);


        var effect = GetFromPool("7000").GetComponent<Effect_7000_Text>();
        effect.InitEffect(pos);
        effect.SetText(2,value, color);
        effect.ActionEffect();

       // -------힐 이펙트 세팅 -----------
        Effect healEffect = GetFromPool("7011");
        healEffect.InitEffect(pos);
        healEffect.ActionEffect();
    }


    //=========================================================
    // 적 기절시
    //========================================================

    void OnEnemyStunned(Enemy e)
    {
        // -------- 텍스트 -------------
        var effect = GetFromPool("7000").GetComponent<Effect_7000_Text>();
        effect.InitEffect(e.center.position);
        effect.SetText(2,"STUNNED", Color.gray);
        effect.ActionEffect();

        // ----- 빙글빙글 ----------
        Effect sEffect = GetFromPool("7020");
        sEffect.InitEffect(e.myTransform.position);
        sEffect.SetTarget(e.center);
        sEffect.SetDependency(e);
        sEffect.ActionEffect();
    }

    //============================================================= 피격 이펙트============================================================

    //===========================================================================================
    //====================================
    // 다음 단계로 넘어갈 수 있는 포탈을 생성한다.  - 플레이어 위치와 겹치지 않게 하거나, 생성후 일정 시간동안은 비활성화 하여 바로 충돌 이벤트 발생하는 거 막아야함. 
    //====================================
    public void CreatePortal()
    {
        // 포탈 오브젝트 생성
        // portal = Instantiate(portal_prefab, Vector3.zero, Quaternion.identity);
    }

    //===================================
    // 스테이지 분위기에 맞는 특수효과를 생성한다./ 원래는 스테이지마다 달라야하는데 2.0 버전까지는 먼지로 통일 
    //===================================
    public void CreateStageSFX()
    {
        Effect currStageEffect = GetFromPool("999");

        currStageEffect.InitEffect( Vector3.zero);
        currStageEffect.ActionEffect();
    }
    

    void OnLevelUp()
    {
        // Effect effect = EffectPoolManager.epm.GetFromPool("006");
        // effect.InitEffect(Player.player.center.position);
        // effect.ActionEffect();
    }


}