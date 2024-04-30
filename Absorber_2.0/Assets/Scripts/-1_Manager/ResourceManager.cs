using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class ResourceManager 
{
    // public static ResourceManager rm;

    public static bool initialized;
    
    // -----------------texts : 나중에 scriptableobject로 바꿔보자 ---------------------------------------------------------
    public static Dictionary<string, TextAsset> dic_data = new Dictionary<string, TextAsset>();
    static string dir_data = "04_Data";
    // --------------------sounds--------------------------------------------------------------------------------------------
    public static Dictionary<string, AudioClip> dic_sounds = new Dictionary<string, AudioClip>();
    static string dir_sounds = "03_Sounds";

    // -------------------prite - 픽토그램에만 사용될듯 -------------------------------------------------------------------------------
    // public Dictionary<string, Dictionary<string, Sprite>> dic_images = new Dictionary<string, Dictionary<string, Sprite>>();
    
    static string dir_pictogram = "00_Images/Pictograms";
    public static Dictionary<string, Sprite> dic_pictograms = new();

    // ---------------------- skillIcons --------------------------------------------------------------------------------------------------------
    public static Dictionary<string, Sprite> dic_skillIcons = new();
    

    //======================================================================================================================================================

    

    public static void Init()
    {
        // Singleton
        Debug.Log("리소스 매니저 초기화합니다..");
        LoadResources();
    }


    //===================
    // 리소스 불러오는 작업 : 여기서 로딩화면 띄울 예정 ****************************
    //===================
    static void LoadResources()
    {
        Debug.Log("리소스 로드 진행");
    
        LoadResources_pictograms();
        LoadResources_skillIcons();

        // 리소스들 전부 로드한 후에 succes => true 하여 다른 풀매니저에서 접근 가능하게
        initialized = true;
    }



    static void LoadResources_pictograms()
    {

    }

    static void LoadResources_skillIcons()
    {
        string path = "00_Images/SkillIcon";
        foreach( var sprite in Resources.LoadAll<Sprite>(path))
        {
            string name= sprite.name;

            string id = name.Split("_")[0];

            dic_skillIcons.Add(id, sprite);
        }
    }

    //=====================================================================================


    public static Sprite GetSkillIcon(string id)
    {
        Sprite ret = null;

        if (dic_skillIcons.TryGetValue(id, out ret))
        {

        }
        return ret;

    }
}
