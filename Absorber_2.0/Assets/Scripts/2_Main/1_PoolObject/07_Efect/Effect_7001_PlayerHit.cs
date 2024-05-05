using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_7001_PlayerHit : Effect
{
    // 개별 초기화 

    protected override void InitEssentialInfo_effect()
    {
        id_effect = "7001";
    }

    public override void InitEffect_custom(Vector3 targetPos)
    {
        lifeTime = 0.5f;
    }

    public override void ActionEffect_custom()
    {

    }
}
