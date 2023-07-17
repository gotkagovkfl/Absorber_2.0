using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public Animator animator;
    
    public float spawnDelay = 5f;
    public int check = 0;


    // 현재 보스
    Enemy boss; 

    //=====================================================================================================================
    public void InitBossSpawner()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0.75f;
    }


    //====================
    // 보스 소환 이펙트 재생 
    //=====================
    public void Action()
    {

    }


    // 보스 생성              
    public void SpawnBoss()              
    {
        string id ="b_001";


        boss = EnemyPoolManager.epm.SpawnBoss(id);
        boss.InitEnemyStatus();
        boss.myTransform.position = transform.position;
    }
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
        Destroy(gameObject);
    }



    // // 보스 즉사시키기 - 테스트 환경을 위함
    // public void BossKill()
    // {
    //     if (boss ==null)
    //     {
    //         return;
    //     }
        
    //     float dmg = 9999999f;
        
    //     // EntityEffectController.eec.CreateValueText(currBoss, dmg);
    //     EffectPoolManager.epm.CreateText(boss.transform.position, dmg.ToString(), Color.white);
        
    //     boss.GetComponent<Enemy>().hp -= dmg;
    // }


}
