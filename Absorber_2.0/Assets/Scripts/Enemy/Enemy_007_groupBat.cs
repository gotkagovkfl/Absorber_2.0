using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_007_groupBat : Enemy
{
    public float distance;
    Vector3 dirVec;

    float dashPower;
    float dashTime;
    float coolTime;

    float lifetime;

    bool canDash;
    //bool isDash;

    public override void DieCustom()
    {
        
    }

    public override void InitEnemyStatusCustom()
    {
        hpFull = 8;
        hp = hpFull;

        damage = 2;
        attackSpeed = 3f;
        speed = 3f;
        dashPower = 15f;
        dashTime = 5f;
        coolTime = 10f;
        canDash = true;
        //isDash = false;
        canKnockBack = true;
        strongAttack = true;
        oneShot = true;
        shot = false;


        itemProb = 5;
        manaValue = 3;
    }

    public override void InitEssentialEnemyInfo()
    {
        id_enemy = "007";
    }

    public override void MoveCustom()
    {

    }

    protected override void AttackCustom()
    {
        shot = true;
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        canKnockBack = false;
        //isDash = true;
        canDash = false;
        canMove = false;
        rb.velocity = Vector3.zero;
        
        yield return new WaitForSeconds(0.1f);


        Vector3 dirVec = base.target.transform.position + new Vector3(Random.Range(-3f, 3f), Random.Range(-2f, 2f)) - transform.position;  // ���� = Ÿ�� ��ġ - �� ��ġ
        //Vector3 dirVec = base.target.transform.position - transform.position;
        rb.velocity = dirVec.normalized * dashPower;

        yield return new WaitForSeconds(dashTime);

        //isDash = false;
        canKnockBack = true;
        canMove = true;

        CleanDeath();
        //yield return new WaitForSeconds(coolTime);
    }

}
