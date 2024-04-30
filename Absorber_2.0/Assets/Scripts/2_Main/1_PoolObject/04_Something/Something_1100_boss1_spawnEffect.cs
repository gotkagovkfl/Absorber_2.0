using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Something_1100_boss1_spawnEffect : Something
{
    public Animator animator;
    
    public float spawnDelay = 5f;
    public int check = 0;


    // 현재 보스
    Enemy boss; 

    //=====================================================================================================================
    protected override void InitEssentialInfo_something()
    {
        _id_something = "1100";
    }

    // 개별 능력치 초기화 
    public override void InitSomething_custom(Vector3 targetPos)
    {        
        pos = targetPos;
        offset = Vector3.zero;
 
        speed = 0f;
        lifeTime = 1f;


        animator = GetComponent<Animator>();
        animator.speed = 0.75f;
    }
    
    // 개별 초기화 
    public override void ActionSomething_custom()
    {
        
    }


    // 보스 생성              *********************** 이거 instance으로 옮기자 
    public void SpawnBoss()              
    {
        string id ="b_001";


        boss = EnemyPoolManager.instance.SpawnBoss(id);
        boss.InitEnemyStatus();
        boss.myTransform.position = transform.position;
    }


    // *******************************수정해야함
    public void DestroyCrown()
    {
        if (StageManager.sm.currStage.bossSpawnObject)
        {
            Destroy(StageManager.sm.currStage.bossSpawnObject);
        }
        
    }


    //
    public void DestroySpawner()
    {
        _isDead = true;
    }

}
