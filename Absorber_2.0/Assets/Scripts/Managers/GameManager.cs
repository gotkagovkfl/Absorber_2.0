using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SpawnManager pool;
    public int KillCount;
    public string Player_Level;
    public string Player_Weapon;
    public string Player_Hp;
    public string Player_Atk;
    public string Player_Atk_Speed;
    public string Player_Speed;
    public string Player_Range;
    public string Player_Crit;
    public float Stage1_PlayerTime;
    public int Score;
	//=================================
    // �⺻ ����
    //================================
    public static GameManager gm;

    public float gameSpeed = 1f;

    public float totalGameTime;  // pure playing Time ( as in the case of exception of pause, animation event )

    public bool isPaused = false;
    // public GameObject pauseBoard;


    public bool onPlay;     //the flag whether the game is in progress

    public bool gameClear;

    //=============================================================================================================================
    //
    public void AccelGameSpeed(bool flag)
    {
        gameSpeed = (flag)?3:1;

        Time.timeScale = gameSpeed;
    }


    // =========================================
    // Pause : Stop All entities and Time Progress
    // =========================================
    public void Pause(bool flag)
    {
        isPaused = flag;
        // pauseBoard.gameObject.SetActive(flag);
        Time.timeScale = (flag) ? 0 : gameSpeed;                    // �߿�
    }

    //=============================================================================================================================

    //==========================================
    // Init Game 
    //==========================================
    public void InitGame()
    {
        /*totalGameTime = 0;       // Ÿ�̸� �ʱ�ȭ
                            // ü�� Ǯ��
                            // �� ���� ������
        KillCount = 0;
        Score = 0;*/
        onPlay = false;         // have to call 'StartGame'
        
        gameClear = false;

    }

    //==========================================
    // Init Game 
    //==========================================
    public void StartGame()
    {
        totalGameTime = 0;       // Ÿ�̸� �ʱ�ȭ
                                 // ü�� Ǯ��
                                 // �� ���� ������
        KillCount = 0;
        Score = 0;
        onPlay = true;

        gameClear = false;
    }

    //==========================================
    // Finish Game ( Kill Final Boss / Fail Any Stage )
    //==========================================
    public void FinishGame(bool clear)
    {
        onPlay = false;
        gameClear = clear;
        Player_Level = "Level " + Player.Instance.Level;
        Player_Weapon = "Weapon\n" + Player.Instance.GetComponent<PlayerWeapon>().currWeapon[0].ToString().Remove(0, 11).Replace(" (UnityEngine.GameObject)", "").Replace("(Clone)", ""); ;
        Player_Hp = "Hp " + Player.Instance.Max_Hp;
        Player_Atk = "Damage " + Player.Instance.Atk;
        Player_Atk_Speed = "Attack_Speed " + (Player.Instance.Attack_Speed + Player.Instance.Attack_Speed * Player.Instance.Attack_Speed_Plus / 100f);
        Player_Range = "Range " + (Player.Instance.Range + Player.Instance.Range * Player.Instance.Range_Plus / 100f);
        Player_Speed = "Speed " + (Player.Instance.Speed + Player.Instance.Speed * Player.Instance.Speed_Plus / 100f);
        Player_Crit = "Crit " + Player.Instance.Crit + "%";
        Stage1_PlayerTime = StageManager.sm.currStageTimer;
        // when kill final boss
        if (clear)
        {
            // Debug.Log("Game Clear");
        }
        // when fail stage (by playerdeath, any other cause) 
        else
        {
            // Debug.Log("Game Over");
                   
        }
        //
        // BackendRank.Instance.RankInsert(GameManager.gm.KillCount);
        

        // Go To Next Scene to check result
        // StartCoroutine(FinishGame_c());
        SceneManager.LoadScene("FinishGameScene");
        //
        // UIGameResult.ugr.ShowGameResult(clear);
    }
    
    public IEnumerator FinishGame_c()
    {
        // Fade.fade.FadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("FinishGameScene");
    }


    // ======================================================================================================================================================
    void Awake()
    {
        // Singleton
        if (gm == null)
        {
            gm = this;     
        }
        else if (gm != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);      //


        //
        Application.targetFrameRate = 70;   // set maximum fps  

    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        //The code below will not run unless 'onPlay' is true
        if (!onPlay)
        {
            return;
        }
        
        // totalGameTime increases by pure playing time ( as in the case of exception of pause, animation event )
        if (StageManager.sm.currStageState == StageManager.StageState.onGoing)
        {
            totalGameTime += Time.deltaTime;
        }
    }
}
