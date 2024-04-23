using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public static class GameEvent 
{
    public static UnityEvent onInitGame;
    public static UnityEvent<InitWork> onStartInitWork; 
    public static UnityEvent onFinishInitWork;    

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
