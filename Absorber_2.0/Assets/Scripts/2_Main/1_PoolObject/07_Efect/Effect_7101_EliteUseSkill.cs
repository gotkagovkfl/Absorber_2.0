using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_7101_EliteUseSkill : Effect
{
    protected override void InitEssentialInfo_effect()
    {
        id_effect = "7101";
    }

    public override void InitEffect_custom(Vector3 targetPos)
    {
        lifeTime = 0.25f;
    }

    public override void ActionEffect_custom()
    {

    }
}
