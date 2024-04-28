using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;            //텍스트매쉬프로 




public class SomethingPoolManager : PoolManager<Something>
{
    public static SomethingPoolManager spm;


    //========================================================================================
    protected override void SetCategory()
    {
        id_category = "04";
    }
    
    
    protected override void Awake()
    {
        base.Awake();
        spm = this;
    }

    void Start()
    {
        // 스테이지 시작시 이벤트
        GameEvent.ge.onStageStart.AddListener( CreateStageSFX );     // 특수효과 생성
        
        // 스테이지 클리어 시 이벤트
        GameEvent.ge.onStageClear.AddListener( CreatePortal );       // 포탈생성

        //
        GameEvent.ge.onChange_level.AddListener(OnLevelUp );


    }
    //============================================================================================


    //=========================== get & take ==================
    // Pool한 obj 별 초기화    
    public override void GetFromPool_custom(Something something)
    {
        // proj.active = true;
        // proj.isAlive = true;
    }

    //=========================================================
    // 풀에 반납
    //========================================================
    public override void TakeToPool_custom(Something something)
    {
        
    }

    //===========================================================================================================================================

    //==========================================  텍스트  ====================================

    //========================================
    // 텍스트를 화면에 생성한다. ( 생성 위치, 텍스트 내용(값), 색깔, 타입번호 )
    // //========================================    
    public void CreateText(Vector3 pos, string value, Color color, int typeNum)
    {       
        // id 000은 텍스트
        Something something = GetFromPool("000");

        Something_0000_valueText text   = something.GetComponent<Something_0000_valueText>();
        TextMeshPro tmp                 = something.GetComponent<TextMeshPro>();
        
        VertexGradient textGradient = tmp.colorGradient;
        tmp.color = Color.white;
        textGradient.bottomLeft = Color.white;
        textGradient.bottomRight = Color.white;
        //
        something.InitSomething(pos);
        text.typeNum = typeNum;
        something.ActionSomething();

        tmp.text = value;
        
        // set color
        switch(typeNum)
        {
            case 0:             // 일반 텍스트(일반공격 포함)
            case 1:             // 도트 공격 
            case 2:  
                textGradient.bottomLeft = color;
                textGradient.bottomRight = color;
                break;      
            case 3:
                break;
        }
        textGradient.topLeft = color;
        textGradient.topRight= color;    
        tmp.colorGradient = textGradient;
    }
    //======================================================================================================
    //=======================
    // something 생성
    //======================
    public Something CreateSomething(string id, Vector3 pos)
    {
        Something something = GetFromPool(id);

        something.InitSomething(pos);
        something.ActionSomething();

        return something;
    }

    //=================================== 피격 이펙트========================================
    // //========================================
    // // hit 이펙트 생성
    // //========================================
    // public void CreateHitEffect(Vector3 hitPoint)
    // {
    //     Something hitEffect = GetFromPool("0200");      

    //     hitEffect.InitSomething(hitPoint);
    //     hitEffect.ActionSomething();
    // }

    //==================================== 이벤트 전달용 ==================================================
    //====================================
    // 다음 단계로 넘어갈 수 있는 포탈을 생성한다.  - 플레이어 위치와 겹치지 않게 하거나, 생성후 일정 시간동안은 비활성화 하여 바로 충돌 이벤트 발생하는 거 막아야함. 
    //====================================
    public void CreatePortal()
    {
        // 포탈 오브젝트 생성
        Something portal = GetFromPool("0001");

        portal.InitSomething( Vector3.zero );   // 원래 위치도 따로 설정해줘야함
        portal.ActionSomething();
    }

    //===================================
    // 스테이지 분위기에 맞는 특수효과를 생성한다./ 원래는 스테이지마다 달라야하는데 2.0 버전까지는 먼지로 통일 
    //===================================
    public void CreateStageSFX()
    {
        Something currStageEffect = GetFromPool("6100");

        currStageEffect.InitSomething( Vector3.zero );
        currStageEffect.ActionSomething();
    }


    void OnLevelUp()
    {
        Effect effect = EffectPoolManager.epm.GetFromPool("006");
        effect.InitEffect(Player.player.center.position);
        effect.ActionEffect();
    }
}
