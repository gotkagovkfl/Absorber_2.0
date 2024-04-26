using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_Bullet : MonoBehaviour
{
    Rigidbody2D bulletRigid;
    public Transform target;
    Animator animator;
    public GameObject warningPrefab;
    public GameObject bigSpearPrefab;

    public GameObject forSpearWarning1Prefab;
    public GameObject forSpearWarning2Prefab;
    public GameObject forSpearWarning3Prefab;
    public GameObject forSpearWarning4Prefab;
    public GameObject forSpearPrefab;
    public GameObject forSpear2Prefab;
    public GameObject forSpear3Prefab;
    public GameObject forSpear4Prefab;
    public List<string> forSpearList;
    public List<int> numList;

    public GameObject spearPrefab;
    public float spearSpeed;

    public GameObject stopPrefab;
    public GameObject stopSpawnPrefab;
    public GameObject normal2Prefab;
    public float nor2Speed = 7f;

    void Start()
    {
        animator = GetComponent<Animator>();
        target = Player.player.transform;

        forSpearList = new List<string>();

        forSpearList.Add("210");
        forSpearList.Add("211");
        forSpearList.Add("212");
        forSpearList.Add("213");
        forSpearList.Add("220");
        forSpearList.Add("221");
        forSpearList.Add("222");
        forSpearList.Add("223");

        spearSpeed = 10.0f;

        Invoke("FireSpear", 5f);
        Invoke("Firestop", 10f);
        Invoke("Firenormal2", 2f);
        Invoke("forSpearStart", 3f);
    }
   
    void forSpearStart()
    {
        StartCoroutine(forSpear());
        Invoke("forSpearStart", 10f);
    }

    void forSpearStartHard()
    {
        StartCoroutine(forSpearHard());
        Invoke("forSpearStartHard", 10f);
    }

    IEnumerator forSpear()
    {
        int ranNum = Random.Range(2, 5);
        float ranTime = Random.Range(0.7f, 1.3f);
        for (int i = 0; i < ranNum; i++)
        {
            if (i != ranNum - 1)
            {
                int ranIndex = Random.Range(0, 4);
                Projectile_Enemy warning = EnemyProjPoolManager.eppm.GetFromPool(forSpearList[ranIndex]);
                warning.SetUp(0, 0, 1, 0, 0, 1f);
                yield return new WaitForSeconds(1.5f);
                Projectile_Enemy forSpear = EnemyProjPoolManager.eppm.GetFromPool(forSpearList[ranIndex + 4]);
                forSpear.SetUp(2, 0, 1, 0, 0, 3f);
                yield return new WaitForSeconds(ranTime);

            }
            else
            {
                int ranIndex = Random.Range(0, 4);
                Projectile_Enemy warning = EnemyProjPoolManager.eppm.GetFromPool(forSpearList[ranIndex]);
                warning.SetUp(0, 0, 1, 0, 0, 1f);
                yield return new WaitForSeconds(1.5f);
                Projectile_Enemy forSpear = EnemyProjPoolManager.eppm.GetFromPool(forSpearList[ranIndex + 4]);
                forSpear.SetUp(2, 0, 1, 0, 0, 3f);
            }
        }
    }

    IEnumerator forSpearHard()
    {
        int ranNum = Random.Range(2, 5);
        float ranTime = Random.Range(0.7f, 1.3f);
        for (int i = 0; i < ranNum; i++)
        {
            if (i != ranNum - 1)
            {
                int ranIndex = Random.Range(0, 4);
                int ranIndex2 = Random.Range(0, 4);
                while (ranIndex == ranIndex2)
                {
                    ranIndex2 = Random.Range(0, 4);
                }
                Projectile_Enemy warning = EnemyProjPoolManager.eppm.GetFromPool(forSpearList[ranIndex]);
                warning.SetUp(0, 0, 1, 0, 0, 1f);
                Projectile_Enemy warning2 = EnemyProjPoolManager.eppm.GetFromPool(forSpearList[ranIndex2]);
                warning2.SetUp(0, 0, 1, 0, 0, 1f);
                yield return new WaitForSeconds(1.5f);
                Projectile_Enemy forSpear = EnemyProjPoolManager.eppm.GetFromPool(forSpearList[ranIndex + 4]);
                forSpear.SetUp(2, 0, 1, 0, 0, 3f);
                Projectile_Enemy forSpear2 = EnemyProjPoolManager.eppm.GetFromPool(forSpearList[ranIndex2 + 4]);
                forSpear2.SetUp(2, 0, 1, 0, 0, 3f);
                yield return new WaitForSeconds(ranTime);

            }
            else
            {
                int ranIndex = Random.Range(0, 4);
                int ranIndex2 = Random.Range(0, 4);
                while (ranIndex == ranIndex2)
                {
                    ranIndex2 = Random.Range(0, 4);
                }
                Projectile_Enemy warning = EnemyProjPoolManager.eppm.GetFromPool(forSpearList[ranIndex]);
                warning.SetUp(0, 0, 1, 0, 0, 1f);
                Projectile_Enemy warning2 = EnemyProjPoolManager.eppm.GetFromPool(forSpearList[ranIndex2]);
                warning2.SetUp(0, 0, 1, 0, 0, 1f);
                yield return new WaitForSeconds(1.5f);
                Projectile_Enemy forSpear = EnemyProjPoolManager.eppm.GetFromPool(forSpearList[ranIndex + 4]);
                forSpear.SetUp(2, 0, 1, 0, 0, 3f);
                Projectile_Enemy forSpear2 = EnemyProjPoolManager.eppm.GetFromPool(forSpearList[ranIndex2 + 4]);
                forSpear2.SetUp(2, 0, 1, 0, 0, 3f);
            }
        }
    }
    
    void FireSpear()
    {
        Projectile_Enemy spearBullet = EnemyProjPoolManager.eppm.GetFromPool("201");
        spearBullet.SetUp(1, spearSpeed, 1, 0, 0, -1f);
        spearBullet.transform.position = transform.position;
        spearBullet.SetDirection(target);
        Rigidbody2D spearRigid = spearBullet.GetComponent<Rigidbody2D>();
        Vector2 direction = target.position - transform.position;
        spearRigid.velocity = direction.normalized * spearSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spearRigid.rotation = angle;
        spearBullet.Action();

        Invoke("FireSpear", 20f);
    }

    void Firenormal2()
    {

        Projectile_Enemy normal2Bullet = EnemyProjPoolManager.eppm.GetFromPool("200");
        normal2Bullet.SetUp(1, nor2Speed, 1, 0, 0, 5f);
        normal2Bullet.transform.position = transform.position;
        normal2Bullet.SetDirection(target);
        Rigidbody2D normal2Rigid = normal2Bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = (target.position - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        normal2Rigid.rotation = angle;
        normal2Bullet.Action();        

        Invoke("Firenormal2", 2f);
    }

    void Firestop()
    {
        List<Vector2> ranPositions = new List<Vector2>();
        for (int i = 0; i < 10; i++)
        {
            float randx = Random.Range(-14f, 14f);
            float randy = Random.Range(-7f, 7f);
            Vector2 stopPosition = Vector2.zero + new Vector2(randx, randy);
            ranPositions.Add(stopPosition);
        }
        StartCoroutine(stopSpawn(ranPositions));
        Invoke("Firestop", 10f);
    }

    IEnumerator stopSpawn(List<Vector2> ranPositions)
    {
        foreach (Vector2 position in ranPositions)
        {
            Projectile_Enemy stopSpawn = EnemyProjPoolManager.eppm.GetFromPool("202");
            stopSpawn.SetUp(0, 0, 1, 0, 0, 2f);
            stopSpawn.transform.position = position;
        }
        yield return new WaitForSeconds(2f);
        foreach (Vector2 position in ranPositions)
        {
            Projectile_Enemy stopBullet = EnemyProjPoolManager.eppm.GetFromPool("203");
            stopBullet.SetUp(1, 0, 1, 0, 0, 5f);
            stopBullet.transform.position = position;
        }
    }

    public IEnumerator stopInvoke()
    {
        CancelInvoke();
        yield return new WaitForSeconds(5f);
        Invoke("FireSpear", 5f);
        Invoke("Firestop", 10f);
        Invoke("Firenormal2", 2f);
        Invoke("forSpearStartHard", 3f);
    }

    public IEnumerator stopInvoke2()
    {
        CancelInvoke();
        yield return null;

    }
}
