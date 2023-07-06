using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_001 : Stage
{
    //BGM
    public AudioClip bgm_normal;
    public AudioClip bgm_bossIn;
    public AudioClip bgm_boss;
    
    // sound 
    public GameObject prefabs_sound_bats;
    //
    
    Vector3 spawnPos = Vector3.zero;
    int cnt = 1;
    int weight = 0;
    int location = 0;
    float newX = 0f;
    float newY = 0f;
    int spawn_cnt = 4; // Enemy_007_groupBat spawn count
    int flag = 0;

    float spawn_late = 0f;


    public override void InitStageInfo_custom()
    {
        id_stage = "001";

        num_stage = 1;

        routineOnGoing = true;

        //
        bgm_normal = Resources.Load<AudioClip>("Sound/1.bgm_DF");
        bgm_bossIn = Resources.Load<AudioClip>("Sound/12_BossIn");
        bgm_boss = Resources.Load<AudioClip>("Sound/1.bgm_Tr(bossmain)");
        //

        prefabs_sound_bats = Resources.Load<GameObject>("Prefabs/W/SoundObjects/SoundObject_000_bats");
    }

    public override void StartStageRoutine_custom()
    {
        audioSource.clip = bgm_normal;
        audioSource.loop = true;
        audioSource.Play();
        //


        var nGroup = StartCoroutine(spawn_Group());
        var item_Box = StartCoroutine(spawn_Box());
        //StartCoroutine(add_weight());

        float currPhaseStartTime = 120f;
        float currPhaseEndTime = 150f;
        
        //0 ~ 120)
        var n       = StartCoroutine(spawn_Enemy("001", 25, 0f, currPhaseStartTime, 120  ));    //2, 10
        var nRanger = StartCoroutine(spawn_Enemy("002", 30, 20f, currPhaseStartTime, 100 ));    //2, 10
        var nBomb   = StartCoroutine(spawn_Enemy("003", 18, 40f, currPhaseStartTime, 80 ));
        var nDash   = StartCoroutine(spawn_Enemy("004", 18, 60f, currPhaseStartTime, 60 ));     // 2,10
        var nHeal   = StartCoroutine(spawn_Enemy("006", 6, 80f, currPhaseStartTime, 40 ));


        // 120 ~150    - elite 1)
        var elite = StartCoroutine(spawn_Enemy("100", 1, currPhaseStartTime, currPhaseStartTime + 1f, 0));
        //var elite = StartCoroutine(spawn_Enemy("100", 1, 10, 11f, 0));

        n = StartCoroutine(spawn_Enemy("001", 8, currPhaseStartTime, currPhaseEndTime, 30));
        nRanger = StartCoroutine(spawn_Enemy("002", 6, currPhaseStartTime, currPhaseEndTime, 30));
        nBomb   = StartCoroutine(spawn_Enemy("003", 3, currPhaseStartTime, currPhaseEndTime, 30));
        nDash   = StartCoroutine(spawn_Enemy("004", 6, currPhaseStartTime, currPhaseEndTime, 30));
        nHeal   = StartCoroutine(spawn_Enemy("006", 1, currPhaseStartTime, currPhaseEndTime, 30));


        // 150~ 195  - epic
        currPhaseStartTime = 150f;
        currPhaseEndTime = 195f;

        n        = StartCoroutine(spawn_Enemy("001", 9, currPhaseStartTime, currPhaseEndTime, 15));    // 3, 15
        nRanger  = StartCoroutine(spawn_Enemy("002", 7, currPhaseStartTime, currPhaseEndTime, 15));
        nBomb    = StartCoroutine(spawn_Enemy("003", 7, currPhaseStartTime, currPhaseEndTime, 30));
        nDash    = StartCoroutine(spawn_Enemy("004", 6, currPhaseStartTime, currPhaseEndTime, 15));
        nHeal    = StartCoroutine(spawn_Enemy("006", 4, currPhaseStartTime, currPhaseEndTime, 30));

        var eNormal = StartCoroutine(spawn_Enemy("011", 3, currPhaseStartTime, currPhaseEndTime, 15));       
        var eRanger = StartCoroutine(spawn_Enemy("012", 3, currPhaseStartTime, currPhaseEndTime, 15));
        var eDash   = StartCoroutine(spawn_Enemy("014", 1, currPhaseStartTime, currPhaseEndTime, 15));


        // 195~ 240 
        currPhaseStartTime = 195f;
        currPhaseEndTime = 240f;

        n       = StartCoroutine(spawn_Enemy("001", 9, currPhaseStartTime, currPhaseEndTime, 15));      //4 , 30   
        nRanger = StartCoroutine(spawn_Enemy("002", 7, currPhaseStartTime, currPhaseEndTime, 15));
        nDash   = StartCoroutine(spawn_Enemy("004", 6, currPhaseStartTime, currPhaseEndTime, 15));

        eNormal = StartCoroutine(spawn_Enemy("011", 4, currPhaseStartTime, currPhaseEndTime, 15));
        eRanger = StartCoroutine(spawn_Enemy("012", 4, currPhaseStartTime, currPhaseEndTime, 15));
        eDash   = StartCoroutine(spawn_Enemy("014", 3, currPhaseStartTime, currPhaseEndTime, 15));


        // 240 ~ 
        currPhaseStartTime = 240f;
        currPhaseEndTime = 3600f;

        var boss = StartCoroutine(spawn_Boss());

        n       = StartCoroutine(spawn_Enemy("001", 1, currPhaseStartTime, currPhaseEndTime, 12));
        nRanger = StartCoroutine(spawn_Enemy("002", 1, currPhaseStartTime, currPhaseEndTime, 12));
        nBomb   = StartCoroutine(spawn_Enemy("003", 1, currPhaseStartTime, currPhaseEndTime, 16));
        nDash   = StartCoroutine(spawn_Enemy("004", 1, currPhaseStartTime, currPhaseEndTime, 12));
        nHeal   = StartCoroutine(spawn_Enemy("006", 1, currPhaseStartTime, currPhaseEndTime, 20));

        eNormal = StartCoroutine(spawn_Enemy("011", 1, currPhaseStartTime, currPhaseEndTime, 20));
        eRanger = StartCoroutine(spawn_Enemy("012", 1, currPhaseStartTime, currPhaseEndTime, 20));
        eDash   = StartCoroutine(spawn_Enemy("014", 1, currPhaseStartTime, currPhaseEndTime, 30));


    }

    // 001:�⺻, 002:Ranger, 003:Bomb, 004:Dash, 005:makeSpawn, 006:Healer

    public IEnumerator spawn_Enemy(string id, int num, float spawnTime, float stopTime, int late)
    {
        // id : enemy_id | num : enemy count | spawnTime : start spawn | stopTime : stop spawn
        yield return new WaitForSeconds(spawnTime);
        while (routineOnGoing)
        {



            for (int i=0;i<num; i++)
            {
                // set next interval
                float interval = late / num;
                float nextInterval = Random.Range(interval * 0.6f, interval * 1.4f);
  
                yield return new WaitForSeconds(nextInterval);


                // set pos
                flag = Random.Range(0, 2);
                if (flag == 0)
                {
                    spawnPos = GetRandomSpawnPos_spawnPoint();
                }
                else
                {
                    spawnPos = GetRandomSpawnPos_spawnRange();
                }

                // spawn enemy
                EnemyPoolManager.epm.SpawnEnemy(id, 1, spawnPos);
            }

            // check break
            if (StageManager.sm.currStageTimer > stopTime)
            {
                num = 0;
                yield break;
            }

            yield return new WaitForSeconds(late);

        }
    }

    IEnumerator spawn_Box()
    {
        yield return new WaitForSeconds(45f);
        while (routineOnGoing)
        {
            // ������ Ȯ���� ���� ������ ���� �ð����� ���� ������ �ʰ� ������ ����


            Enemy box = EnemyPoolManager.epm.GetFromPool("000");
            box.InitEnemyStatus();
            box.myTransform.position = GetRandomSpawnPos_spawnRange();
            yield return new WaitForSeconds( Random.Range(20, 50) );
        }
    }

    IEnumerator spawn_Group()
    {
        yield return new WaitForSeconds(20f);

        while (routineOnGoing)
        {
            SoundObject sound = Instantiate( prefabs_sound_bats).GetComponent<SoundObject>();
            sound.Play();
            
            
            location = Random.Range(0, 4);
            select_Location(location);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    //j,i
                    Enemy enemy = EnemyPoolManager.epm.GetFromPool("007");
                    enemy.InitEnemyStatus();
                    enemy.myTransform.position = new Vector3(newX, newY + (j));
                }
                newX++;
            }

            yield return new WaitForSeconds(Random.Range(20f, 30f));
            // spawn_cnt--;
        }
        yield break;
    }

    IEnumerator spawn_Boss()
    {
        yield return new WaitForSeconds(240f);
        BossSpawnManager.bsm.SpawnBoss();

        StartCoroutine( PlayBossBGM());
    }

    IEnumerator PlayBossBGM()
    {
        audioSource.Stop();
        audioSource.clip = bgm_bossIn;
        audioSource.loop = false;
        audioSource.Play();

        yield return new WaitUntil( ()=>!audioSource.isPlaying );
        
        audioSource.clip = bgm_boss;
        audioSource.loop = false;
        audioSource.Play();
    }

    // ����ġ ���� �ڷ�ƾ �߰� 
    IEnumerator add_weight()
    {
        yield return new WaitForSeconds(120f);
        weight++;
        yield break;
    }

    IEnumerator decrease_late()
    {
        yield return new WaitForSeconds(1f);
        spawn_late += 0.1f;
    }

    void select_Location(int num)
    {
        if(num == 0)
        {
            newX = Random.Range(-30, 30);
            newY = 30;
        }
        else if(num == 1)
        {
            newX = Random.Range(-30, 30);
            newY = -30;
        }
        else if(num == 2)
        {
            newX = 30;
            newY = Random.Range(-30, 30);
        }
        else
        {
            newX = -30;
            newY = Random.Range(-30, 30);
        }
    }
}
