using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_005_amethstEffect : Effect
{

    protected override void InitEssentialInfo_effect()
    {
        id_effect = "005";
    }

    // 개별 능력치 초기화 
    public override void InitEffect_custom(Vector3 targetPos)
    {        
        pos = targetPos;
        offset = Vector3.zero;
 
        speed = 0f;
        lifeTime = 0.3f;
    }
    
    // 개별 초기화 
    public override void ActionEffect_custom()
    {
        audioSource.PlayOneShot( audioSource.clip);
    }

}
