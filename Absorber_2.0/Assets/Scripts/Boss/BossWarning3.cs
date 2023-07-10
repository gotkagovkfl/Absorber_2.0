using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWarning3 : Projectile_Enemy
{

    Animator animator;

    protected override void InitEssentialInfo_enemyProj()
    {
        id_proj = "212";
    }

    public override void Action()
    {

    }

    public override void EnemyProjDestroy_custom()
    {

    }
}
