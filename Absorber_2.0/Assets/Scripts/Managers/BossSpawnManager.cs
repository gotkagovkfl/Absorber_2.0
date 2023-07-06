using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnManager : MonoBehaviour
{
    public static BossSpawnManager bsm;
    //

    

    //
    public GameObject[] bosses = new GameObject[8];         // 모든 보스 몬스터 프리팹
    public GameObject[] bossSpawners = new GameObject[8];   // 모든 보스 스환 이펙트 프리팹
    
    //
    public BossSpawner bossSpawner;

    void Awake()
    {
        bsm = this;


        bosses[0] = Resources.Load<GameObject>("Prefabs/Boss/Boss_001");       // test 
        bosses[1] = Resources.Load<GameObject>("Prefabs/Boss/Boss_001");
        // bosses[2] = Resources.Load<GameObject>("Prefabs/Boss/Boss2");
        // bosses[3] = Resources.Load<GameObject>("Prefabs/Boss/Boss3");
        // bosses[4] = Resources.Load<GameObject>("Prefabs/Boss/Boss4");



        bossSpawners[0] = Resources.Load<GameObject>("Prefabs/Boss/BossSpawner_001");   //test
        bossSpawners[1] = Resources.Load<GameObject>("Prefabs/Boss/BossSpawner_001");
        // bosses[2] = Resources.Load<GameObject>("Prefabs/Boss/Boss2");
        // bosses[3] = Resources.Load<GameObject>("Prefabs/Boss/Boss3");
        // bosses[4] = Resources.Load<GameObject>("Prefabs/Boss/Boss4");
    }

    /*public void Update()
    {
        if (StageManager.sm.currStageTimer == 60)
        {
            SpawnBoss();
        }
    }*/
    public void SpawnBoss()
    {
        // 보스 연출 
        StartCoroutine(SpawnBoss_c());
    }

    public IEnumerator SpawnBoss_c()
    {
        DirectingManager.dm.DirectingBegin();
        DirectingManager.dm.ShowBossAppearanceEffect();
        yield return new WaitForSeconds(3f);
        
        Vector3 bossSpawnPoint = Vector3.zero;
        if (StageManager.sm.currStage.bossSpawnPoint != null)
        {
            bossSpawnPoint = StageManager.sm.currStage.GetBossSpawnPos();
        }
        bossSpawner = Instantiate(bossSpawners[StageManager.sm.currStageNum], bossSpawnPoint, Quaternion.identity).GetComponent<BossSpawner>();
        bossSpawner.InitBossSpawner();
        bossSpawner.Action();
        

    }




}
