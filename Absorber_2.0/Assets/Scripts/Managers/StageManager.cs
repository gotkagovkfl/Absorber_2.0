using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ========================================================================
// 스테이지매니저 : 게임매니저(gm)으로부터 현재 스테이지 정보를 받아 그에 맞춰 스테이지를 구성하고, 게임을 진행한다. 
//==========================================================================
public class StageManager : MonoBehaviour
{
    public static StageManager sm;

    //
    public GameObject[] stages = new GameObject[100];
    public Dictionary<string, GameObject> dic_stages = new Dictionary<string, GameObject>();       


    public GameObject portal_prefab;
    public GameObject portal;

    //
    public float currStageTimer; 
    public int currStageNum;    // 현재 스테이지 Num ( GM에서 가져옴 )

    public Effect currStageEffect;  // 현재 스테이지를 꾸미는 이펙트
    
    public enum StageState { wait, ready, onGoing, finished }
    public StageState currStageState;

    // 해당 스테이지 전용 정보
    public Stage currStage;                     // 현재 스테이지 정보 

    public int finalStageNum;               // 최종 스테이지 번호 
    public int totalStageNum;

    // 
    bool isPortalBreak;

    //--------------------------------------------------------------------------------------------------------------------------------------------

    //==============================================================
    void Awake()
    {
        sm = this;

        portal_prefab = Resources.Load<GameObject>("Prefabs/W/Stages/Object_000_portal");  // 포탈 프리팹 로드 **************************** 나중에수정

        LoadStagePrefabs(); // 모든 스테이지 로드 & 초기화
    }


    void Update()
    {
        if (currStageState == StageState.onGoing)
        {
            currStageTimer += Time.deltaTime;
        }

    }

    //====================================================================================================================================================
    
    //============================
    // 최초 1회 스테이지 프리팹을 메모리에 로드하고/ 정보를 초기화 해놓는다. (스테이지 번호 오름차 순으로 정렬)
    // 스테이지 프리팹 정보 : 이미지, 콜라이더, 스폰포인트, 스폰레인지, 보스스폰포인트, 스페셜( 스테이지 특수 좌표나 시스템 )
    //============================
    public void LoadStagePrefabs()
    {
        finalStageNum = 1;
        totalStageNum = 100;
        
        GameObject[] list_stages = Resources.LoadAll<GameObject>("Prefabs/W/Stages");


        // 일단 가져온 스테이지 오브젝트를 사전에 등록
        for(int i = 0;i<list_stages.Length;i++)
        {
            Stage s = list_stages[i].GetComponent<Stage>();
            if (s != null)
            {
                s.InitStageInfo(); 
                stages[s.num_stage] = s.gameObject;
                // dic_stages.Add( s.id_stage, s.gameObject );
            }
        }
    }
    
    //===================================================== 스테이지 루틴 ====================================================
    
    //======================================================================
    // 스테이지 초기화 : stageNum에 맞는 스테이지 세팅을 한다. 
    //======================================================================
    public void InitStage()
    {
        currStageTimer = 0;
    }

    //======================================================================
    // 스테이지 로드 : 스테이지를 시작하기 전 필요 리소스들을 다운받거나 애니메이션 연출을 진행한다. 
    //======================================================================
    public void LoadStage()
    {
        
        // 할일끝나고 게임 시작  
        StartStage();
    }

    //================================================
    // 스테이지 시작 : 현재 스테이지의 루틴을 실행한다. 
    //==============================================
    public void StartStage()
    {
        // 먼지 파티클
        currStageEffect = EffectPoolManager.epm.GetFromPool("999");
        currStageEffect.InitEffect( Vector3.zero);
        currStageEffect.ActionEffect();

        // UI
        StageUI.su.SetStageEnterUI();

        //
        
        
        // 플레이어 위치 초기화
        Player.Instance.myTransform.position = currStage.startPoint;

        // 스테이지의 루틴 시작
        if(currStage !=null )
        {
            StartCoroutine(StartStage_c());
        }
        else
        {
            
        }
        // Time.timeScale = 3;
    }

    public IEnumerator StartStage_c()
    {
        Fade.fade.FadeIn();
        DirectingManager.dm.DirectingBegin();
        yield return new WaitForSeconds(1f);
        DirectingManager.dm.DirectingEnd();

        currStageState = StageState.onGoing;
        currStage.StartStageRoutine();
    }


    //========================================================
    // 스테이지 종료 : 클리어 / 실패 flag에 맞춰 오브젝트 들을 정리하고 화면을 구성한다. 
    //========================================================
    public void FinishStage(bool flag)
    {
        currStageState = StageState.finished;

        // 일단 스테이지의 루틴 종료 
        if (currStage != null)
        {
            currStage.FinishStageRoutine();
        }
        else
        {
            
        }


        //===============================

        // flag == true: 성공
        if(flag ==true)
        {
            // 스테이지 청소 
            CleanStage();
            
            // 성공화면 : 성공 문구를 출력하고, 다음단계로 진행할 수 있는 시스템 (버튼 or 다리나 문 같은 오브젝트)을 생성한다. 
                // 클리어 UI 호출
            StageUI.su.ShowStageClearUI();
                // 포탈 생성
            CreatePortal();
        }
        // flag == false: 실패 이부분은 굳이 필요 없을 수도 (gm에서 호출해도 되니까.)
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
    public void CleanStage()
    {
        // 보스제외 적 : 사망처리 ( 대신 아이템은 드랍안하고 점수 포함 안함  )
        EnemyPoolManager.epm.CleanEveryEnemy();
        // 적 투사체
        EnemyProjPoolManager.eppm.CleanEveryEnemyProj();
        
        // 아이템 : 플레이어가 전부 획득 
        ItemPoolManager.ipm.CleanEveryItem();
    }

    
    //===================================
    // 다음 스테이지 세팅 : 다음 스테이지 번호를 설정한다. 
    // 현재는 그냥 스테이지 번호 증가로 되어있지만, 최종적으로 랜덤으로 설정할것임. 
    //===================================
    public void SetNextStage()
    {
        if (currStageNum ==finalStageNum)
        {
            // 최종 스테이지를 클리어 했다면,
            GameManager.gm.FinishGame(true);
            // Debug.Log("게임 클리어!");
            return;
        }
        
        do 
        {
            currStageNum++;
            currStageNum %= totalStageNum;
        }
        while( stages[currStageNum] == null );  // 여기 조심. 이거때문에 무한 루프 걸릴 수 있음. 잘 필터링 해야함 .
    }

    //====================================
    // 다음 단계로 넘어갈 수 있는 포탈을 생성한다.  - 플레이어 위치와 겹치지 않게 하거나, 생성후 일정 시간동안은 비활성화 하여 바로 충돌 이벤트 발생하는 거 막아야함. 
    //====================================
    public void CreatePortal()
    {
        // 포탈 오브젝트 생성
        portal = Instantiate(portal_prefab, Vector3.zero, Quaternion.identity);

        // 포탈 입장 지시 HUD 추가 ( 화살표 nextStage 같은거 )
        //code here

    }
    //=================================================================================================================================

    //==============================================
    // 스테이지 전환 : player와 portal 충돌시 발생 - 
    //=================================================
    public void GoToNextStage()
    {        
        StageUI.su.CloseStageClearUI();
        currStage.routineOnGoing = false;
        DirectingManager.dm.onDirecting = true;

        Fade.fade.FadeOut( ChangeRoutine );  
    }

    //==============================================
    // 스테이지 전환 연출: 페이드 아웃되면서 다음 스테이지로 진행할 준비를 한다.  
    //=================================================
    public void ChangeRoutine()
    {   
        // 다음 스테이지 세팅
        SetNextStage();
        
        //스테이지가 교체 되는 사이에 플레이어의 입력을 차단한다. 
        // 플레이어 입력 차단 
        
        // 스테이지 교체
        ChangeStage();

        // 
        InitStage();
        LoadStage();

        //
        DirectingManager.dm.onDirecting = false;
    }

    // ==================================================================================================
    // 스테이지 교체 : 기존 스테이지를 파괴하고 현재 stageNum에 맞는 스테이지를 생성한다. ( 포탈에 입장하면 실행됨 )
    // ==================================================================================================
    public void ChangeStage()
    {        
        DestroyStage();
        GenerateStage();
    }   



    //=================================================================================================================================


    //===================================================================
    // 현재 스테이지 오브젝트들을 파괴한다. ( 스테이지 종료 후 교체 목적)
    //===================================================================
    public void DestroyStage()
    {
        // 스테이지 이펙트 파괴
        if (currStageEffect!=null)
        {
            currStageEffect.readyDestroy = true;
        }
        
        
        // 스테이지 게임오브젝트 파괴
        if (currStage != null && !currStage.routineOnGoing)
        {
            Destroy(currStage.gameObject);
        }
    }

    //====================================================================
    // 스테이지 생성 - 현재 스테이지 번호에 맞는 스테이지를 생성한다.  
    //====================================================================
    public void GenerateStage()
    {
        // 스테이지 생성
        currStage = Instantiate(stages[currStageNum], transform).GetComponent<Stage>();
        currStage.transform.position = Vector3.zero;
    }
    

    //===============================================================================================
}
