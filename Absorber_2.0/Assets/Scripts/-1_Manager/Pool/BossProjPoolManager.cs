using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjPoolManager : PoolManager<Projectile_Enemy>
{
    public static BossProjPoolManager bppm;
    
    protected override void Awake()
    {
        base.Awake();

        bppm = this;
    }
    
    protected override void SetCategory()
    {
        id_category = "03";
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
