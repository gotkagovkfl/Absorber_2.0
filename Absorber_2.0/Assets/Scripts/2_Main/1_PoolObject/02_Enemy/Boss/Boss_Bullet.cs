using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bullet : MonoBehaviour
{
    public List<System.Action> ranFire = new List<System.Action>();

    List<System.Action> selectedFire = new List<System.Action>();

    Rigidbody2D bulletRigid;
    public Transform target;

    public GameObject normalPrefab;
    public float baseNorSpeed;
    public float baseNorTime;
    public int baseNorBullet;
    public bool norCheck;

    public GameObject bunbPrefab;
    public float baseBunbSpeed;
    public float baseBunbTime;
    public int baseBunbNum;
    
    public GameObject bombPrefab;
    public float baseBombSpeed;
    public float baseBombTime;
    public int baseBombNum;
    public int i = 0;

    public GameObject throwPrefab;
    public float baseThrowSpeed;
    public float baseThrowTime;

    public GameObject aroundPrefab;
    public int numBullets;
    public float distance;
    public bool bosscheck;

    // animation
    public BossAnimationController_001 bac;

    /*public float bosshp = 1;
    public bool bosscheck = false;
    public int numBullets = 18;
    public float distance = 10f;

    public GameObject Boss_Fall;
    public GameObject slicePrefab;

    public GameObject stopPrefab;
    public GameObject normal2Prefab;
    public float nor2Speed = 7f;

    public int ranNum;*/

    public AudioSource audioSource;
    public AudioClip sound_normal;
    public AudioClip sound_halfBullet;
    //=======================================================================================

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sound_normal = Resources.Load<AudioClip>("Sound/18_Boss_bloodbullet");
        sound_halfBullet = Resources.Load<AudioClip>("Sound/18_Boss_halfbullet");
        
        target = Player.player.t_player;
        // bunbPrefab = Resources.Load<GameObject>("Prefabs/Boss/Boss_bunB");
        // bombPrefab = Resources.Load<GameObject>("Prefabs/Boss/Boss_Bomb");
        // normalPrefab = Resources.Load<GameObject>("Prefabs/Boss/Boss_Bullet");
        // throwPrefab = Resources.Load<GameObject>("Prefabs/Boss/Boss_Throw");

        bac = GetComponent<BossAnimationController_001>();

        baseNorSpeed = 5.0f;
        baseNorTime = 1.5f;
        baseNorBullet = 3;
        norCheck = true;

        baseBunbSpeed = 2.0f;
        baseBunbTime = 5.0f;
        baseBunbNum = 3;

        baseBombSpeed = 1.5f;
        baseBombTime = 10.0f;
        baseBombNum = 10;

        baseThrowSpeed = 15.0f;
        baseThrowTime = 2.0f;

        numBullets = 18;
        distance = 10f;
        bosscheck = false;

        ranFire.Add(Firenormal);
        ranFire.Add(Firebunb);
        ranFire.Add(Firebomb);
        ranFire.Add(FireThrow);


        // ���� 4�� �� 3�� ��� ����
        
        System.Random random = new System.Random();

        while (selectedFire.Count < 3)
        {
            System.Action fire = ranFire[random.Next(ranFire.Count)];
            if (!selectedFire.Contains(fire))
            {
                selectedFire.Add(fire);
            }
        }


    }


    public void StartSkillRoutine()
    {
        StartCoroutine(SkillRoutine_c());
    }

    public IEnumerator SkillRoutine_c()
    {
        yield return new WaitForSeconds(1f);
        
        foreach (System.Action fire in selectedFire)
        {
            fire();
        }
    }




    void Firenormal()
    { 
        if (GetComponent<Enemy>().hp <= 0)
        {
            return;
        }
        
        
        //
        audioSource.PlayOneShot(sound_normal);
        //
        bac.ShowEffect_skill_normal();
        //
        int normalRanBullet;
        float norSpeed = 0;
        float angle;

        Vector2 direction = Vector2.zero;

        if (norCheck)
        {
            normalRanBullet = Random.Range(baseNorBullet - 2, baseNorBullet);
            norSpeed = Random.Range(baseNorSpeed, baseNorSpeed + 4.0f);
            for (int i = 0; i < normalRanBullet; i++)
            {
                angle = i * 30f + 15f - normalRanBullet * 15f;
                direction = Quaternion.Euler(0f, 0f, angle) * (target.position - transform.position);
            }
            norCheck = false;            
        }else if (!norCheck)
        {
            normalRanBullet = Random.Range(baseNorBullet, baseNorBullet + 3);
            norSpeed = Random.Range(baseNorSpeed - 2.0f, baseNorSpeed - 3.0f);
            for (int i = 0; i < normalRanBullet; i++)
            {
                angle = i * 15f + 7.5f - normalRanBullet * 7.5f;
                direction = Quaternion.Euler(0f, 0f, angle) * (target.position - transform.position);
            
            }
            norCheck = true;
        }

        Projectile_Enemy normalBullet = EnemyProjPoolManager.instance.GetFromPool("100");
        normalBullet.SetUp(8, norSpeed, 1, 0, 0, 5f );
        normalBullet.transform.position = transform.position;
        normalBullet.SetDirection( direction);
        normalBullet.RotateProj();
        normalBullet.Action();

        Invoke("Firenormal", baseNorTime);
    }

    void Firebunb()
    {
        if (GetComponent<Enemy>().hp <= 0)
        {
            return;
        }
        
        audioSource.PlayOneShot(sound_normal);
        //
        bac.ShowEffect_skill_split();
        //
        for (int i = 0; i < baseBunbNum; i++)
        {
            
            Projectile_Enemy bunbBullet = EnemyProjPoolManager.instance.GetFromPool("101");
            bunbBullet.SetUp(10 ,baseBunbSpeed, 1, 0, 0, 10f);
            bunbBullet.transform.position = transform.position;
            
            Vector3 direction;
            if (baseBunbNum == 1)
            {
                direction = target.position - transform.position;
                
            }else
            {
                direction = Quaternion.AngleAxis(360f / baseBunbNum * i, Vector3.forward) * transform.right;
            }

            bunbBullet.SetDirection(direction);
            bunbBullet.Action();

        }
        Invoke("Firebunb", baseBunbTime);
    }

    void Firebomb()
    {
        if (GetComponent<Enemy>().hp <= 0)
        {
            return;
        }
        
        audioSource.PlayOneShot(sound_normal);
        // 
        bac.ShowEffect_skill_bomb();
        //
        int ranNum = Random.Range(0, 12);
        float ranSpeed = Random.Range(baseBombSpeed, baseBombSpeed + 2);
        
        /*for (int i = 0; i < ranNum; i++)
        {
            Projectile_Enemy bombBullet = EnemyProjPoolManager.instance.GetFromPool("102");
            bombBullet.SetUp( 2,baseBombSpeed, 1, -99, 0, 5f  );
            bombBullet.transform.position = transform.position;
            bombBullet.SetDirection( Quaternion.AngleAxis(360f / ranNum * i, Vector3.forward) * transform.right );
            bombBullet.Action();

        }*/
        Projectile_Enemy bombBullet = EnemyProjPoolManager.instance.GetFromPool("102");
        bombBullet.SetUp(10, ranSpeed, 1, -99, 0, 5f);
        bombBullet.transform.position = transform.position;
        bombBullet.SetDirection(Quaternion.AngleAxis(30f * i, Vector3.forward) * transform.right);
        bombBullet.Action();
        i++;
        Invoke("Firebomb", 1f);
    }

    void FireThrow()
    {
        if (GetComponent<Enemy>().hp <= 0)
        {
            return;
        }
        
        // bac.SetAnimation_weapon_skill();
        
        int ranNum = Random.Range(0, 2);
        float ranX;
        float ranY;
        float ranSpeed = Random.Range(baseThrowSpeed - 5.0f, baseThrowSpeed + 10.0f);
        float ranTime = Random.Range(baseThrowTime - 0.5f, baseThrowTime + 0.5f);
        if (ranNum == 0)
        {
            ranX = Random.Range(-1f, 1f) < 0f ? -40f : 40f;
            ranY = Random.Range(-20f, 20f);
        }else
        {
            ranX = Random.Range(-40f, 40f);
            ranY = Random.Range(-1f, 1f) < 0f ? -20f : 20f;
        } 

        Projectile_Enemy throwBullet = EnemyProjPoolManager.instance.GetFromPool("103");
        throwBullet.SetUp( 15, ranSpeed, 1, 0, 0, 10f );
        throwBullet.transform.position = new Vector3(ranX, ranY, 0f);
        throwBullet.SetDirection(Player.player.t_player);
        throwBullet.RotateProj();
        throwBullet.Action();

        Invoke("FireThrow", ranTime);
    }

    /*void Firestop()
    {
        for (int i = 0; i < 10; i++)
        {
            float randx = Random.Range(-14f, 14f);
            float randy = Random.Range(-7f, 7f);
            Vector2 stopPosition = Vector2.zero + new Vector2(randx, randy);
            GameObject stopBullet = Instantiate(stopPrefab, stopPosition, Quaternion.identity);
            Rigidbody2D stopRigid = stopBullet.GetComponent<Rigidbody2D>();
        }
        Invoke("Firestop", 10f);
    }*/

    public IEnumerator HalfAround()
    {       
        // audioSource.PlayOneShot(sound_halfBullet);
        
        bac.ShowEffect_skill_around();
        
        for (int i = 0; i < numBullets; i++)
        {
            float angle = i * 360 / numBullets;
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
            Vector3 position = Vector3.zero + direction * distance;
            // GameObject bulletObject = Instantiate(aroundPrefab, position, Quaternion.Euler(0, 0, angle));

            Projectile_Enemy bulletObject = EnemyProjPoolManager.instance.GetFromPool("104");
            bulletObject.SetUp(24, 3,1, 0, 0, -1);
            bulletObject.caster = transform;
            bulletObject.transform.position = position;
            bulletObject.transform.rotation = Quaternion.Euler(0, 0, angle);
            bulletObject.SetDirection(Quaternion.Euler(0, 0, angle) * Vector3.right);
            bulletObject.Action();

        }
        bosscheck = true;
        yield break;
    }

    public IEnumerator stopInvoke()
    {
        CancelInvoke();
        yield return new WaitForSeconds(7f);
        // ���� 4�� ���� ����
        // ���� ���� ��ȭ base ģ���� ��ġ ���� ����.
        Invoke("Firenormal", 1f);
        Invoke("Firebunb", 1f);
        Invoke("Firebomb", 1f);
        Invoke("FireThrow", 1f);
    }

    public IEnumerator stopInvoke2()
    {
        CancelInvoke();
        yield return null;

    }

    /*public IEnumerator Fall()
    {
        GameObject slice = Instantiate(slicePrefab);
        yield return new WaitForSeconds(1f);
        GameObject fall1 = Instantiate(Boss_Fall);
    }*/


}
