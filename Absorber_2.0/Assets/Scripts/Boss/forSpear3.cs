using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forSpear3 : Projectile_Enemy
{
    Animator animator;

    public override void InitEssentialProjInfo()
    {
        id_proj = "222";
    }

    public override void Action()
    {

    }


    public override void EnemyProjDestroy_custom()
    {
        //animator.SetTrigger("default");
    }

}
