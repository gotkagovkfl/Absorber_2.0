using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_7011_EnemyHeal : Effect
{
    protected override void InitEssentialInfo_effect()
    {
        id_effect = "7011";
    }

    public override void InitEffect_custom(Vector3 targetPos)
    {
        lifeTime = 1f;
    }

    public override void ActionEffect_custom()
    {

    }
}
