using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_7021_Bleed : Effect
{
        protected override void InitEssentialInfo_effect()
    {
        id_effect = "7021";
    }

    public override void InitEffect_custom(Vector3 targetPos)
    {
        pos = targetPos;
        offset = new Vector3( Random.Range(-0.7f,0.7f), Random.Range(-0.7f,0.7f));;
 
        speed = 0f;
        lifeTime = 0.25f;
    }

    public override void ActionEffect_custom()
    {

    }
}
