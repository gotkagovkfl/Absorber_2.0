using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_002_Normal_Ranger : Enemy
{
    public Transform direction;
    Vector2 movement;
    Vector3 dirVec;

    public Transform firePoint;
    public float distance;

    

    protected override void InitEssentialInfo_enemy()
    {
        id_enemy = "002";
    }


    
    public override void InitEnemyStatusCustom()
    {
        hpFull = 18;
        damage = 3;
        hp = 15;
        speed = 1.8f;
        attackSpeed = 0.5f;

        itemProb = 5;
        manaValue = 8;

        firePoint = transform.Find("FirePoint");

        hasAttackCustom = true;
    }
    protected override void AttackCustom()
    {
        //GameObject proj = Instantiate(prefabBullet, firePoint.position, Quaternion.identity);
        StartCoroutine(Fire());
        
    }

    public IEnumerator Fire()
    {
        yield return new WaitForSeconds(1f);
        
        Projectile_Enemy proj = EnemyProjPoolManager.eppm.GetFromPool("000");
        proj.SetUp(damage*2, 6, 1, 0, 0, 3.5f);
        proj.transform.position = firePoint.position;
        //proj.RotateProj(Projectile_Enemy.ProjDir.up);
       
        proj.SetDirection(target.transform);
        proj.RotateProj();
        float extraAngle = Random.Range(-5f,5f);
        proj.RotateProj(extraAngle);
        proj.Action();
    }

    public override void DieCustom()
    {

    }

    public override void MoveCustom()
    {

        distance = Vector3.Distance(transform.position, base.target.transform.position);
        //dirVec = base.target.transform.position - transform.position; // ���� = Ÿ�� ��ġ - �� ��ġ
        //Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // ���� ��ġ
        Vector3 dirVec = base.target.transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f)) - transform.position;  // ���� = Ÿ�� ��ġ - �� ��ġ
        rb.velocity = Vector2.zero; // 물리적 속도 0으로 고정


        if (distance >= 10)
        {
            //rb.MovePosition(transform.position + nextVec);
            rb.velocity = dirVec.normalized * speed;
        }
        else
        {
            //rb.MovePosition(transform.position - nextVec);
            rb.velocity = dirVec.normalized * -speed;
        }
        

    }

}
