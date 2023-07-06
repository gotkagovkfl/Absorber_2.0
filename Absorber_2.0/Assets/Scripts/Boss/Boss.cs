using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{   
    public Boss_Kill uiBoss_Kill;
    public Rigidbody2D rigid;
    public Transform target;                  // 플레이어 위치

    public Transform center;

    public float speed;                       // 보스 속도
    public bool bossDied;
    public bool move = false;

    public float Atk;
    public float Hp;
    public float MaxHp;
    public float Def;
    public float baseHp;
    public float baseAtk;
    public float baseDef;                     // 방어력 뺄까 고민중

    public Boss_Bullet bullet;
    public bool bulletCheck = false;
    
    public bool active = false;

    public BossAnimationController_001 bac;

    public void Start()
    {
        // gameObject.AddComponent<EnemyType>();       // 에너미 타입 결정 : 일단은 분열 작동시키기 위해.
        // GetComponent<EnemyType>().InitType();

        bac = GetComponent<BossAnimationController_001>();
        
        baseHp = 1000;
        baseAtk = 20;
        baseDef = 5;
        bullet = GetComponent<Boss_Bullet>();
        Atk = Random.Range(baseAtk - 5, baseAtk + 5);
        MaxHp = Random.Range(baseHp - 200, baseHp + 200);
        Def = Random.Range(baseDef - 1, baseDef + 1);

        Hp = MaxHp;
        speed = 2f;
        rigid = GetComponent<Rigidbody2D>();
        target = Player.Instance.myTransform;
        bossDied = false;


        center = transform.Find("Center");
        
        
        
    }
    
    public void StartRoutine()
    {
        active = true;
        move = true;
        bullet.StartSkillRoutine();
    }


    private void FixedUpdate()
    {
        if(Hp < (MaxHp * 0.9) && !bulletCheck)       // 반피 때 변신 예정. 변신 시 행동 중단 및 반피 패턴.
        {
            Enter2Phase();
        }

        if (Hp <= 0 && !bossDied)
        {
            bossDied = true;
            StartCoroutine(Die());
        }
        if (move)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    public void Damaged(float damage)
    {
        Hp -= damage;
    }

    public void ChangeHp()
    {
        Hp += Atk;
    }

    public void Enter2Phase()
    {
        bulletCheck = true;
        StartCoroutine(bullet.stopInvoke());
        StartCoroutine(BossStop());
    }


    IEnumerator BossStop()
    {
        move = false;
        yield return new WaitForSeconds(5f);
        
        bac.OnEnter2Phase();        // 연출

        yield return new WaitForSeconds(2f);

        move = true;
        StartCoroutine(bullet.HalfAround());
    }
    IEnumerator Die()   // 보스 체력 0 이하로 내려갈 시 3초 뒤 보스 오브젝트 파괴
    {
        speed = 0f;
        forDestroy[] des = FindObjectsOfType<forDestroy>();
        foreach (forDestroy obj in des)
        {
            Destroy(obj.gameObject);
        }
        StartCoroutine(bullet.stopInvoke2());
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        // GameObject.Find("Boss").GetComponent<BossUI>().check = 0f;
    }

    IEnumerator MoveDown()    // 보스 등장 장면
    {
        rigid.velocity = new Vector2(0, -5);
        yield return new WaitForSeconds(3f);
        rigid.velocity = Vector2.zero;
        yield return new WaitForSeconds(3f);
        move = true;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            Vector3 hitPoint = center.position;
            
            int dmg = 3;

            if (dmg != 0 )
            { 
                // Player.player.OnDamage(dmg);
                Player.Instance.OnDamage(dmg, hitPoint, false);
            }
         
        }
    }
}
