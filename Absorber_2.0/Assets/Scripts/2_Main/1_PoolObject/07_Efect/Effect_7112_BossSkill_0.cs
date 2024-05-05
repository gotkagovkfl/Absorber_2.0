using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_7112_BossSkill_0 : Effect
{
    protected override void InitEssentialInfo_effect()
    {
        id_effect = "7112";
    }

    public override void InitEffect_custom(Vector3 targetPos)
    {
        lifeTime = 0.4f;
    }

    public override void ActionEffect_custom()
    {

    }
}
