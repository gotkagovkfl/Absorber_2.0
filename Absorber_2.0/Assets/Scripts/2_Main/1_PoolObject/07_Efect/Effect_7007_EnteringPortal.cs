using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_7007_EnteringPortal : Effect
{
    protected override void InitEssentialInfo_effect()
    {
        id_effect = "7007";
    }

    public override void InitEffect_custom(Vector3 targetPos)
    {
        lifeTime = 2f;
    }

    public override void ActionEffect_custom()
    {

    }
}
