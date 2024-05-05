using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_7005_PlayerGetExp : Effect
{
    protected override void InitEssentialInfo_effect()
    {
        id_effect = "7005";
    }

    public override void InitEffect_custom(Vector3 targetPos)
    {
         pos = targetPos;
        offset = Vector3.zero;
 
        speed = 0f;
        lifeTime = 1f;
    }

    public override void ActionEffect_custom()
    {

    }
}
