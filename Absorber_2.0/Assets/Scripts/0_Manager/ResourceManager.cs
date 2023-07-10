using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ResourceManager : MonoBehaviour
{
    public static ResourceManager rm;

    public bool isLoadSuccess = false;
    
    // -----------------texts : 나중에 scriptableobject로 바꿔보자 ---------------------------------------------------------
    public Dictionary<string, TextAsset> dic_data = new Dictionary<string, TextAsset>();
    string dir_data = "Data";
    // --------------------sounds--------------------------------------------------------------------------------------------
    public Dictionary<string, AudioClip> dic_sounds = new Dictionary<string, AudioClip>();
    string dir_sounds = "Sounds";

    // -------------------prite - 픽토그램에만 사용될듯 -------------------------------------------------------------------------------
    public Dictionary<string, Dictionary<string, Sprite>> dic_images = new Dictionary<string, Dictionary<string, Sprite>>();
    
    string dir_pictogram = "Images/Pictograms";
    Dictionary<string, Sprite> dic_pictograms = new Dictionary<string, Sprite>();

    // ----------------------prefabs --------------------------------------------------------------------------------------------------------
    Dictionary<string, string> dic_prefabDir = new Dictionary<string, string>();
    
    string dir_weapons = "Prefabs/00_Weapons";          // 00
    string dir_projs = "Prefabs/01_Projectiles";        // 01
    string dir_enemies = "Prefabs/02_Enemies";          // 02
    string dir_enemyProjs = "Prefabs/03_EnemyProjs";    // 03
    string dir_effects = "Prefabs/04_Effects";          // 04
    string dir_items = "Prefabs/05_DropItems";          // 05
    string dir_stages = "Prefabs/06_Stages";            // 06

    public Dictionary<string, Dictionary<string, GameObject>> dic_prefabs = new Dictionary<string, Dictionary<string, GameObject>>();

    
    Dictionary<string, GameObject> dic_weapons = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> dic_projs = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> dic_enemies = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> dic_enemyProjs = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> dic_effects = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> dic_items = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> dic_stages = new Dictionary<string, GameObject>();
    
    //======================================================================================================================================================

    void Awake()
    {
        // Singleton
        if (rm == null)
        {
            rm = this;     
        }
        else if (rm != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);   

        InitDic();
        LoadResources();
    }

    //===================
    // 사전 초기화 작업
    //===================
    void InitDic()
    {
        InitDic_prefabDir();
        
        //
        dic_images.Add("pictogram",dic_pictograms);
        //
        dic_prefabs.Add("00",dic_weapons);
        dic_prefabs.Add("01",dic_projs);
        dic_prefabs.Add("02",dic_enemies);
        dic_prefabs.Add("03",dic_enemyProjs);
        dic_prefabs.Add("04",dic_effects);
        dic_prefabs.Add("05",dic_items);
        dic_prefabs.Add("06",dic_stages);
    }

    //===================
    // 프리팹) 경로 사전 초기화 작업
    //===================
    void InitDic_prefabDir()
    {
        dic_prefabDir.Add("00", dir_weapons);
        dic_prefabDir.Add("01", dir_projs);
        dic_prefabDir.Add("02", dir_enemies);
        dic_prefabDir.Add("03", dir_enemyProjs);
        dic_prefabDir.Add("04", dir_effects);
        dic_prefabDir.Add("05", dir_items);
        dic_prefabDir.Add("06", dir_stages);
    }




    //===================
    // 리소스 불러오는 작업 : 여기서 로딩화면 띄울 예정 ****************************
    //===================
    void LoadResources()
    {
        LoadResources_data();
        LoadResources_sounds();
        LoadResources_images();
        LoadResources_prefabs();

        // 리소스들 전부 로드한 후에 succes => true 하여 다른 풀매니저에서 접근 가능하게
        isLoadSuccess = true;
    }

    void LoadResources_data()
    {

    }
    
    void LoadResources_sounds()
    {

    }

    void LoadResources_images()
    {

    }

    //=====================================
    // 프리팹 로드 : dic_prefabs에 프리팹들을 로드한다. 카테고리에 따라 다른 곳에 저장한다. (ex, enemy, weapon, projectile.... )
    //=====================================
    void LoadResources_prefabs()
    {
        GameObject[] list_prefabs; 

        // 카테고리별로
        foreach( var i in dic_prefabDir )
        {
            string id_category = i.Key;
            // Debug.Log(id_category);
            list_prefabs = Resources.LoadAll<GameObject>( dic_prefabDir[ id_category] ); // dic_prefabDir엔 해당 카테고리에 해당되는 프리팹의 주소가 담겨있다.

            // 개별 프리팹을 사전에 추가.
            for (int idx=0;idx<list_prefabs.Length;idx++)
            {
                GameObject prefab = list_prefabs[idx];

                IPoolObject po = prefab.GetComponent<IPoolObject>();
                if (po!=null)
                {

                    po.InitEssentialInfo();        // 개별 프리팹 id 초기화

                    if (dic_prefabs[id_category].ContainsKey(po.GetId())) // id 중복 검사
                    {
                        continue;
                    }

                    dic_prefabs[id_category].Add( po.GetId(), prefab);
                    
                } 
            }

            // Debug.Log(dic_prefabs[id_category].Count);
            // Debug.Log("----------------------");
        }

        
    }

    //=====================================================================================
    public Dictionary<string,GameObject> GetDic_prefab(string id_category)
    {
        return dic_prefabs[id_category];
    } 
    
    
    public GameObject GetFromDic_prefab(string id_category, string id)
    {
        return dic_prefabs[id_category][id];
    }


}
