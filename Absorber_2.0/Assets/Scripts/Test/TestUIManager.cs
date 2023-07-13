using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUIManager : MonoBehaviour
{
    public static TestUIManager tum;

    //================================ 게임 진행 ================================================
    public GameObject btn_accel;

    
    //================================ 스테이지 진행 ================================================
    public GameObject btn_StageClear;
    public GameObject btn_StageFail;


    //================================== 플레이어 능력치 ==========================================================

    //----------------------------------- 투사체수-------------------------------------------------
    public Slider slider_projNum; 
    public Text text_projNum;

    public void SetProjNum(float value)
    {
        Player.Instance.projNum = (int)value;
        text_projNum.text = Player.Instance.projNum.ToString();
        
        Player.Instance.GetComponent<PlayerWeapon>().InitWeapons();
    }


    //---------------------------------- 분열 레벨 -------------------------------------------------
    public Slider slider_splitNum;
    public Text text_splitNum;

    public void SetSplitNum(float value)
    {
        Player.Instance.splitNum = (int)value;
        text_splitNum.text = Player.Instance.splitNum.ToString();

        Player.Instance.GetComponent<PlayerWeapon>().InitWeapons();
    }


    //---------------------------------- 폭발 레벨 -------------------------------------------------
    public Slider slider_explosionLevel;
    public Text text_explosionLevel;

    public void SetExplosionLevel(float value)
    {
        Player.Instance.explosionLevel = (int)value;
        text_explosionLevel.text = Player.Instance.explosionLevel.ToString();

        Player.Instance.GetComponent<PlayerWeapon>().InitWeapons();
    }

     //---------------------------------- 출혈 레벨 -------------------------------------------------
    public Slider slider_bleedingLevel;
    public Text text_bleedingLevel;

    public void SetBleedingLevel(float value)
    {
        Player.Instance.bleedingLevel = (int)value;
        text_bleedingLevel.text = Player.Instance.bleedingLevel.ToString();

        Player.Instance.GetComponent<PlayerWeapon>().InitWeapons();
    }

    //---------------------------------- 성역 레벨 -------------------------------------------------
    public Slider slider_sanctuaryLevel;
    public Text text_sanctuaryLevel;

    public void SetSanctuaryLevel(float value)
    {
        Player.Instance.sanctuaryLevel = (int)value;
        text_sanctuaryLevel.text = Player.Instance.sanctuaryLevel.ToString();

        Player.Instance.GetComponent<PlayerWeapon>().InitWeapons();
    }







    void Awake()
    {
        tum = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {

        #region gameProgress ----------------------------------------------------------

        btn_accel =  GameObject.Find("Btn_Accel");
        btn_accel.GetComponent<Toggle>().onValueChanged.AddListener( GameManager.gm.AccelGameSpeed ); 
        
        #endregion


        
        #region StageProceed ----------------------------------------------------------



        btn_StageClear = GameObject.Find("Btn_StageClear");
        btn_StageFail = GameObject.Find("Btn_StageFail");

        // 버튼에 리스너 추가 (스테이지 성공/ 실패 )
        btn_StageClear.GetComponent<Button>().onClick.AddListener( ()=>GameManager.gm.FinishGame(true)  ); 
        btn_StageFail.GetComponent<Button>().onClick.AddListener( ()=>GameManager.gm.FinishGame(false)  ); 

        #endregion

        #region PlayerSatus ----------------------------------------------------------------
        
        slider_projNum = GameObject.Find("Slider_projNum").GetComponent<Slider>();
        slider_splitNum = GameObject.Find("Slider_splitNum").GetComponent<Slider>();
        slider_explosionLevel = GameObject.Find("Slider_explosionLevel").GetComponent<Slider>();
        slider_bleedingLevel = GameObject.Find("Slider_bleedingLevel").GetComponent<Slider>();
        slider_sanctuaryLevel = GameObject.Find("Slider_sanctuaryLevel").GetComponent<Slider>();

        text_projNum = GameObject.Find("Text_projNum").GetComponent<Text>();
        text_splitNum = GameObject.Find("Text_splitNum").GetComponent<Text>();      
        text_explosionLevel = GameObject.Find("Text_explosionLevel").GetComponent<Text>();
        text_bleedingLevel = GameObject.Find("Text_bleedingLevel").GetComponent<Text>();      
        text_sanctuaryLevel = GameObject.Find("Text_sanctuaryLevel").GetComponent<Text>();   

        // UI에 이벤트에 리스너 추가
        slider_projNum.onValueChanged.AddListener(SetProjNum);
        slider_splitNum.onValueChanged.AddListener(SetSplitNum);
        slider_explosionLevel.onValueChanged.AddListener(SetExplosionLevel);
        slider_bleedingLevel.onValueChanged.AddListener(SetBleedingLevel);
        slider_sanctuaryLevel.onValueChanged.AddListener(SetSanctuaryLevel);
        
        #endregion
    }

}
