using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

using TMPro;

//====================================================================================
//  게임 결과 등을 화면에 표시한다. (  게임 클리어 / 게임 오버 )
//====================================================================================
public class UIGameResult : MonoBehaviour
{
    public static UIGameResult ugr;
    
    public GameObject gameResult;
    public GameObject Pic_GetAbility;
    public Image[] Abilities = new Image[63];
    public GameObject plane;
    public GameObject bloodImage;
    public GameObject infos;

    public TextMeshProUGUI text_resultTitle;
    public TextMeshProUGUI text_stageTime;
    public TextMeshProUGUI text_totalGameTime;

    public TMP_Text Player_Level;
    public TMP_Text Player_KillCount;
    public TMP_Text Player_Weapon;
    public TMP_Text Player_GetItem;
    public TMP_Text Player_Score;

    public TMP_Text Player_Hp;
    public TMP_Text Player_Damage;
    public TMP_Text Player_Attack_Speed;
    public TMP_Text Player_Range;
    public TMP_Text Player_Speed;
    public TMP_Text Player_Crit;

    public Button btn_GotoLobby;

    public AudioSource audioSource;
    public AudioClip sound_victory;
    public AudioClip sound_defeat;

    //================================================================================================================================
    void Awake()
    {
        ugr = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }



    // Start is called before the first frame update
    void Start()
    {
        gameResult = GameObject.Find("Canvas").transform.Find("GameResult").gameObject;
        Pic_GetAbility = GameObject.Find("Pic_GetAbility");

        for (int i = 0; i < Pic_GetAbility.transform.childCount; i++)
            Abilities[i] = Pic_GetAbility.transform.GetChild(i).GetComponent<Image>();

        plane = gameResult.transform.Find("Plane").gameObject;
        bloodImage = gameResult.transform.Find("BloodImage").gameObject;
        infos = gameResult.transform.Find("Infos").gameObject;

        text_resultTitle    = infos.transform.Find("Text_ResultTitle").GetComponent<TextMeshProUGUI>();
        text_stageTime      = infos.transform.Find("Text_StageTime").GetComponent<TextMeshProUGUI>();
        text_totalGameTime  = infos.transform.Find("Text_TotalGameTime").GetComponent<TextMeshProUGUI>();

        btn_GotoLobby = gameResult.transform.Find("Btn_GoToLobby").GetComponent<Button>();
        
        // btn_GotoLobby.onClick.AddListener( ()=> Fade.fade.BtnClickSound() );   ******************************************************
        // btn_GotoLobby.onClick.AddListener( ()=> SceneManager.LoadScene("TestLobby") );      // 로비로 가는 버튼 연결
        

        sound_victory = Resources.Load<AudioClip>("Sound/14_victory");
        sound_defeat = Resources.Load<AudioClip>("Sound/21_Defeatresult");

        //

        ShowGameResult();
    }


    //====================================================================================
    // GameManager의 게임클리어 여부에 따라 게임 결과를 설정한다.  
    //====================================================================================
    public void SetResult()
    {
        string title;
        Color titleColor;
        Color btnColor;

        if (GameManager.gm.gameClear)
        {
            bloodImage.GetComponent<Image>().color = new Color32(80,80,80,0);
            title = "승리!";
            titleColor = new Color(0,0,200);
            btnColor = new Color(0,0,150); 
            audioSource.clip = sound_victory;
            audioSource.Play();
        }
        else
        {
            StartCoroutine( showBlood() );

            title = "패배";
            titleColor = new Color(255,0,0);
            btnColor = new Color(200,0,0);
            audioSource.clip = sound_defeat;
            audioSource.Play();
        }

        text_resultTitle.text = title;
        text_resultTitle.color = titleColor;
        btn_GotoLobby.GetComponent<Image>().color = btnColor;
        float gameTime_raw = GameManager.gm.Stage1_PlayerTime;
        float Full_gameTime_raw = GameManager.gm.totalGameTime;
        int Full_gameTime_minutes = (int)Full_gameTime_raw / 60;
        int Full_gameTime_seconds = (int)Full_gameTime_raw % 60;
        int gameTime_minutes = (int)gameTime_raw / 60;
        int gameTime_seconds = (int)gameTime_raw % 60;
        string seconds = "";
        if (gameTime_seconds < 10)
        {
            seconds = "0";
        }
        string Full_seconds = "";
        if (Full_gameTime_seconds < 10)
        {
            Full_seconds = "0";
        }
        seconds += gameTime_seconds.ToString();
        Full_seconds += Full_gameTime_seconds.ToString();
        text_totalGameTime.text = "Total Game Time " + string.Format("{0}:{1}", Full_gameTime_minutes, Full_seconds);
        text_stageTime.text = string.Format("{0}:{1}", gameTime_minutes, seconds);
        Player_Level.text = GameManager.gm.Player_Level;
        Player_Weapon.text = GameManager.gm.Player_Weapon;
        Player_KillCount.text = "Kill " + GameManager.gm.KillCount.ToString();
        Player_Score.text = "Score " + GameManager.gm.Score.ToString();
        Player_Hp.text = GameManager.gm.Player_Hp;
        Player_Damage.text = GameManager.gm.Player_Atk;
        Player_Attack_Speed.text = GameManager.gm.Player_Atk_Speed;
        Player_Range.text = GameManager.gm.Player_Range;
        Player_Speed.text = GameManager.gm.Player_Speed;
        Player_Crit.text = GameManager.gm.Player_Crit;
        for(int i=0; i<Player.player.chooseList.Count; i++)
        {
            Abilities[i].sprite = Resources.Load<Sprite>("Pictogram/" + Player.player.chooseList[i]);
            Abilities[i].color = new Color(1, 1, 1, 1);
        }
    }

    public IEnumerator showBlood()
    {
        
        yield return new WaitForSeconds(1f);
        bloodImage.GetComponent<Image>().color = new Color32(80,80,80,255);
    }


    //====================================================================================
    //  GameManager의 게임클리어 여부에 따라 게임 결과창을 화면에 띄운다. 
    //==================================================================================== 
    public void ShowGameResult()
    {
        gameResult.SetActive(true);

        SetResult();
    }

}
