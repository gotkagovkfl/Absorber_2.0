using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStop : Projectile_Enemy
{
    protected override void InitEssentialInfo_enemyProj()
    {
        id_proj = "203";
    }

    public override void Action()
    {
        
    }

    public override void EnemyProjDestroy_custom()
    {

    }
}
