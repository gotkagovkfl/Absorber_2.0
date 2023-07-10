using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//================== Manager ===================
// 해당 스테이지의 정보를 관리한다
//==============================================
public class StageInfoManager : MonoBehaviour
{
    // public static StageInfoManager sim;

    // public bool win = false;

    // public GameObject obj_text_die;
    // public GameObject obj_text_win;
    
    // public GameObject obj_result;

    // public GameObject obj_btn_win;
    // public GameObject obj_btn_die;

    
    //=============================
    // 현재 스테이지를 종료한다. 게임 결과를 세팅한다. 
    //================================

    // public void FinishStage(bool isWin)
    // {
    //     // GameManager.gm.Pause(true); // 게임 일시정지 - 나중에 바꿀거임 - 적 죽는 애니메이션과 아이템이 움직이는 애니메이션이 진행되어야함. 
    //     // StageManager.sm.ClearStage();

    //     // win = isWin;

    //     // // 스테이지 종료시 결과 텍스트 생성
    //     // ShowText();

    //     // // 결과창 지연 생성
    //     // StartCoroutine(ShowResult());
    // }

    
    //=============================
    // 결과에 따라 텍스트 on
    //================================
    // public void ShowText()
    // {
    //     obj_text_win.SetActive(win);
    //     obj_text_die.SetActive(!win); 
    // }
    // //=============================
    // // 텍스트 off
    // //================================
    // public void OffText()
    // {
    //     obj_text_win.SetActive(false);
    //     obj_text_die.SetActive(false);         
    // }

    //=====================================
    // 결과창 on ( 일시정지가 걸려 timeScale==0 이라서 코루틴으로 사용 )
    //=======================================
    // IEnumerator ShowResult()
    // {
        // // 나중에는 여기서 pause하도록 할거임.
        
        // yield return new WaitForSecondsRealtime(2f);
        // // 결과 텍스트 off
        // OffText();

        // // 결과창 on
        // obj_result.SetActive(true); 

        // // 결과 버튼 on
        // ShowBtn();
    // }

    //=============================
    // 게임 결과에 따라 버튼 on
    //================================
    // public void ShowBtn()
    // {
    //     // obj_btn_win.SetActive(win);
    //     // obj_btn_die.SetActive(!win);        
    // }

    // //=============================
    // // 버튼 off
    // //================================
    // public void OffBtn()
    // {
    //     obj_btn_win.SetActive(false);
    //     obj_btn_die.SetActive(false);           
    // }

    
    // public void GoToLobby()
    // {
    //     // OffBtn();
    //     // GameManager.gm.Pause(false);        // 로비로 가기전 일시정지 해제 (timescale 1로 돌리기 )
    //     // SceneManager.LoadScene("TestLobby");
    // }

    // public void GoToNextStage()
    // {
    //     // OffBtn();
    //     // obj_result.SetActive(false);    // 결과창 닫기 
    //     // GameManager.gm.Pause(false);    // 일시정지해제
    //     // GameManager.gm.InitGame(); // 타이머도 초기화

    //     // // 그리고 어디선가 스테이지 세팅해야함.
    //     // // StageManager.sm.SetStage();
    //     // // StageManager.sm.GenerateStage();

    //     // Debug.Log("다음 스테이지");
    // }



    //
    //
    //
    //
    //
    //
    // //==============================
    // void Awake()
    // {
    //     sim = this;
    // }
}

