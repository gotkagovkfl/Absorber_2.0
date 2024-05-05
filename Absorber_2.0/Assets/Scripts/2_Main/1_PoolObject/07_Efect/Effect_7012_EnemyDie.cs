using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_7012_EnemyDie : Effect
{
    protected override void InitEssentialInfo_effect()
    {
        id_effect = "7012";
    }

    public override void InitEffect_custom(Vector3 targetPos)
    {
         pos = targetPos;
        offset = Vector3.zero;
 
        speed = 0f;
        lifeTime = 1.5f;
    }

    public override void ActionEffect_custom()
    {

    }
}
