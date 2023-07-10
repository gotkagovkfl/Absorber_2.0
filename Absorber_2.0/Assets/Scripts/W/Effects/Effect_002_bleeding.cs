using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_002_bleeding : Effect
{

    protected override void InitEssentialInfo_effect()
    {
        id_effect = "102";
    }

    // 개별 능력치 초기화 
    public override void InitEffect_custom(Vector3 targetPos)
    {        
        pos = targetPos;
        offset = new Vector3( Random.Range(-0.7f,0.7f), Random.Range(-0.7f,0.7f));;
 
        speed = 0f;
        lifeTime = 0.25f;

    
    }
    
    // 개별 초기화 
    public override void ActionEffect_custom()
    {
        // rb.velocity = dir * speed;
    }
}
