using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ========================================================================
// 스테이지매니저 
//==========================================================================
public class StageManager : MonoBehaviour
{
    public static StageManager sm;


    // 해당 스테이지 전용 정보
    Stage _currStage;                     // 현재 스테이지 정보 
    //
    float _currStageTimer; 
    string _currStageId;
    
    // 승리를 위해 남은 스테이지 클리어 횟수

    int currStageNo = 0;

    int targetStageNo = 2;

    // 플레이한 스테이지 목록
    List<string> list_playStages = new List<string>();

    //------------------------------

    // 현재 스테이지 정보
    public Stage currStage
    {
        get =>_currStage;

    }
    
    // 스테이지 경과 시간
    public float currStageTimer
    {
        get => _currStageTimer;
    }
    
    // 현재 스테이지 번호 : 플레이한 스테이지 개수
    public int currStageNum
    {
        get => list_playStages.Count;
    }

    // 스테이지가 진행중인지 
    public bool isRunning
    {
        get
        {
            if (_currStage !=null && _currStage.routineOnGoing )
            {
                return true;
            }
            return false;
        }
    }
    
    //==============================================================
    void Awake()
    {
        sm = this;
    }

    //=============================
    // Start 호출 시 스테이지 시작
    //=============================
    IEnumerator Start()
    {
        yield return new WaitUntil(()=>PrefabManager.initialized);
        yield return null;
        _currStage = StagePoolManager.instance.GetFromPool("999");
        // ChangeStage();
    }


    void Update()
    {
        if (isRunning && !DirectingManager.dm.onDirecting)
        {
            _currStageTimer += Time.deltaTime;
        }
    }

    //====================================================================================================================================================
    
    
    //========================================================
    // 스테이지 클리어 - 다음 스테이지로 나아가기 위한 준비를 한다. 
    //========================================================
    public void FinishStage_clear()
    {

        // 일단 스테이지의 루틴 종료 
        _currStage.FinishStageRoutine();

        //===============================
        currStageNo++;
        
        // 스테이지 종료 이벤트 발생시키기 - 풀링오브젝트 제거, UI 호출, 포탈생성
        GameEvent.ge.onStageClear.Invoke();
    }

    //==============================================
    // 스테이지 전환 : player와 portal 충돌시 발생 - 
    //=================================================
    public void GoToNextStage()
    {        

    }

    //---------------------------------------------------------------------------------------------------------------------------

    //==============================================
    // 스테이지 교체작업  
    //=================================================
    // public void ChangeStage()
    // {   
    //     GameEvent.ge.onStageChange.Invoke();
        
    //     // 다음 스테이지 세팅
    //     SetNextStage();
        
    //     // 스테이지 오브젝트 교체 관련
    //     DestroyStage();
    //     GenerateStage();

    //     // 생성된 스테이지 초기화 관련
    //     InitStage();
    //     StartStage();
    // }


    //---------------------------------------------------------------------------------------------------------------------

    //===================================
    // 다음 스테이지 세팅 : 다음 스테이지 번호를 설정한다.   :현재는 그냥 스테이지 번호 증가로 되어있지만, 최종적으로 랜덤으로 설정할것임. 
    //===================================
    // void SetNextStage()
    // {
    //     if ( currStageNo == targetStageNo)
    //     {
    //         // 최종 스테이지를 클리어 했다면,
    //         GameManager.gm.FinishGame(true);

    //         return;
    //     }



    //     if( GameConstant.isDebugMode)
    //     {
    //         _currStageId = "999";
    //         Debug.Log("디버그 스테이지 진행");
    //     }
    //     else    //일단 디버그 아닐 때는 신경쓰지말자. 
    //     {
    //         _currStageId = $"{currStageNo:000}";
    //         Debug.Log("정상 스테이지 진행");
    //     }
    // }

    //===================================================================
    // 현재 스테이지 오브젝트들을 파괴한다. ( 스테이지 종료 후 교체 목적)
    //===================================================================
    // void DestroyStage()
    // {
    //     // 스테이지 게임오브젝트 파괴 
    //     if (_currStage != null)
    //     {
    //         StagePoolManager.instance.TakeToPool(_currStage);
    //     }
    // }

    //====================================================================
    // 스테이지 생성 - 현재 스테이지 번호에 맞는 스테이지를 생성한다.  
    //====================================================================
    // void GenerateStage()
    // {
    //     list_playStages.Add(_currStageId);
    //     Debug.Log("스테이지 생성 : " + _currStageId);
    //     _currStage = StagePoolManager.instance.GetFromPool(_currStageId);
    // }

    //======================================================================
    // 스테이지 초기화 
    //======================================================================
    // void InitStage()
    // {
    //     _currStageTimer = 0;
    // }

    //======================================================================
    // 스테이지 시작 
    //======================================================================
    // void StartStage()
    // {
    //     GameEvent.ge.onStageStart.Invoke();
    // }


    //===============================================================================================
}
