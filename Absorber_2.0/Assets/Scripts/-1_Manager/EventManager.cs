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

    public UnityEvent<Vector3> onPlayerGuard;       //v : hitPoint
    public UnityEvent<Vector3> onPlayerAvoid;       //v : hitPoint
    public UnityEvent<Vector3> onPlayerHit;         //v : hitPoint
    

    

    // enemy
    public UnityEvent<Vector3,int,int> onEnemyHit;      // v: 피격위치, int: dmg, int: type 
    public UnityEvent<Vector3,int> onEnemyBleeding;

    public UnityEvent<Vector3,int> onEnemyheal;
    public UnityEvent<Enemy> onEnemyStunned;

    public UnityEvent<Enemy> onEnemyDie;



    // 아이템
    public UnityEvent<DropItem> onPickUpItem;

    




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
