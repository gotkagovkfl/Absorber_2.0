using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_7100_EnemyDash : Effect
{
    protected override void InitEssentialInfo_effect()
    {
        id_effect = "7100";
    }

    public override void InitEffect_custom(Vector3 targetPos)
    {
         pos = targetPos;
        offset = Vector3.zero;
 
        speed = 0f;
        lifeTime = 0.27f;
    }

    public override void ActionEffect_custom()
    {

    }
}
