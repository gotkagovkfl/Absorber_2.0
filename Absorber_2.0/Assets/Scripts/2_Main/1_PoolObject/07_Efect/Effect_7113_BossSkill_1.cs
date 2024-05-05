using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_7113_BossSkill_1 : Effect
{
    protected override void InitEssentialInfo_effect()
    {
        id_effect = "7113";
    }

    public override void InitEffect_custom(Vector3 targetPos)
    {
        lifeTime = 0.4f;
    }

    public override void ActionEffect_custom()
    {

    }
}
