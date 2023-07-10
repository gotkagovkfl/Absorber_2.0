using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePoolManager :  PoolManager<Stage>
{
    public static StagePoolManager spm;
    
    protected override void Awake()
    {
        base.Awake();
        spm = this;
    }
    
    
    //========================================================================================================================
    
    protected override void SetCategory()
    {
        id_category = "06";
    }


    public override void GetFromPool_custom(Stage obj)
    {
        
    }

    public override void TakeToPool_custom(Stage obj)
    {
        
    }

    //===================================================================================================

    public string GetRandomStageId()
    {
        Dictionary<string, GameObject> dic_stages = ResourceManager.rm.GetDic_prefab(id_category);

        int num = 1; // 원래 랜덤으로 할예정( dic.Count ). 근데 스테이지 수가 적어서 1
        string ret = "000";
        // 스테이지 사전을 훑어 듀토리얼이 아닌 스테이지 하나 뽑아내기
        foreach(var i in dic_stages)
        {
            if (!i.Key.Equals("000"))
            {
                if (--num ==0)
                {
                    ret = i.Key;
                    break;
                }
            }
        }
        return ret;
    }


}
