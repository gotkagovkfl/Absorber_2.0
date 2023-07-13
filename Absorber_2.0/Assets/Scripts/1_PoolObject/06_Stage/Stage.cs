using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stage : MonoBehaviour, IPoolObject
{
    // public AudioSource audioSource;
    
    
    //
    public string id_stage;
    protected string _name_stage;
    public string name_stage
    {
        get => _name_stage;
        
    }

    //------------------------------------------------
    // Routine
    public float routineTimer;      // 루틴 타이머 

    public bool routineOnGoing;    // 스테이지 루틴 진행 여부 
    

    //-------------------------------------------------
    //SpawnPoint
    public Transform[] spawnPoints;
    public int totalSpawnPointsNum;

    //SpawnRange
    public Collider2D[] spawnRange;
    int currSpawnRangeNum = 0;
    public  int totalSpawnRangeNum;

    //BossSpawnPoint
    public Vector3 bossSpawnPoint;
    public GameObject bossSpawnObject;

    //Specials
    public Transform[] specials;
    public int totalSpecialsNum;

    //StartPoinit
    public Vector3 startPoint;
    


    //====================================================// 정보 초기화 관련==================================================================
    public void InitEssentialInfo()
    {
        InitEssentialInfo_stage();
        InitStageInfo();
    }
    protected abstract void InitEssentialInfo_stage();


    public string GetId()
    {
        return id_stage;
    }

    public void OnCreatedInPool()
    {
        // transform.position = Vector3.zero;
    }

    public void OnGettingFromPool()
    {
        transform.position = Vector3.zero;
    }

    //=======================================================================================================
    //============================
    // 스테이지 정보 초기화 
    //============================
    public void InitStageInfo()
    {
        // audioSource = GetComponent<AudioSource>();
        // if (audioSource != null)
        // {
        //     audioSource.playOnAwake = false;
        //     audioSource.loop = false;
        // }
        
        
        // 스테이지 개별 초기화 (id 같은거)
        // InitStageInfo_custom();

        // SpawnPoint
        Transform temp = transform.Find("SpawnPoints");
        spawnPoints = new Transform[temp.childCount];
        for(int i=0;i<temp.childCount;i++)
        {
            spawnPoints[i] = temp.GetChild(i);
        }
        totalSpawnPointsNum = spawnPoints.Length;
        
        // SpawnRange
        spawnRange = transform.Find("SpawnRange").GetComponents<Collider2D>();
        totalSpawnRangeNum = spawnRange.Length;

        // BossSpawnPoint
        bossSpawnPoint = Vector3.zero;
        Transform t = transform.Find("BossSpawnPoint");
        if ( t != null)
        {
            bossSpawnPoint = t.position;
        }


        // Specials
        specials = transform.Find("Specials").GetComponentsInChildren<Transform>();
        totalSpecialsNum = specials.Length;

        // Start
        startPoint = transform.Find("StartPoint").position;
    }


    // public abstract void InitStageInfo_custom();

    

    //===============================================// 루틴 관련 ================================================================

    public void StartStageRoutine()
    {
        routineOnGoing = true;
        // Debug.Log("스테이지 루틴 진행 시작");
        StartStageRoutine_custom();
    }

    public abstract void StartStageRoutine_custom();

    public void FinishStageRoutine()
    {
        routineOnGoing = false;
    }
    


    //===================================================// 좌표 관련==================================================================

    //============================
    // 랜덤 spawnPoint 좌표 get
    //============================
    public Vector3 GetRandomSpawnPos_spawnPoint()
    {
        Vector3 ret = Vector3.zero;
        if (totalSpawnPointsNum >0)
        {
            ret = spawnPoints[ Random.Range(0, totalSpawnPointsNum ) ].position;
        }
        return ret;
    }

    //============================
    // 랜덤 spawnRange 좌표 get
    //============================
    public Vector3 GetRandomSpawnPos_spawnRange()
    {
        Vector3 ret = Vector3.zero;

        if (totalSpawnRangeNum >0)
        {
            ret =  new Vector3(spawnRange[currSpawnRangeNum].bounds.center.x,  spawnRange[currSpawnRangeNum].bounds.center.y);
        
            float boundX = spawnRange[currSpawnRangeNum].bounds.size.x;
            float boundY = spawnRange[currSpawnRangeNum].bounds.size.y;

            ret +=  new Vector3( Random.Range(-boundX/2, boundX/2), Random.Range(-boundY/2, boundY/2)  );

            currSpawnRangeNum = (currSpawnRangeNum+1) % totalSpawnRangeNum;
        }
        
    
        return ret;
    }

    //============================
    // 보스 생성 좌표 get
    //============================
    public Vector3 GetBossSpawnPos()
    {
        return bossSpawnPoint;
    }


}
