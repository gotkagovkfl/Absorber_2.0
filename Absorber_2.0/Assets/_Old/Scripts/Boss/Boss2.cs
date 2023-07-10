using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    public Boss_Kill uiBoss_Kill;
    public Rigidbody2D rigid;
    public Transform target;                  // �÷��̾� ��ġ
    public float speed;                       // ���� �ӵ�
    public bool bossDied;

    public float Atk;
    public float Hp;
    public float MaxHp;
    public float Def;
    public float baseHp;
    public float baseAtk;
    public float baseDef;                     // ���� ���� ������

    public Boss2_Bullet bullet;
    public bool bulletCheck = false;

    public Vector2[] positions;



    public void Start()
    {
        gameObject.AddComponent<EnemyType>();       // ���ʹ� Ÿ�� ���� : �ϴ��� �п� �۵���Ű�� ����.
        GetComponent<EnemyType>().InitType();

        baseHp = 3000;
        baseAtk = 30;
        baseDef = 5;
        bullet = GetComponent<Boss2_Bullet>();
        Atk = Random.Range(baseAtk - 5, baseAtk + 5);
        MaxHp = Random.Range(baseHp - 200, baseHp + 200);
        Def = Random.Range(baseDef - 1, baseDef + 1);

        Hp = MaxHp;
        speed = 2f;
        rigid = GetComponent<Rigidbody2D>();
        target = Player.Instance.transform;
        bossDied = false;

        positions = new Vector2[]
        {
            new Vector2 (10,5),
            new Vector2 (10,-5),
            new Vector2 (-10,5),
            new Vector2 (-10,-5)
        };
        Invoke("move", 5f);
    }
    // Update is called once per frame
    void Update()
    {
        if (Hp < (MaxHp * 0.9) && !bulletCheck)    
        {
            bulletCheck = true;
            CancelInvoke();
            StartCoroutine(bullet.stopInvoke());
            Invoke("move", 5f);
        }

        if (Hp <= 0 && !bossDied)
        {
            bossDied = true;
            StartCoroutine(Die());
        }
    }

    public void Damaged(float damage)
    {
        Hp -= damage;
    }

    void move()
    {
        int ranIndex = Random.Range(0, 4);
        Vector2 ranPosition = positions[ranIndex];
        transform.position = ranPosition;
        Invoke("move", 3f);
    }

    IEnumerator Die()   // ���� ü�� 0 ���Ϸ� ������ �� 3�� �� ���� ������Ʈ �ı�
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        // GameObject.Find("Boss").GetComponent<BossUI>().check = 0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int dmg = 3;
            // Player.Instance.OnDamage(dmg, hitPoint, strongAttack);   

        }
    }
}
