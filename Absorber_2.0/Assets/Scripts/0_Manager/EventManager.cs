using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager em;

    void Awake()
    {      
        if (em == null)
        {
            em = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

        }
    }
    //========================================================
    
    public UnityEvent onAnyButtonClick;

    public UnityEvent onSceneChange;

    public UnityEvent onGameStart;
    public UnityEvent onGameFinish;
    
    public UnityEvent onStageStart;
    public UnityEvent onStageClear;

    public UnityEvent onStageChange;


    public UnityEvent onAppearance_elite;
    public UnityEvent onAppearance_boss;


    public UnityEvent onDead_player;
    public UnityEvent onDead_enemy;


}
