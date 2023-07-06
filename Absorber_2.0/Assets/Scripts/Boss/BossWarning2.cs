using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWarning2 : Projectile_Enemy
{

    Animator animator;

    public override void InitEssentialProjInfo()
    {
        id_proj = "211";
    }

    public override void Action()
    {

    }


    public override void EnemyProjDestroy_custom()
    {

    }
}
