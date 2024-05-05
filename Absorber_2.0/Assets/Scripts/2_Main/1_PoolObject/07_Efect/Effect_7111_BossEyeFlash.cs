using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_7111_BossEyeFlash : Effect
{
    protected override void InitEssentialInfo_effect()
    {
        id_effect = "7111";
    }

    public override void InitEffect_custom(Vector3 targetPos)
    {
        lifeTime = 0.4f;
    }

    public override void ActionEffect_custom()
    {

    }


    

    //
    void Update()
    {
        if (targetToFollow!=null)
        {
            t_effect.position = targetToFollow.position;
        }
        
    }

}
