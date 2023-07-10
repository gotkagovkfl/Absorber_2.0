using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elite_003 : Enemy
{
    public Transform direction;
    Vector2 movement;
    Vector3 dirVec;

    public Transform firePoint;
    public float distance;
    public GameObject prefabBullet;
    public Transform tar;
    
    
    protected override void InitEssentialInfo_enemy()
    {
        id_enemy = "300";
    }
    public override void InitEnemyStatusCustom()
    {
        hpFull = 500;
        damage = 3;
        hp = 500;
        speed = 3;
        attackSpeed = 0.1f;

        itemProb = 20;
        manaValue = 10;

        //firePoint = transform.Find("FirePoint");
        prefabBullet = Resources.Load<GameObject>("Prefabs/Boss/Elite3_Spear");
    }
    protected override void AttackCustom()
    {
        GameObject proj = Instantiate(prefabBullet, transform.position, Quaternion.identity);
        proj.GetComponent<Projectile_Enemy>().SetUp(damage, 10f, 1, 0, 0, 15f);
        proj.GetComponent<Projectile_Enemy>().SetDirection(target.transform);
        //proj.GetComponent<Projectile_Enemy>().RotateProj();
        //proj.GetComponent<Projectile_Enemy>().Action();
        tar = Player.Instance.transform;
        Vector2 dir = tar.position - transform.position;
        Rigidbody2D spearRigid = proj.GetComponent<Rigidbody2D>();
        float spearSpeed = 10f;
        spearRigid.velocity = dir.normalized * spearSpeed;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        spearRigid.rotation = angle;
    }

    public override void DieCustom()
    {
        // gameObject.SetActive(false);
        // GetComponent<Collider2D>().enabled = true;
        // hp = hpFull;
    }

    public override void MoveCustom()
    {
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


}
