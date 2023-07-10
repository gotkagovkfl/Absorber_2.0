using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWarning4 : Projectile_Enemy
{

    Animator animator;

    protected override void InitEssentialInfo_enemyProj()
    {
        id_proj = "213";
    }

    public override void Action()
    {

    }

    public override void EnemyProjDestroy_custom()
    {

    }
}
