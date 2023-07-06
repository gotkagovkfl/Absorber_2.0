using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_006_LevelUp : Effect
{
    public override void InitEssentialEffectInfo()
    {
        id_effect = "006";
    }

    // 개별 능력치 초기화 
    public override void InitEffect_custom(Vector3 targetPos)
    {        
        pos = targetPos;
        offset = Vector3.zero;
 
        speed = 0f;
        lifeTime = 2;
    }
    
    // 개별 초기화 
    public override void ActionEffect_custom()
    {
        audioSource.PlayOneShot( audioSource.clip);
    }
}
