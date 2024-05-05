using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_7110_BossSpawn : Effect
{
    public Animator animator;
    
    public float spawnDelay = 5f;
    public int check = 0;


    // 현재 보스
    Enemy boss; 

    //=====================================================================================================================

    protected override void InitEssentialInfo_effect()
    {
        id_effect = "7110";
    }

    public override void InitEffect_custom(Vector3 targetPos)
    {
        lifeTime = 1f;


        animator = GetComponent<Animator>();
        animator.speed = 0.75f;
    }

    public override void ActionEffect_custom()
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
