using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_666_eliteSkill : Effect
{
    public override void InitEssentialEffectInfo()
    {
        id_effect = "666";
    }

    // 개별 능력치 초기화 
    public override void InitEffect_custom(Vector3 targetPos)
    {        
        pos = targetPos;
        offset = Vector3.zero;
 
        speed = 0f;
        lifeTime = 0.25f;
    }
    
    // 개별 초기화 
    public override void ActionEffect_custom()
    {
        // rb.velocity = dir * speed;
    }
}
