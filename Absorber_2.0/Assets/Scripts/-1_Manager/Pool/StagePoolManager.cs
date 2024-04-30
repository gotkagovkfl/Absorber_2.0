using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePoolManager :  PoolManager<Stage>
{
    public static StagePoolManager instance;
    
    // protected override void Awake()
    // {
    //     base.Awake();
    //     spm = this;
    // }
    
    
    //========================================================================================================================
    
    protected override void Init_custom()
    {
        id_category = PoolType.stage;
        instance = this;
    }


    public override void GetFromPool_custom(Stage obj)
    {
        
    }

    public override void TakeToPool_custom(Stage obj)
    {
        
    }

    //===================================================================================================




}
