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
    
    public UnityEvent<bool> onChange_aimMode;      // 타게팅 모드 변경시,     bool : 자동 타게팅일경우, 


    // player
    public UnityEvent onChange_hp;
    public UnityEvent onChange_exp;
    public UnityEvent onChange_level;

    public UnityEvent onDash;

    
    // public static UnityEvent onInitGame;
    // public static UnityEvent<InitWork> onStartInitWork; 
    // public static UnityEvent onFinishInitWork;    

    //
    




    //



    public static UnityEvent onAnyButtonClick;

    public static UnityEvent onSceneChange;

    public static UnityEvent onGameStart;
    public static UnityEvent onGameFinish;
    
    public static UnityEvent onStageStart;
    public static UnityEvent onStageClear;

    public static UnityEvent onStageChange;


    public static UnityEvent onAppearance_elite;
    public static UnityEvent onAppearance_boss;


    public static UnityEvent onDead_player;
    public static UnityEvent onDead_enemy;


}
