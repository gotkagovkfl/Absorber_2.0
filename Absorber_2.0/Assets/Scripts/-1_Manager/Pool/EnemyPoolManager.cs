using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : PoolManager<Enemy>
{
    public static EnemyPoolManager instance;

    // public Transform[] makePoints;
    float spawnRate = 1f;
    public GameObject spawnInfo;


    protected override void Init_custom()
    {
        id_category = PoolType.enemy;
        instance = this;

        spawnInfo = Resources.Load<GameObject>("Prefabs/02_Enemies/SpawnInfo");

        // 스테이지 종료 이벤트 발생시 일반 적 제거 (아이템 드랍 없이 그냥 지우기)
        GameEvent.ge.onStageClear.AddListener( CleanEveryObjects_enemy );   
    }

    public override void GetFromPool_custom(Enemy obj)
    {
        obj.isDead = false;
    }

    // public override string GetId(Enemy obj)
    // {
    //     return obj.id_enemy;
    // }

    // Ǯ�� �ݳ�
    public override void TakeToPool_custom(Enemy obj)
    {
        if (obj.isDead)
        {
            return;
        }
        obj.isDead = true;
        obj.dotQ.Clear();         
    }

    //===================================================================
    public void SpawnEnemy(string id)       // *************
    {
        Debug.Log("몬스터 생성");
        //Enemy enemy = GetFromPool(id);
        //enemy.InitEnemyStatus();
        Vector3 pos = StageManager.sm.currStage.GetRandomSpawnPos_spawnRange();
        StartCoroutine(create_SpawnInfo(id, pos));
        //enemy.transform.position = pos;
    }

    public void SpawnEnemy(string id, int num, Vector3 pos)       // *************
    {
        //Vector3 pos = Vector3.zero;
        //Enemy enemy = GetFromPool(id);
        for (int i=0; i<num; i++)
        {
            StartCoroutine(create_SpawnInfo(id, pos));
            //enemy.InitEnemyStatus();
            //enemy.myTransform.position = pos;

            //return enemy;
        }

        //return enemy;
    }

    public Enemy SpawnBoss(string id)
    {
        Enemy boss = GetFromPool(id);
        boss.InitEnemyStatus();
        boss.myTransform.position = StageManager.sm.currStage.GetBossSpawnPos();
        
        return boss;
    }

    public IEnumerator create_SpawnInfo(string id, Vector3 pos)
    {
        // Debug.Log("표식 생성");
        GameObject info = Instantiate(spawnInfo, transform.position, Quaternion.identity);
        info.transform.position = pos;
        yield return new WaitForSeconds(1f);
        Destroy(info);
        

        Enemy e = GetFromPool(id);
        e.InitEnemyStatus();
        e.myTransform.position = pos;

    }

    //=======================================
    // 스테이지 종료시 남아있는 모든 적 제거 - 아이템을 생성하지않고 점수를 증가시키지 않음. 
    //=======================================
    public void CleanEveryObjects_enemy()
    {
        Enemy[] enemies = GetComponentsInChildren<Enemy>();

        foreach(var enemy in enemies)
        {
            enemy.CleanDeath();
        }
    }


}
