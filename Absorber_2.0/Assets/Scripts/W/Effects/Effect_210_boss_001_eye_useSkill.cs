using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_210_boss_001_eye_useSkill : Effect
{   
    protected override void InitEssentialInfo_effect()
    {
        id_effect = "210";
    }

    // 개별 능력치 초기화 
    public override void InitEffect_custom(Vector3 targetPos)
    {
        pos = targetPos;

        offset = Vector3.zero;
 
        speed = 0f;
        lifeTime = 0.4f;
    }

    // 개별 초기화 
    public override void ActionEffect_custom()
    {
        // rb.velocity = dir * speed;
    }

    //
    void Update()
    {
        if (target!=null)
        {
            myTransform.position = target.position;
        }
        
    }
}
