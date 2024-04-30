using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjPoolManager : PoolManager<Projectile_Enemy>
{
    public static BossProjPoolManager instance;    
    
    
    protected override void Init_custom()
    {
        id_category = PoolType.enemyProj;

        instance = this;
    }

    // public override void SetDir()
    // {
    //     dir = "Prefabs/Boss";
    // }
    
    // public override string GetId(Projectile_Enemy proj)
    // {
    //     return proj.id_proj;
    // }


    //--------------------------------------------------------------------
    
    public override void GetFromPool_custom(Projectile_Enemy proj)
    {
        
    }

    public override void TakeToPool_custom(Projectile_Enemy proj)
    {
        
    }
}
