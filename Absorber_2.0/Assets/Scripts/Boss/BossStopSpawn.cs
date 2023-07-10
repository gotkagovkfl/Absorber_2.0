using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStopSpawn : Projectile_Enemy
{
    protected override void InitEssentialInfo_enemyProj()
    {
        id_proj = "202";
    }

    public override void Action()
    {

    }

    public override void EnemyProjDestroy_custom()
    {

    }

}
