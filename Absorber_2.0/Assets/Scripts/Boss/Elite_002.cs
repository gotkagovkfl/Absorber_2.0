using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elite_002 : Enemy
{
    public Transform direction;
    Vector2 movement;
    Vector3 dirVec;

    public Transform firePoint;
    public float distance;
    public GameObject prefabBullet;


    public override void InitEnemyStatusCustom()
    {
        hpFull = 500;
        damage = 3;
        hp = 500;
        speed = 3;
        attackSpeed = 0.6f;

        itemProb = 20;
        manaValue = 10;

        //firePoint = transform.Find("FirePoint");
        prefabBullet = Resources.Load<GameObject>("Prefabs/Boss/Elite2_Throw");
    }
    protected override void AttackCustom()
    {
        int ranNum = Random.Range(0, 2);
        float ranX;
        float ranY;
        float ranSpeed = Random.Range(10f, 15f);
        if (ranNum == 0)
        {
            ranX = Random.Range(-1f, 1f) < 0f ? -40f : 40f;
            ranY = Random.Range(-20f, 20f);
        }
        else
        {
            ranX = Random.Range(-40f, 40f);
            ranY = Random.Range(-1f, 1f) < 0f ? -20f : 20f;
        }
        GameObject proj = Instantiate(prefabBullet, new Vector2(ranX, ranY), Quaternion.identity);
        proj.GetComponent<Projectile_Enemy>().SetUp(damage, ranSpeed, 1, 0, 0, 10f);
        proj.GetComponent<Projectile_Enemy>().SetDirection(target.transform);
        proj.GetComponent<Projectile_Enemy>().RotateProj();
        proj.GetComponent<Projectile_Enemy>().Action();
    }

    public override void DieCustom()
    {
        // gameObject.SetActive(false);
        // GetComponent<Collider2D>().enabled = true;
        // hp = hpFull;
    }

    public override void MoveCustom()
    {

        distance = Vector3.Distance(transform.position, base.target.transform.position);
        dirVec = base.target.transform.position - transform.position; // ���� = Ÿ�� ��ġ - �� ��ġ
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // ���� ��ġ

        rb.MovePosition(transform.position + nextVec);


    }

    // public override void Damaged(int damage, Vector3 hitPoint, float knockbackPower)
    // {
    //     hp -= damage;

    //     //drain
    //     int prob = Random.Range(1, 101);
    //     if (prob <= Player.Instance.Drain_prob)
    //         Player.Instance.ChangeHp(Player.Instance.Drain);

    //     // detect death
    //     if (hp <= 0)
    //     {
    //         Death();
    //     }
    // }

    public override void InitEssentialEnemyInfo()
    {
        id_enemy = "200";
    }
}
