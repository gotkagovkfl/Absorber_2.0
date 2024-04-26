using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;


//==========================================
// HUD (Head Up Display) 
// 체력, 경험치, 시간 등 게임 진행 정보를 화면에 보여준다. 
//==========================================
public class HUD : MonoBehaviour
{
    // HUD 요소들
    TextMeshProUGUI text_gameTime; // 진행 경과 시간 UI

    //=========================================================================
    // Start - 초기화
    void Start()
    {
        text_gameTime = GameObject.Find("Text_StageTime").GetComponent<TextMeshProUGUI>();

        StartCoroutine(UpdateTimeUI());
    }
    
    public IEnumerator UpdateTimeUI()
    {
        while (true)
        {
            float gameTime_raw = StageManager.sm.currStageTimer;
            int gameTime_minutes = (int)gameTime_raw/60;
            int gameTime_seconds = (int)gameTime_raw%60;

            string seconds="";
            if (gameTime_seconds< 10)
            {
                seconds= "0";
            }
            seconds+=gameTime_seconds.ToString();
            
            text_gameTime.text = string.Format("{0}:{1}",gameTime_minutes,seconds);

            yield return new WaitForSeconds(1f);
        }
    }


    // // UI 업데이트
    // void LateUpdate()
    // {
    //     // 시간 업데이트


    //     // hp바 업데이트

    //     // exp바 업데이트
    // }
}
