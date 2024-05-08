using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup_LevelUp : MonoBehaviour
{
    [SerializeField] GameObject prefab_levelUpOption;
    [SerializeField] Transform t_layout;


    // 행운테이블 등급 별 확률 (누적 확률)
    int[,] probTable =  { { 60, 90, 99, 100}, { 50, 85, 99, 100}, { 35, 80, 95, 100 }, { 25, 70, 90, 100 }, { 20, 60, 90, 100 } };

    Dictionary<int, List<ILevelUpOption>> options= new();


    //================================================================================================
    public void InitPopup()
    {
        DestroyOptions();

        options = new()
        {
            {1, new List<ILevelUpOption>{   
                new LevelUpOption_100(),
                new LevelUpOption_101(),
                new LevelUpOption_102(),
                new LevelUpOption_103(),
                new LevelUpOption_104(),
                new LevelUpOption_105(),
                new LevelUpOption_106(),
                new LevelUpOption_107(),
                new LevelUpOption_108(),
                new LevelUpOption_109(),
             } },
            {2, new List<ILevelUpOption>{   
                new LevelUpOption_200(),
                new LevelUpOption_201(),
                new LevelUpOption_202(),
                new LevelUpOption_203(),
                new LevelUpOption_204(),
                new LevelUpOption_205(),
                new LevelUpOption_206(),
                new LevelUpOption_207(),
                new LevelUpOption_208(),
                new LevelUpOption_209(),
                new LevelUpOption_210(),
                new LevelUpOption_211(),
                new LevelUpOption_212(),
                new LevelUpOption_213(),
                new LevelUpOption_214(),
                new LevelUpOption_215(),
                new LevelUpOption_216(),
                } },
            {3, new List<ILevelUpOption>{   
                new LevelUpOption_300(),
                new LevelUpOption_301(),
                new LevelUpOption_302(),
                new LevelUpOption_303(),
                new LevelUpOption_304(),
                new LevelUpOption_305(),
                new LevelUpOption_306(),
                new LevelUpOption_307(),
                new LevelUpOption_308(),
                new LevelUpOption_309(),
                new LevelUpOption_310(),
                new LevelUpOption_311(),
                new LevelUpOption_312(),
                new LevelUpOption_313(),
            } },
            {4, new List<ILevelUpOption>{   
                new LevelUpOption_400(),
                new LevelUpOption_401(),
                new LevelUpOption_402(),
                new LevelUpOption_403(),
                new LevelUpOption_404(),
                new LevelUpOption_405(),
                new LevelUpOption_406(),
                new LevelUpOption_407(),
                new LevelUpOption_408(),
                new LevelUpOption_409(),
                new LevelUpOption_410(),
                } }
        };


        GameEvent.ge.onChange_level.AddListener(OnLevelUp);
    }

    void OpenPopup()
    {
        gameObject.SetActive(true);
    }

    void ClosePopup()
    {
        gameObject.SetActive(false);
    }


    //==========================================================
    void DestroyOptions()
    {
        for(int i=0;i< t_layout.childCount;i++)
        {
            Destroy(t_layout.GetChild(i).gameObject);
        }
    }
    


    //=======================
    // 레벨업시 
    //==========================
    void OnLevelUp()
    {
        
        // 기존 옵션 파괴 
        DestroyOptions();
        
        // 선택 가능 옵션 구하기
        List<ILevelUpOption> options = GetOptions(3);

        // 해당 옵션 생성
        foreach(var option in options)
        {
            UI_LevelUpOption op = Instantiate(prefab_levelUpOption, t_layout).GetComponent<UI_LevelUpOption>();
            op.Init(option);

            op.GetComponent<Button>().onClick.AddListener(ClosePopup);      // 각 선택지에 팝업 닫기 달아놓기
        }        
        


        OpenPopup();
    }


    //==========================
    // 발동시 플레이어 행운레벨로 행운테이블을 읽어서, 랜덤 옵션을 뽑는다.
    //=========================
    List<ILevelUpOption> GetOptions(int num)
    {
        
        List<ILevelUpOption> ret = new();

        int luck = Player.player.luck;

        Debug.Log(" --------------- 레벨업 옵션 ---------------------");
        for(int i=0;i<num;i++)
        {
            int rand = UnityEngine.Random.Range(1,101);
            int grade = 1;

            // 등급 선택
            if (rand > probTable[luck, 3])
            {
                grade = 4;            
            }
            else if (rand > probTable[luck, 2])
            {
                grade = 3;
            }
            else if (rand > probTable[luck, 1])
            {
                grade = 2;
            }



            var currOptions = options[grade];
            
            int randIdx = UnityEngine.Random.Range(0, currOptions.Count);
            
            ILevelUpOption option = currOptions[randIdx];
            ret.Add( option);
            Debug.Log(option);
        }



       Debug.Log("--------------------------");

        return ret;
    }
    

}
