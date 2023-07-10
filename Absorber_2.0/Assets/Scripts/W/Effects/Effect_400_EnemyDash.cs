using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_400_EnemyDash : Effect
{
    protected override void InitEssentialInfo_effect()
    {
        id_effect = "400";
    }

    // 개별 능력치 초기화 
    public override void InitEffect_custom(Vector3 targetPos)
    {        
        pos = targetPos;
        offset = Vector3.zero;
 
        speed = 0f;
        lifeTime = 0.27f;
    }
    
    // 개별 초기화 
    public override void ActionEffect_custom()
    {
        // rb.velocity = dir * speed;
    }
}
