using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//=============================================================
// GameManager: 씬을 넘나들면서 게임 시작, 종료, 게임 속도 등을 관리한다. 
//=============================================================
public class GameManager : MonoBehaviour
{
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
    // Use in Scene_Lobbby
    // ------------------------

    //==========================================
    // Init Game 
    //==========================================
    public void InitGame()
    {
        onPlay = false;         // have to call 'StartGame'
        gameClear = false;

        totalGameTime = 0;       
        KillCount = 0;
        Score = 0;
    }

    //==========================================
    // Init Game 
    //==========================================
    public void StartGame()
    {
        onPlay = true;

        // GoToNextScene( "Scne_Main",1f);
        // EventManager.em.onGameStart.Invoke();

        // DirectingManager.dm.FadeOut(()=>SceneManager.LoadScene("Scene_Main"));
    }

    //=============================================================================================================================
    // Use in Scene_Main
    // ------------------------
    
    // =========================================
    // Pause Game: Stop All entities and Time Progress
    // =========================================
    public void PauseGame(bool flag)
    {
        isPaused = flag;
        Time.timeScale = (isPaused) ? 0 : gameSpeed;   
        // OnApplicationPause(isPaused);
    }
    
    //test *************************
    void OnGUI()
    {
        if (isPaused)
            GUI.Label(new Rect(100, 100, 50, 30), "Game paused");
    }

    void OnApplicationFocus(bool hasFocus)
    {
        PauseGame(!hasFocus);
    }

    //==========================================
    // Accelate GameSpeed : *3 (can't undo )
    //==========================================
    public void AccelGameSpeed(bool flag)
    {
        gameSpeed = (flag)?3:1;

        Time.timeScale = gameSpeed;
    }

    //=============================================================================================================================


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
        // DirectingManager.dm.FadeOut( ()=>SceneManager.LoadScene("Scene_Result") );
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
        if (StageManager.sm !=null && StageManager.sm.isRunning)
        {
            totalGameTime += Time.deltaTime;
        }
    }
}
