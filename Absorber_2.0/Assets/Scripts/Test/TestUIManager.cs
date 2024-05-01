using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestUIManager : MonoBehaviour
{
    public static TestUIManager tum;

    //================================ 게임 진행 ================================================
    public Toggle btn_accel;

    
    //================================ 스테이지 진행 ================================================
    public Button btn_StageClear;
    public Button btn_StageFail;


    //================================== 플레이어 능력치 ==========================================================

    //----------------------------------- 투사체수-------------------------------------------------
    public Slider slider_projNum; 
    public TextMeshProUGUI text_projNum;

    public void SetProjNum(float value)
    {
        Player.playerStatus.Set_projNum((int)value);
        text_projNum.text = Player.player.projNum.ToString();
        
        Player.player.GetComponent<PlayerWeapon>().InitWeapons();
    }


    //---------------------------------- 분열 레벨 -------------------------------------------------
    public Slider slider_splitNum;
    public TextMeshProUGUI text_splitNum;

    public void SetSplitNum(float value)
    {
        Player.playerStatus.Set_split((int)value);
        text_splitNum.text = Player.player.split.ToString();

        Player.player.GetComponent<PlayerWeapon>().InitWeapons();
    }


    //---------------------------------- 폭발 레벨 -------------------------------------------------
    public Slider slider_explosionLevel;
    public TextMeshProUGUI text_explosionLevel;

    public void SetExplosionLevel(float value)
    {
        
        Player.playerStatus.Set_explosion((int)value);
        text_explosionLevel.text = Player.player.explosionLevel.ToString();

        Player.player.GetComponent<PlayerWeapon>().InitWeapons();
    }

     //---------------------------------- 출혈 레벨 -------------------------------------------------
    public Slider slider_bleedingLevel;
    public TextMeshProUGUI text_bleedingLevel;

    public void SetBleedingLevel(float value)
    {
        Player.playerStatus.Set_bleeding((int)value);
        text_bleedingLevel.text = Player.player.bleedingLevel.ToString();

        Player.player.GetComponent<PlayerWeapon>().InitWeapons();
    }

    //---------------------------------- 성역 레벨 -------------------------------------------------
    public Slider slider_sanctuaryLevel;
    public TextMeshProUGUI text_sanctuaryLevel;

    public void SetSanctuaryLevel(float value)
    {
        Player.playerStatus.Set_sancutary((int)value);
        text_sanctuaryLevel.text = Player.player.sanctuaryLevel.ToString();

        Player.player.GetComponent<PlayerWeapon>().InitWeapons();
    }







    void Awake()
    {
        tum = this;

        gameObject.SetActive( GameConstant.isDebugMode);
    }
    
    // Start is called before the first frame update
    void Start()
    {

        #region gameProgress ----------------------------------------------------------

        // btn_accel =  transform.Find("Btn_Accel").GetComponent<Toggle>();
        btn_accel.onValueChanged.AddListener( GameManager.gm.AccelGameSpeed ); 
        
        #endregion


        
        #region StageProceed ----------------------------------------------------------

        // 버튼에 리스너 추가 (스테이지 성공/ 실패 )
        // btn_StageClear = transform.Find("Btn_StageClear").GetComponent<Button>();
        btn_StageClear.onClick.AddListener( ()=>GameManager.gm.FinishGame(true)  ); 
        
        // btn_StageFail = transform.Find("Btn_StageFail").GetComponent<Button>();
        btn_StageFail.onClick.AddListener( ()=>GameManager.gm.FinishGame(false)  ); 
        
        
        #endregion

        #region PlayerSatus ----------------------------------------------------------------


        // UI에 이벤트에 리스너 추가
        slider_projNum.onValueChanged.AddListener(SetProjNum);
        slider_splitNum.onValueChanged.AddListener(SetSplitNum);
        slider_explosionLevel.onValueChanged.AddListener(SetExplosionLevel);
        slider_bleedingLevel.onValueChanged.AddListener(SetBleedingLevel);
        slider_sanctuaryLevel.onValueChanged.AddListener(SetSanctuaryLevel);
        
        #endregion
    }

}
