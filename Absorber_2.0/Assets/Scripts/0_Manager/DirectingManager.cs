using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectingManager : MonoBehaviour
{
    public static DirectingManager dm;
    //
    public bool onDirecting = false;
    

    public Cinemachine.CinemachineVirtualCamera c;
    // ======================================================
    
    //========================
    // 보스페이즈 진입, 스테이지 시작 등 연출 시작할때 발생
    //========================  
    public void DirectingBegin()
    {
        onDirecting = true;
    }

    //========================
    // 연출 종료시
    //========================  
    public void DirectingEnd()
    {
        onDirecting = false;
    }



    //======================================================================
    void Awake()
    {
        dm = this;
        mainCamera = Camera.main;
    }

    void Start()
    {
        warningMessage = GameObject.FindObjectOfType<WarningMessage>();

        c = GameObject.Find("Virtual Camera").GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }
    //===============================================================================================
    public Camera mainCamera;
    Vector3 cameraPos;

    [SerializeField] float shakeRange = 0.1f;
    [SerializeField] float duration = 3f;

    //===================================================================
    public void ShakeCamera()
    {
        cameraPos = mainCamera.transform.position;
        InvokeRepeating("StartShakeCamera", 0f, 0.005f);
        Invoke("StopShakeCamera", duration);
    }

    void StartShakeCamera()
    {
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        mainCamera.transform.position = cameraPos;
    }

    void StopShakeCamera()
    {
        CancelInvoke("StartShakeCamera");
        mainCamera.transform.position = cameraPos;

    }

    //
    public void FocusCamera_Boss()
    {
        c.Follow = StageManager.sm.currStage.transform.Find("BossSpawnPoint");
    }

    public void FocusCamera_Player()
    {
        c.Follow = Player.Instance.myTransform.Find("CameraTarget");
    }


    //=========================================================
    public WarningMessage warningMessage;


    public void ShowBossAppearanceEffect()
    {
        StartCoroutine(ShowBossAppearanceEffect_c());
    }

    public IEnumerator  ShowBossAppearanceEffect_c()
    {
        warningMessage.MessageOn();
        yield return new WaitForSeconds(1.5f);
        warningMessage.MessageOff();
        ShakeCamera();
        FocusCamera_Boss();
    }



    


}
