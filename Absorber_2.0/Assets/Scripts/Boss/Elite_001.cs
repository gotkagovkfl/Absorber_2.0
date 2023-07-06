using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elite_001 : Enemy
{
    public Transform direction;
    Vector2 movement;
    Vector3 dirVec;

    public Transform firePoint;
    public float distance;
    public GameObject prefabBullet;


    public override void InitEnemyStatusCustom()
    {
        hpFull = 1000;
        damage = 10;
        hp = 1000;
        speed = 2.8f;
        attackSpeed = 0.01f;

        itemProb = 100;
        manaValue = 550;

        //firePoint = transform.Find("FirePoint");
        prefabBullet = Resources.Load<GameObject>("Prefabs/Boss/Elite1_bunB");
    }
    protected override void AttackCustom()
    {

    }

    // public void Update()
    // {
    //     // if (hp <= 3)
    //     // {
    //     //     Destroy(gameObject);
    //     // }
    // }

    public override void DieCustom()
    {
        Instantiate( Resources.Load<GameObject>("Prefabs/W/Stages/Object_666_crown"), myTransform.position, Quaternion.identity);
    }

    public override void MoveCustom()
    {

        distance = Vector3.Distance(transform.position, base.target.transform.position);
        //dirVec = base.target.transform.position - transform.position; 
        //Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        Vector3 dirVec = base.target.transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f)) - transform.position;  // ���� = Ÿ�� ��ġ - �� ��ġ
        rb.velocity = dirVec.normalized * speed;


    }


    public override void InitEssentialEnemyInfo()
    {
        id_enemy = "100";
    }
}
