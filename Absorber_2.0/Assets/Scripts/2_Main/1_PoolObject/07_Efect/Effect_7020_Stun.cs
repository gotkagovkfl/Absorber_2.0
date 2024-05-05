using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_7020_Stun : Effect
{


    protected override void InitEssentialInfo_effect()
    {
        id_effect = "7020";
    }

    public override void InitEffect_custom(Vector3 targetPos)
    {
         pos = targetPos;
        offset = Vector3.zero;
 
        speed = 0f;
        lifeTime = 5f;
    }

    public override void ActionEffect_custom()
    {
        offset = new Vector3 ( 0, targetToFollow.position.y - pos.y);
    }

    
    void Update()
    {
        if (targetToFollow !=null)
        {
            myTransform.position = targetToFollow.position  + offset;
        }
        
        if (enemy_d.isDead)
        {
            _isDead = true;
        }
    }
}
