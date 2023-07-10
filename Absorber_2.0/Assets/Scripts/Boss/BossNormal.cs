using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormal : Projectile_Enemy 
{
    protected override void InitEssentialInfo_enemyProj()
    {
        id_proj =  "100";
    }
    
    public override void Action()
    {
        rb.velocity = direction * speed;
    }

    public override void EnemyProjDestroy_custom()
    {
        
    }

    // void Start()
    // {
    //     // Destroy(gameObject, 5f);
    // }

}
