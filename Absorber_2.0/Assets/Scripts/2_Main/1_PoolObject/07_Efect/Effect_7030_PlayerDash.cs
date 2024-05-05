using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_7030_PlayerDash : Effect
{
    protected override void InitEssentialInfo_effect()
    {
        id_effect = "7030";
    }

    public override void InitEffect_custom(Vector3 targetPos)
    {
        lifeTime = 0.33f;
    }

    public override void ActionEffect_custom()
    {

    }
}
