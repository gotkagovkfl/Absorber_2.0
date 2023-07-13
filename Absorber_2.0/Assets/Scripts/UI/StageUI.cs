using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class StageUI : MonoBehaviour
{

    public static StageUI su;
    
    public GameObject stageUI;
    public GameObject stageName;
    public GameObject stageText1;

    TextMeshProUGUI text_stageTime; // 진행 경과 시간 UI
    public TextMeshProUGUI text_stageName;
    public TextMeshProUGUI text_stageText1;

    // public Button btn_GotoLobby;
    
    //================================================================================================================================
    void Awake()
    {
        su = this;

        stageUI = GameObject.Find("Canvas").transform.Find("StageUI").gameObject;

        stageName     = stageUI.transform.Find("Text_StageClear").gameObject;
        stageText1     = stageUI.transform.Find("Text_StageClearTime").gameObject;

        text_stageTime        =  stageUI.transform.Find("Text_StageTime").GetComponent<TextMeshProUGUI>();
        text_stageName       = stageName.GetComponent<TextMeshProUGUI>();
        text_stageText1     = stageText1.GetComponent<TextMeshProUGUI>();

        OffUI(0);
    }

    void Start()
    {
        StartCoroutine(UpdateTimeUI());
    }

    //=================
    // 시간 UI 업데이트
    //================
    public IEnumerator UpdateTimeUI()
    {
        while (true)
        {
            float stageTime_raw = StageManager.sm.currStageTimer;
            
            int stageTime_minutes = (int)stageTime_raw/60;
            int temp = (int)stageTime_raw%60;

            string stageTime_seconds=(temp< 10)?"0":"";
            stageTime_seconds+=stageTime_seconds.ToString();
        
            text_stageTime.text = string.Format("{0}:{1}",stageTime_minutes, stageTime_seconds);

            yield return new WaitForSeconds(1f);
        }
    }

    //==============================================================

    public IEnumerator OffUI(float time)
    {
        yield return new WaitForSeconds(time);
        stageName.SetActive(false);
        stageText1.SetActive(false);
        
        text_stageName.color  = Color.white;
    }

    //====================================================================================
    //  스테이지 진입시 텍스트를 출력한다. 
    //====================================================================================
    public void SetStageEnterUI()
    {
        stageName.SetActive(true);
        stageText1.SetActive(false);
        
        string text = StageManager.sm.currStage.name_stage;


        text_stageName.text = string.Format(text, StageManager.sm.currStage.name_stage);
        StartCoroutine(StageEnterAnimation());
        
    }

    public IEnumerator StageEnterAnimation()
    {
        Vector3 originalPos = stageName.transform.position;
        
        Color color = Color.white;
        for (int i=0;i<10;i++)
        {
            color.a = 0.1f*i;
            text_stageName.color  = color;
            stageName.transform.position += Vector3.up*1.5f;
            yield return null;
            yield return null;
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        for (int i=9;i>=0;i--)
        {
            color.a = 0.1f*i;
            text_stageName.color  = color;
            yield return null;
            yield return null;
            yield return null;
            stageName.transform.position -= Vector3.up * 1.5f;
            
        }
        stageName.transform.position = originalPos;
        StartCoroutine( OffUI(0f) );
    }




    //====================================================================================
    //  게임 결과를 설정한다. 
    //====================================================================================
    public void SetStageClearUI()
    {
        text_stageName.text = string.Format("스테이지 {0} 클리어!", StageManager.sm.currStage.name_stage);

        text_stageText1.text = string.Format("클리어 시간 {0}", text_stageTime.text);

    }


    //====================================================================================
    //  스테이지 클리어 UI를 보여준다
    //==================================================================================== 
    public void ShowStageClearUI()
    {
        stageName.SetActive(true);
        stageText1.SetActive(true);

        SetStageClearUI();
    }


    //====================================================================================
    //  스테이지 클리어 UI를 닫는다.
    //==================================================================================== 
    public void CloseStageClearUI()
    {
        stageName.SetActive(false);
        stageText1.SetActive(false);
    }
}
