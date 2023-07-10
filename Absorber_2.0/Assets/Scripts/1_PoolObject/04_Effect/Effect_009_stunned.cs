using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_009_stunned : Effect
{

    protected override void InitEssentialInfo_effect()
    {
        id_effect = "009";
    }

    // 개별 능력치 초기화 
    public override void InitEffect_custom(Vector3 targetPos)
    {        
        pos = targetPos;
        offset = Vector3.zero;
 
        speed = 0f;
        lifeTime = 5;
    }
    
    // 개별 초기화 
    public override void ActionEffect_custom()
    {
        offset = new Vector3 ( 0, target.position.y - pos.y);
        // rb.velocity = dir * speed;
    }

    void Update()
    {
        myTransform.position = target.position  + offset;
        if (enemy_d.isDead)
        {
            lifeTime=-1;
            readyDestroy = true;
            StartCoroutine(EffectDestroy());
        }
    }
}
