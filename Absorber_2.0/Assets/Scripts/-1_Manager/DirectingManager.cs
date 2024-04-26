using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//===================================================
// 디렉팅 매니저 : 페이드 인/아웃 등 각 종 상황에 대한 연출을 담당한다. 
//         **  페이드 인/아웃, 블러드오버레이(low_hp), 카메라 흔들기 (보스, 엘리트 등장), 경고메세지
//===================================================
public class DirectingManager : MonoBehaviour
{
    public static DirectingManager dm;
    //
    bool _onDirecting = false;
    public bool onDirecting
    {
        get => _onDirecting;
    }


    



    //-------------------------------------------------------------
   void Awake()
    {
        // singleton;
        if (dm == null)
        {
            dm = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        animator_fade  = transform.Find("000_fade").GetComponent<Animator>();
        animator_bloodOverlay = transform.Find("001_bloodOverlay").GetComponent<Animator>();
        animator_warningMessage = transform.Find("002_warning").GetComponent<Animator>();

        
    }
    
    
    // =============================================================================================================
    
    //========================
    // 보스페이즈 진입, 스테이지 시작 등 연출 시작할때 발생
    //========================  
    public void DirectingBegin()
    {
        _onDirecting = true;
    }

    //========================
    // 연출 종료시
    //========================  
    public void DirectingEnd()
    {
        _onDirecting = false;
    }


    //=======================================================================================================================================
    #region fade In/Out
    //==========================================================
    // 페이드 인/아웃 
    //==========================================================
    Animator animator_fade;

    public delegate void Delegate_fadeOut();


    //--------------
    // 페이드 아웃
    //-------------
    // public void FadeOut( Delegate_fadeOut d)
    // {
    //     if (animator_fade!=null)
    //     {
    //         _onDirecting = true;
    //         animator_fade.SetTrigger("fadeOut");
    //     }
    //     StartCoroutine(Fade_c( d ));
    // }

    //--------------
    // 페이드인
    //--------------
    // public void FadeIn( Delegate_fadeOut d )
    // {
    //     if (animator_fade!=null)
    //     {
    //         _onDirecting = true;
    //         animator_fade.SetTrigger("fadeIn");
    //     }

    //     StartCoroutine(Fade_c( d) );
    // }

    //-----------
    // 페이드 인/아웃 후에 전달받은 함수 실행 
    //-----------
    // IEnumerator Fade_c( Delegate_fadeOut d )
    // {
    //     yield return new WaitForSeconds(1f);
    //     _onDirecting = false;
    //     d();
        
    // }
    #endregion
    // public void BtnClickSound()             ********************** 버튼 클릭 사운드는 사운드 매니저에서 합시다
    // {
    //     audioSource.PlayOneShot(sound_click);
    // }

    #region camera 
    //======================================================
    // 카메라 쉐이크 (보스등장, 엘리트 등장)
    //======================================================
    float shakeRange = 0.1f;
    float duration = 3f;

    //------------------------
    public void ShakeCamera()
    {
        StartCoroutine( ShakeCamera_c());
    }
    
    IEnumerator ShakeCamera_c()
    {
        var camera_c = StartCoroutine( ShakeCamera_cc());
        yield return new WaitForSeconds(duration);
        StopCoroutine(camera_c);
    }

    IEnumerator ShakeCamera_cc()
    {
        float cameraDx; 
        float cameraDy;
        Vector3 newCameraPos;
        while (true)
        {
            cameraDx = Random.value * shakeRange * 2 - shakeRange;
            cameraDy = Random.value * shakeRange * 2 - shakeRange;

            newCameraPos = Camera.main.transform.position;
            newCameraPos.x += cameraDx;
            newCameraPos.y += cameraDy;

            Camera.main.transform.position = newCameraPos;

            yield return null;
        }
    }

    //======================================================
    // 카메라 포커스 
    //======================================================
    public void FocusCamera_Boss()
    {
        Cinemachine.CinemachineVirtualCamera c = GameObject.Find("Virtual Camera").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        c.Follow = StageManager.sm.currStage.transform.Find("BossSpawnPoint");
    }

    public void FocusCamera_Player()
    {
        Cinemachine.CinemachineVirtualCamera c = GameObject.Find("Virtual Camera").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        
        c.Follow = Player.Instance.myTransform.Find("CameraTarget");
    }
    #endregion

    #region bloodOverlay
    //======================================================
    // 피 테두리 (low hp 효과)  ********* 수정 필요 
    //======================================================
    Animator animator_bloodOverlay;

    public void Show_BloodOverlay(bool isLowHp)
    {
        
        animator_bloodOverlay.SetBool("active", isLowHp);
    }    
    #endregion

    #region bossApperance
    //===================================================
    // 보스 등장 연출
    //===================================================
    Animator animator_warningMessage;

    public void ShowDirecting_bossAppearance()
    {
        _onDirecting = true;
        animator_warningMessage.SetBool("active", true);
        StartCoroutine(ShowDirecting_afterWarning_c());
    }

    public IEnumerator ShowDirecting_afterWarning_c()
    {
        yield return new WaitForSeconds(1.5f);

        ShakeCamera();
        FocusCamera_Boss();
    }


    #endregion

    //============================================================================================================



}
