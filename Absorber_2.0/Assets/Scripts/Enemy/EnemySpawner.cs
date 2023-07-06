using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] makePoints; // ���� ���� ��ġ�� ��� �迭
    float timer;
    float spawnRate = 1f;
    Vector2 spawnPos;
    GameObject spawnInfo;

    void Awake()
    {
        makePoints = GetComponentsInChildren<Transform>();
        spawnInfo = Resources.Load<GameObject>("Prefabs/Enemies/SpawnInfo");

    }

    //============================
    // num만큼 적 생성 
    //============================
    public void SpawnEnemy(int id, int num)
    {

        for (int i=0;i<num;i++)
        {
            spawnPos = makePoints[Random.Range(1, makePoints.Length)].position;
            StartCoroutine(create_SpawnInfo(spawnPos, 0));
        }

    }
    public void SpawnBomb(int num)
    {
        for (int i = 0; i < num; i++)
        {
            //spawnPos = new Vector2(Random.Range(-16f, 14f), Random.Range(-8f, 7f));
            spawnPos = makePoints[Random.Range(1, makePoints.Length)].position;
            StartCoroutine(create_SpawnInfo(spawnPos, 1));
        }

    }
    public void SpawnDash(int num)
    {

        for (int i = 0; i < num; i++)
        {
            //spawnPos = new Vector2(Random.Range(-16f, 14f), Random.Range(-8f, 7f));
            spawnPos = makePoints[Random.Range(1, makePoints.Length)].position;
            StartCoroutine(create_SpawnInfo(spawnPos, 3));

        }
    }

    public void SpawnRanger(int num)
    {
        for (int i = 0; i < num; i++)
        {
            //spawnPos = new Vector2(Random.Range(-16f, 14f), Random.Range(-8f, 7f));
            spawnPos = makePoints[Random.Range(1, makePoints.Length)].position;
            StartCoroutine(create_SpawnInfo(spawnPos, 4));
        }

    }

    public void SpawnMakeSpawn(int num)
    {
        for (int i = 0; i < num; i++)
        {
            //spawnPos = new Vector2(Random.Range(-16f, 14f), Random.Range(-8f, 7f));
            // 맵이 균일하지 않아서 makepoints 활용
            spawnPos = makePoints[Random.Range(1, makePoints.Length)].position;
            StartCoroutine(create_SpawnInfo(spawnPos, 5));
        }

    }

    public void SpawnHealer(int num)
    {
        for (int i = 0; i < num; i++)
        {
            //spawnPos = new Vector2(Random.Range(-16f, 14f), Random.Range(-8f, 7f));
            // 맵이 균일하지 않아서 makepoints 활용
            spawnPos = makePoints[Random.Range(1, makePoints.Length)].position;
            StartCoroutine(create_SpawnInfo(spawnPos, 6));
        }
    }

    // 박스생성
    public void SpawnBox(int num)   // *****************************************************
    {
        for (int i=0;i<num;i++)
        {
            GameObject box = GameManager.gm.pool.Get(2);
            
            float newX = Random.Range(-16f, 14f);
            float newY = Random.Range(-8f, 7f );

            Vector3 newPos = new Vector3(newX, newY, 0);

            box.transform.position = newPos;
        }
    }

    IEnumerator create_SpawnInfo(Vector2 pos, int code)
    {
        GameObject info = Instantiate(spawnInfo, transform.position, Quaternion.identity);
        info.transform.position = pos;
        yield return new WaitForSeconds(1f);

        GameObject enemy = GameManager.gm.pool.Get(code);
        enemy.transform.position = pos;

        Destroy(info);

    }

    void Update()
    {
        // 자동으로 생기는게 아니라 버튼 누르면 생성되도록 함.
        
        
        // timer += Time.deltaTime;

        // if(timer > spawnRate)
        // {
        //     timer = 0;
        //     GameObject enemy = GameManager.gm.pool.Get(Random.Range(0, 1));
        //     enemy.transform.position = makePoints[Random.Range(1, makePoints.Length)].position;
        // }
        
    }
}
