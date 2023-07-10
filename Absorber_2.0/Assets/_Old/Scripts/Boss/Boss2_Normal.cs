using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_Normal : Projectile_Enemy
{
    protected override void InitEssentialInfo_enemyProj()
    {
        id_proj = "200";
    }

    public override void Action()
    {
        rb.velocity = direction * speed;
    }

    public override void EnemyProjDestroy_custom()
    {

    }
}
