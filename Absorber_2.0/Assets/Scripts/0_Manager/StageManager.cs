using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ========================================================================
// 스테이지매니저 
//==========================================================================
public class StageManager : MonoBehaviour
{
    public static StageManager sm;

    //
    public GameObject portal_prefab;
    public GameObject portal;

    // 해당 스테이지 전용 정보
    Stage _currStage;                     // 현재 스테이지 정보 
    //
    float _currStageTimer; 
    string _currStageId;
    int _currStageNum;   
    //
    bool _isRunning;
    
    // 승리를 위해 남은 스테이지 클리어 횟수
    int numStageLeft = 1;
    int numStageLeft_total = 1;

    // 플레이한 스테이지 목록
    List<string> list_playStages = new List<string>();

    //------------------------------
    public Stage currStage
    {
        get
        {
            return _currStage;
        }
    }
    public float currStageTimer
    {
        get
        {
            return _currStageTimer;
        }
    }

    public bool isRunning
    {
        get
        {
            return _isRunning;
        }
    }


    public Effect currStageEffect;  // 현재 스테이지를 꾸미는 이펙트
    


    //==============================================================
    void Awake()
    {
        sm = this;

        portal_prefab = Resources.Load<GameObject>("Prefabs/06_Stages/Object_000_portal");  // 포탈 프리팹 로드 **************************** 나중에수정

        // LoadStagePrefabs(); // 모든 스테이지 로드 & 초기화
    }

    //=============================
    // Start 호출 시 스테이지 시작
    //=============================
    void Start()
    {
        ChangeRoutine();
    }


    void Update()
    {
        if (_isRunning && !DirectingManager.dm.onDirecting)
        {
            _currStageTimer += Time.deltaTime;
        }
    }

    //====================================================================================================================================================


    //===================================================== 스테이지 루틴 ====================================================
    
    //======================================================================
    // 스테이지 초기화 : stageNum에 맞는 스테이지 세팅을 한다. 
    //======================================================================
    void InitStage()
    {
        _currStageTimer = 0;
    }

    //======================================================================
    // 스테이지 로드 : 스테이지를 시작하기 전 필요 애니메이션 연출을 진행한다. 
    //======================================================================
    void LoadStage()
    {
        
        // 할일끝나고 게임 시작  
        // StartStage();
        Fade.fade.FadeIn( StartStage );
    }

    //================================================
    // 스테이지 시작 : 현재 스테이지의 루틴을 실행한다. 
    //==============================================
    // public void StartStage()
    // {
    //     // 먼지 파티클      // 이벤트 체인에 넣자****************
    //     currStageEffect = EffectPoolManager.epm.GetFromPool("999");
    //     currStageEffect.InitEffect( Vector3.zero);
    //     currStageEffect.ActionEffect();

    //     // UI       // 이벤트 체인에 넣자****************
    //     StageUI.su.SetStageEnterUI();

    //     //
        
        
    //     // 플레이어 위치 초기화     // 이벤트 체인에 넣자****************
    //     Player.Instance.myTransform.position = currStage.startPoint;

    //     // 스테이지의 루틴 시작
    //     StartCoroutine(StartStage_c());
    //     Fade.fade.FadeIn( )
    //     // Time.timeScale = 3;
    // }

    void StartStage()
    {
        
        
        // Fade.fade.FadeIn();
        // DirectingManager.dm.DirectingBegin(); // 이벤트 체인에 넣자****************
        // yield return new WaitForSeconds(1f);
        // DirectingManager.dm.DirectingEnd();   // 이벤트 체인에 넣자****************

        _isRunning = true;
        _currStage.StartStageRoutine();
    }


    //========================================================
    // 스테이지 종료 : 클리어 / 실패 flag에 맞춰 오브젝트 들을 정리하고 화면을 구성한다. 
    //========================================================
    public void FinishStage(bool flag)
    {
        _isRunning = false;

        // 일단 스테이지의 루틴 종료 
        _currStage.FinishStageRoutine();

        //===============================

        // flag == true: 성공
        if(flag ==true)
        {
            numStageLeft--; //   
            
            // 스테이지 청소 
            CleanStage();  // 이벤트 체인에 넣자****************
            
            // 성공화면 : 성공 문구를 출력하고, 다음단계로 진행할 수 있는 시스템 (버튼 or 다리나 문 같은 오브젝트)을 생성한다. 
                // 클리어 UI 호출 
            StageUI.su.ShowStageClearUI();    // 이벤트 체인에 넣자****************
                // 포탈 생성
            CreatePortal();  // 이벤트 체인에 넣자****************
        }
        // flag== false: 실패 - 나중에 확장성을 위해 일단 구현해놓기
        else
        {
            // 실패화면 :  실패 애니메이션 ,점수, 능력치 스텟, 선택 과정 등을 화면에 보여주고 로비로 가는 버튼을 생성한다. 
            GameManager.gm.FinishGame(false);
        }
    }

    //=======================================================================================================================

    //=====================================
    // 스테이지 정리 - 스테이지 전환시 플레이어를 제외한 오브젝트를 전부 지운다.  
    // 아이템, 적, 효과, 맵 다 지우기 
    //====================================
    public void CleanStage()    // 이벤트 체인에 넣자****************
    {
        // 보스제외 적 : 사망처리 ( 대신 아이템은 드랍안하고 점수 포함 안함  )
        EnemyPoolManager.epm.CleanEveryEnemy();  // 이벤트 체인에 넣자****************
        // 적 투사체
        EnemyProjPoolManager.eppm.CleanEveryEnemyProj();  // 이벤트 체인에 넣자****************
        
        // 아이템 : 플레이어가 전부 획득 
        ItemPoolManager.ipm.CleanEveryItem();  // 이벤트 체인에 넣자****************
    }

    
    //===================================
    // 다음 스테이지 세팅 : 다음 스테이지 번호를 설정한다. 
    // 현재는 그냥 스테이지 번호 증가로 되어있지만, 최종적으로 랜덤으로 설정할것임. 
    //===================================
    void SetNextStage()
    {
        if ( numStageLeft == 0)
        {
            // 최종 스테이지를 클리어 했다면,
            GameManager.gm.FinishGame(true);
            // Debug.Log("게임 클리어!");
            return;
        }

        _currStageId = (list_playStages.Count ==0)?"000":StagePoolManager.spm.GetRandomStageId();
    }

    //====================================
    // 다음 단계로 넘어갈 수 있는 포탈을 생성한다.  - 플레이어 위치와 겹치지 않게 하거나, 생성후 일정 시간동안은 비활성화 하여 바로 충돌 이벤트 발생하는 거 막아야함. 
    //====================================
    public void CreatePortal()  // 이벤트 체인에 넣자****************
    {
        // 포탈 오브젝트 생성
        portal = Instantiate(portal_prefab, Vector3.zero, Quaternion.identity);

    }
    //=================================================================================================================================

    //==============================================
    // 스테이지 전환 : player와 portal 충돌시 발생 - 
    //=================================================
    public void GoToNextStage()
    {        
        StageUI.su.CloseStageClearUI();  // 이벤트 체인에 넣자****************
        DirectingManager.dm.onDirecting = true;  // 이벤트 체인에 넣자****************

        Fade.fade.FadeOut( ChangeRoutine );  
    }

    //==============================================
    // 스테이지 전환 연출: 페이드 아웃되면서 다음 스테이지로 진행할 준비를 한다.  
    //=================================================
    public void ChangeRoutine()
    {   
        // 다음 스테이지 세팅
        SetNextStage();
        
        // 스테이지 오브젝트 교체 관련
        DestroyStage();
        GenerateStage();

        // 생성된 스테이지 초기화 관련
        InitStage();
        LoadStage();

        //
        DirectingManager.dm.onDirecting = false; // 이벤트 체인에 넣자****************
    }

    //=================================================================================================================================


    //===================================================================
    // 현재 스테이지 오브젝트들을 파괴한다. ( 스테이지 종료 후 교체 목적)
    //===================================================================
    void DestroyStage()
    {
        // 스테이지 이펙트 파괴 // 이벤트 체인에 넣자****************
        if (currStageEffect!=null)
        {
            currStageEffect.readyDestroy = true;
        }
        
        
        // 스테이지 게임오브젝트 파괴
        if (_currStage != null && !_currStage.routineOnGoing)
        {
            StagePoolManager.spm.TakeToPool(_currStage);
        }
    }

    //====================================================================
    // 스테이지 생성 - 현재 스테이지 번호에 맞는 스테이지를 생성한다.  
    //====================================================================
    void GenerateStage()
    {
        // 스테이지 생성
        list_playStages.Add(_currStageId);
        _currStage = StagePoolManager.spm.GetFromPool(_currStageId);
    }


    //===============================================================================================
}
