using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_7010_EnemyHit : Effect
{
    protected override void InitEssentialInfo_effect()
    {
        id_effect = "7010";
    }

    public override void InitEffect_custom(Vector3 targetPos)
    {
        lifeTime = 0.5f;
    }

    public override void ActionEffect_custom()
    {

    }
}
