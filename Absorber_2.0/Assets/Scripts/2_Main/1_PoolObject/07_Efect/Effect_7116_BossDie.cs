using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_7116_BossDie: Effect
{
    protected override void InitEssentialInfo_effect()
    {
        id_effect = "7116";
    }

    public override void InitEffect_custom(Vector3 targetPos)
    {
        lifeTime = 4.5f;
    }

    public override void ActionEffect_custom()
    {

    }
}
