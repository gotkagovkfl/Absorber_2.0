using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class StageUI : MonoBehaviour
{

    public static StageUI su;
    
    public GameObject stageClearUI;
    public GameObject stageText0;
    public GameObject stageText1;

    public TextMeshProUGUI text_stageText0;
    public TextMeshProUGUI text_stageText1;

    // public Button btn_GotoLobby;
    
    //================================================================================================================================
    void Awake()
    {
        su = this;

        stageClearUI = GameObject.Find("Canvas").transform.Find("StageUI").gameObject;

        stageText0     = stageClearUI.transform.Find("Text_StageClear").gameObject;
        stageText1     = stageClearUI.transform.Find("Text_StageClearTime").gameObject;

        text_stageText0       = stageText0.GetComponent<TextMeshProUGUI>();
        text_stageText1     = stageText1.GetComponent<TextMeshProUGUI>();

        OffUI(0);
    }

    // UI 업데이트
    void LateUpdate()
    {
        // 시간 업데이트
        float stageTime_raw = StageManager.sm.currStageTimer;
        int stageTime_minutes = (int)stageTime_raw/60;
        int stageTime_seconds = (int)stageTime_raw%60;
        
        // text_gameTime.text = string.Format("{0}:{1}",gameTime_minutes,gameTime_seconds);

    }

    public IEnumerator OffUI(float time)
    {
        yield return new WaitForSeconds(time);
        stageText0.SetActive(false);
        stageText1.SetActive(false);
        
        text_stageText0.color  = Color.white;
    }

    //====================================================================================
    //  스테이지 진입시 텍스트를 출력한다. 
    //====================================================================================
    public void SetStageEnterUI()
    {
        stageText0.SetActive(true);
        stageText1.SetActive(false);
        
        string text = "";
        if (StageManager.sm.currStageNum == 0)
        {
            text ="듀토리얼";
        }
        if (StageManager.sm.currStageNum == 1)
        {
            text ="스테이지 1";
        }


        text_stageText0.text = string.Format(text, StageManager.sm.currStageNum);
        StartCoroutine(StageEnterAnimation());
        
    }

    public IEnumerator StageEnterAnimation()
    {
        Vector3 originalPos = stageText0.transform.position;
        
        Color color = Color.white;
        for (int i=0;i<10;i++)
        {
            color.a = 0.1f*i;
            text_stageText0.color  = color;
            stageText0.transform.position += Vector3.up*1.5f;
            yield return null;
            yield return null;
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        for (int i=9;i>=0;i--)
        {
            color.a = 0.1f*i;
            text_stageText0.color  = color;
            yield return null;
            yield return null;
            yield return null;
            stageText0.transform.position -= Vector3.up * 1.5f;
            
        }
        stageText0.transform.position = originalPos;
        StartCoroutine( OffUI(0f) );
    }




    //====================================================================================
    //  게임 결과를 설정한다. 
    //====================================================================================
    public void SetStageClearUI()
    {
        text_stageText0.text = string.Format("스테이지 {0} 클리어!", StageManager.sm.currStageNum);

        // 시간 업데이트
        float stageTime_raw = StageManager.sm.currStageTimer;
        int stageTime_minutes = (int)stageTime_raw/60;
        int stageTime_seconds = (int)stageTime_raw%60;

        string seconds="";
        if (stageTime_seconds< 10)
        {
            seconds= "0";
        }
        seconds+=stageTime_seconds.ToString();

        text_stageText1.text = string.Format("클리어 시간 {0}:{1}",stageTime_minutes, seconds);

    }


    //====================================================================================
    //  스테이지 클리어 UI를 보여준다
    //==================================================================================== 
    public void ShowStageClearUI()
    {
        stageText0.SetActive(true);
        stageText1.SetActive(true);

        SetStageClearUI();
    }


    //====================================================================================
    //  스테이지 클리어 UI를 닫는다.
    //==================================================================================== 
    public void CloseStageClearUI()
    {
        stageText0.SetActive(false);
        stageText1.SetActive(false);
    }
}
