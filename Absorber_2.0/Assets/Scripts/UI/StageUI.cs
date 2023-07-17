using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class StageUI : MonoBehaviour
{
    GameObject stageUI;

    GameObject obj_stageStartUI;
    GameObject obj_stageClearUI;

    //--
    TextMeshProUGUI text_stageTime; // 진행 경과 시간 UI
    //
    TextMeshProUGUI text_stageClear;
    TextMeshProUGUI text_enterPortal;
    //
    TextMeshProUGUI text_stageName;
    
    
    //================================================================================================================================
    void Awake()
    {
        stageUI = GameObject.Find("Canvas").transform.Find("StageUI").gameObject;

        obj_stageStartUI = stageUI.transform.Find("StageStartUI").gameObject;
        obj_stageClearUI = stageUI.transform.Find("StageClearUI").gameObject;


        text_stageTime  =  stageUI.transform.Find("Text_StageTime").GetComponent<TextMeshProUGUI>();

        text_stageName  = obj_stageStartUI.transform.Find("Text_StageName").GetComponent<TextMeshProUGUI>();

        text_stageClear = obj_stageClearUI.transform.Find("Text_StageClear").GetComponent<TextMeshProUGUI>();
        text_enterPortal= obj_stageClearUI.transform.Find("Text_EnterPortal").GetComponent<TextMeshProUGUI>();
        //
        CloseStageStartUI();
        CloseStageClearUI();
        
        // 스테이지 교체시 이벤트
        EventManager.em.onStageChange.AddListener( CloseStageClearUI);
        
        // 스테이지 시작시 이벤트
        EventManager.em.onStageStart.AddListener( ShowStageStartUI );
        // 스테이지 클리어시 이벤트
        EventManager.em.onStageClear.AddListener( ShowStageClearUI );

    }

    void Start()
    {
        StartCoroutine(UpdateTimeUI());
    }
    //=========================================================================================================================

    //=================
    // 시간 UI 업데이트
    //================
    public IEnumerator UpdateTimeUI()
    {
        while (true)
        {
            float stageTime_raw = StageManager.sm.currStageTimer; 

            int stageTime_minutes = (int)stageTime_raw/60;
            int temp = (int)stageTime_raw % 60;

            string stageTime_seconds=(temp< 10)?"0":"";
            stageTime_seconds+=temp.ToString();

            text_stageTime.text = string.Format("{0}:{1}",stageTime_minutes, stageTime_seconds);

            yield return new WaitForSeconds(1f);
        }
    }


    //====================================================================================================================================

    //===========================================
    //  스테이지 시작 UI를 보여준다.
    //===========================================
    public void ShowStageStartUI()
    {        
        obj_stageStartUI.SetActive(true);
        
        text_stageName.text = StageManager.sm.currStage.name_stage;

        StartCoroutine(ShowStageStartUIAnimation());
    }

    //===================================
    // 스테이지 시작 UI 애니메이션 
    //===================================
    public IEnumerator ShowStageStartUIAnimation()
    {
        Vector3 originalPos = obj_stageStartUI.transform.position;
        
        Color color = Color.white;
        for (int i=0;i<10;i++)
        {
            color.a = 0.1f*i;
            text_stageName.color  = color;
            obj_stageStartUI.transform.position += Vector3.up*1.5f;
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
            obj_stageStartUI.transform.position -= Vector3.up * 1.5f;
            
        }
        obj_stageStartUI.transform.position = originalPos;
        
        CloseStageStartUI();
    }
    //========================================
    //  스테이지 시작 UI를 닫는다.
    //========================================
    public void CloseStageStartUI()
    {
        obj_stageStartUI.SetActive(false);
    }

    //===================================================================================================================


    //=========================================
    //  스테이지 클리어 UI를 보여준다
    //=========================================
    public void ShowStageClearUI()
    {
        obj_stageClearUI.SetActive(true);

        text_stageClear.text = string.Format("{0} 클리어!", StageManager.sm.currStage.name_stage);
    }


    //========================================
    //  스테이지 클리어 UI를 닫는다.
    //========================================
    public void CloseStageClearUI()
    {
        obj_stageClearUI.SetActive(false);
    }
}
