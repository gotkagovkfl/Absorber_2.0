using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forSpear : Projectile_Enemy
{
    Animator animator;

    protected override void InitEssentialInfo_enemyProj()
    {
        id_proj = "220";
    }


    public override void Action()
    {

    }


    public override void EnemyProjDestroy_custom()
    {
        //animator.SetTrigger("default");
    }

}