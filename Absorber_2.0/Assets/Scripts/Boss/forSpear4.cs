using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forSpear4 : Projectile_Enemy
{
    Animator animator;

    public override void InitEssentialProjInfo()
    {
        id_proj = "223";
    }

    public override void Action()
    {


    }


    public override void EnemyProjDestroy_custom()
    {
        //animator.SetTrigger("default");
    }

}
