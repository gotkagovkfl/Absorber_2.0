using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_001_hitEffect : Effect
{
    protected override void InitEssentialInfo_effect()
    {
        id_effect = "001";
    }

    // 개별 능력치 초기화 
    public override void InitEffect_custom(Vector3 targetPos)
    {
        pos = targetPos;
        // dir =  new Vector3(Random.Range(-0.2f,0.2f), 0.5f, 0).normalized;
        
        // float newX = Random.Range(-0.5f,0.5f);
        // float newY = Random.Range(0.45f, 0.6f);
        // offset = new Vector3(newX, newY, 0);
        offset = Vector3.zero;
 
        speed = 0f;
        lifeTime = 0.5f;
    }

    // 개별 초기화 
    public override void ActionEffect_custom()
    {
        audioSource.PlayOneShot( audioSource.clip);
    }
}
