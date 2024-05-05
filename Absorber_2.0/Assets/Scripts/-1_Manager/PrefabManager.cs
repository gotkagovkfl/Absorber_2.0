using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PoolType
{
    weapon = 0,
    proj = 1,
    enemy = 2,
    enemyProj = 3,
    something = 4,
    item =5,
    stage = 6,


    effect = 7,
}


/// <summary>
/// 게임에서 사용하는 풀링 오브젝트들 관리
/// </summary>
public static class PrefabManager 
{   
    // public static PrefabManager pm;

    public static bool initialized;

    // 프리팹 
    public static Dictionary<PoolType, Dictionary<string, GameObject>> dic_prefabs = new();

    //======================================================================================================

    //======================================================================================================
    public static void Init()
    {
        LoadPrefabs();

        initialized = true;
    }


    static void LoadPrefabs()
    {
        // 사전 초기화.
        dic_prefabs = new()
        {
            {PoolType.weapon, new()},
            {PoolType.proj,new()},
            {PoolType.enemy,new()},
            {PoolType.enemyProj,new()},
            {PoolType.item,new()},
            {PoolType.something,new()},
            {PoolType.stage,new()} ,
            {PoolType.effect,new()}
        };
        
        
        // 프리팹 경로. 
        string common = "02_Prefabs/";

        string dir_weapons      = common + "00_Weapons";       // 00
        string dir_projs        = common + "01_Projectiles";   // 01
        string dir_enemies      = common + "02_Enemies";       // 02
        string dir_enemyProjs   = common + "03_EnemyProjs";    // 03
        string dir_somethings   = common + "04_Somethings";    // 04
        string dir_items        = common + "05_DropItems";     // 05
        string dir_stages       = common + "06_Stages";        // 06
        string dir_effect       = common + "07_Effect";        // 06
    
        //
        Dictionary<PoolType, string> dirs  = new(){ {PoolType.weapon, dir_weapons},
                                                    {PoolType.proj,dir_projs},
                                                    {PoolType.enemy,dir_enemies},
                                                    {PoolType.enemyProj,dir_enemyProjs},
                                                    {PoolType.item,dir_items},
                                                    {PoolType.something,dir_somethings},
                                                    {PoolType.stage,dir_stages},
                                                    {PoolType.effect,dir_effect}
                                                     };

        // 카테고리별로 프리팹 로드
        Debug.Log("============  프리팹 로드 ============");
        foreach( var kv in dirs )
        {
            PoolType id_category = kv.Key;
            string dir = kv.Value;

            // Debug.Log(id_category);
            GameObject[] list_prefabs = Resources.LoadAll<GameObject>( dir ); // dic_prefabDir엔 해당 카테고리에 해당되는 프리팹의 주소가 담겨있다.


            Debug.Log($"---------- {id_category} -----------");
            // 개별 프리팹을 사전에 추가.
            for (int idx=0;idx<list_prefabs.Length;idx++)
            {
                GameObject prefab = list_prefabs[idx];

                IPoolObject po = prefab.GetComponent<IPoolObject>();
                if (po!=null)
                {

                    po.InitEssentialInfo();        // 개별 프리팹 id 초기화

                    if (! dic_prefabs[id_category].ContainsKey(po.GetId())) // id 중복 검사
                    {
                        dic_prefabs[id_category].Add( po.GetId(), prefab);
                        Debug.Log(prefab.name);
                    }

                } 
            }

        }


        // 
        Debug.Log("============  완료  ============");
    }


    //=============================================================
    public static GameObject GetWeapon( string id )
    {
        PoolType poolType = PoolType.weapon;
        return dic_prefabs[poolType][id];
    }


    public static GameObject GetProj(string id)
    {
        PoolType poolType = PoolType.weapon;
        return dic_prefabs[poolType][id];
    }

    public static  GameObject GetEnemy(string id)
    {
        PoolType poolType = PoolType.enemy;
        return dic_prefabs[poolType][id];
    }

    public static GameObject GetEnemyProj(string id)
    {
        PoolType poolType = PoolType.enemyProj;
        return dic_prefabs[poolType][id];
    }

    public static GameObject GetItem(string id)
    {
        PoolType poolType = PoolType.item;
        return dic_prefabs[poolType][id];
    }

    public static GameObject GetStage(string id)
    {
        PoolType poolType = PoolType.stage;
        return dic_prefabs[poolType][id];
    }

    public static GameObject GetSomething(string id)
    {
        PoolType poolType = PoolType.something;
        return dic_prefabs[poolType][id];
    }

}
