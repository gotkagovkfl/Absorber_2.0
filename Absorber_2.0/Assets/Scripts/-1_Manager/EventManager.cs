using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


public class GameEvent : MonoBehaviour
{
    public static GameEvent ge; 


    void Awake()
    {
        if(ge == null)
        {
            ge =this;
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }
   }
    


    // system 

    public UnityEvent onPause;
    public UnityEvent onResume;


    // player
    public UnityEvent<bool> onChange_aimMode;      // 타게팅 모드 변경시,     bool : 자동 타게팅일경우, 


    public UnityEvent<int> onChange_hp;
    public UnityEvent onChange_exp;
    public UnityEvent onChange_level;
    public UnityEvent onDash;


    public UnityEvent<KeyCode> onUseSkill;
    

    

    
    // public static UnityEvent onInitGame;
    // public static UnityEvent<InitWork> onStartInitWork; 
    // public static UnityEvent onFinishInitWork;    

    //
    




    //



    public  UnityEvent onAnyButtonClick;

    public  UnityEvent onSceneChange;

    public  UnityEvent onGameStart;
    public  UnityEvent onGameFinish;
    
    public  UnityEvent onStageStart;
    public  UnityEvent onStageClear;

    public UnityEvent onStageChange;


    public  UnityEvent onAppearance_elite;
    public  UnityEvent onAppearance_boss;


    public  UnityEvent onDead_player;
    public  UnityEvent onDead_enemy;


}
