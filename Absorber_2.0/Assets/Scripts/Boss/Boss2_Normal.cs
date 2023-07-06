using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_Normal : Projectile_Enemy
{
    public override void InitEssentialProjInfo()
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
