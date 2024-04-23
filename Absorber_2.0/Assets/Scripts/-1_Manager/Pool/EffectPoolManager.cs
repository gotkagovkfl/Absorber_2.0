using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using TMPro;            //텍스트매쉬프로 



public class EffectPoolManager : PoolManager<Effect>
{
    public static EffectPoolManager epm;
    
    public enum valueType {dmg_e_n, dmg_e_c, dmg_,e}

    

    //========================================================================================
    protected override void SetCategory()
    {
        id_category = "04";
    }
    
    
    
    // public override void SetDir()
    // {
    //     dir = "Prefabs/W/Effects";
    // }

    
    protected override void Awake()
    {
        base.Awake();
        epm = this;
    }

    void Start()
    {
        // 스테이지 시작시 이벤트
        GameEvent.onStageStart.AddListener( CreateStageSFX );     // 특수효과 생성
        
        // 스테이지 클리어 시 이벤트
        GameEvent.onStageClear.AddListener( CreatePortal );       // 포탈생성


    }
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
    //========================================
    // 텍스트를 화면에 생성한다. ( 생성 위치, 텍스트 내용(값), 색깔 )
    // //========================================    
    // public void CreateText(Vector3 pos, string value, Color color)
    // {       
    //     // id 000은 텍스트
    //     Effect effect = GetFromPool("000");
    //     effect.InitEffect(pos);
    //     effect.ActionEffect();

    //     TextMeshPro tmp = effect.GetComponent<TextMeshPro>();
    //     tmp.text = value;
        
    //     tmp.color = color;
    // }


    //========================================
    // 텍스트를 화면에 생성한다. ( 생성 위치, 텍스트 내용(값), 색깔, 타입번호 )
    // //========================================    
    public void CreateText(Vector3 pos, string value, Color color, int typeNum)
    {       
        // id 000은 텍스트
        Effect effect = GetFromPool("000");

        Effect_000_valueText text   = effect.GetComponent<Effect_000_valueText>();
        TextMeshPro tmp             = effect.GetComponent<TextMeshPro>();
        
        VertexGradient textGradient = tmp.colorGradient;
        tmp.color = Color.white;
        textGradient.bottomLeft = Color.white;
        textGradient.bottomRight = Color.white;
        
        effect.InitEffect(pos);
        text.typeNum = typeNum;
        effect.ActionEffect();

        tmp.text = value;
        
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





    
    //=============================================================  텍스트  ===============================================================
    //========================================
    // 값 생성 - 개체의 체력에 변동이 생겼을 때 그 값을 화면에 출력한다.
    // //========================================
    // public void CreateValueText(Transform target, float value)
    // {                 
    //     // 풀에서 생성
    //     Effect text = GetFromPool("000");
    //     // 초기화 후 작동
    //     text.InitEffect(target.position);
    //     text.ActionEffect();
        
    //     //
    //     TextMeshPro tmp = text.GetComponent<TextMeshPro>();

    //     // 텍스트에 값 작성 (절대값으로) - 삼항연산자가 Mathf.abs보다 성능이 좋음
    //     tmp.text = ((value >= 0) ? value : -value).ToString();

    //     // 텍스트 발생하는 대상에 따라 설정 ( 색깔, 애니메이션 등 )
    //     if (target.CompareTag("Player"))    // 플레이어에게 발생시 
    //     {
    //         tmp.color = (value<0)? Color.red: Color.green;             // 음수(피해)는 red, 양수(회복)는 green
    //     }
    //     else    // 나머지 (적, 보스, 구조물 등)
    //     {
    //         tmp.color = Color.white;
    //     } 
 
    // }

    // //========================================
    // // 글씨 생성 - 수치로 표시할 수 없는 문자열을 표시할 때 사용한다. 
    // //========================================
    // public void CreateText(GameObject target, string content)
    // {
    //     // 풀에서 생성
    //     Effect text = GetFromPool("000");
    //     // 초기화 후 작동
    //     text.InitEffect(target.transform.position);
    //     text.ActionEffect();
        
    //     //
    //     TextMeshPro tmp = text.GetComponent<TextMeshPro>();
    //     tmp.text = content;
    //     tmp.color = new Color(200,200,200);

    // }

    //============================================================= 피격 이펙트============================================================
    //========================================
    // hit 이펙트 생성
    //========================================
    public void CreateHitEffect(Vector3 hitPoint)
    {
        Effect hitEffect = GetFromPool("001");       // <- 001~ 003 입력하여 이펙트 교체 

        hitEffect.InitEffect(hitPoint);
        hitEffect.ActionEffect();
    }

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
}
