using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_7006_PlayerLevelUp : Effect
{   protected override void InitEssentialInfo_effect()
    {
        id_effect = "7006";
    }

    public override void InitEffect_custom(Vector3 targetPos)
    {
        lifeTime = 2f;
    }

    public override void ActionEffect_custom()
    {

    }
}
