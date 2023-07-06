using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_004_Normal_Dash : Enemy
{
    public float dashPower = 12f; // ���� ��
    float dashTime = 1f; // ���� ���ӽð�
    float cooltime = 10f; // ���� ��Ÿ��
    bool isDash = false; // �������ΰ�
    bool canDash = true; // ���� ���ɿ���


    public override void InitEnemyStatusCustom()
    {
        hpFull = 50;
        hp = 50;

        damage = 6;

        itemProb = 15;
        manaValue = 25;

        speed = 2.75f;
        attackSpeed = 0.2f;
        cooltime = 10f;
        dashPower = 12f;
        isDash = false;
        canDash = true;

        hasAttackCustom = true;
    }

    public override void MoveCustom()
    {
        Vector3 dirVec = base.target.transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f)) - transform.position;  // ���� = Ÿ�� ��ġ - �� ��ġ
        rb.velocity = Vector2.zero; // 물리적 속도 0으로 고정
        rb.velocity = dirVec.normalized * speed;
    }
    protected override void AttackCustom()
    {
        StartCoroutine(Dash());
    }

    public override void DieCustom()
    {
        // gameObject.SetActive(false);
        // GetComponent<Collider2D>().enabled = true;
        // hp = hpFull;
    }

    IEnumerator Dash()
    {

        canKnockBack = false;
        isDash = true;
        canDash = false;

        canMove = false;
        rb.velocity = Vector3.zero;
        //Debug.Log(" 대시 충전");
        yield return new WaitForSeconds(0.8f);
        StartCoroutine( OnDashEffect() );
        rb.mass = 10000f;

        strongAttack = true;
        damage = 12;

        Vector3 dirVec = base.target.transform.position + new Vector3(Random.Range(-2f,2f), Random.Range(-2f,2f)) - transform.position;  // ���� = Ÿ�� ��ġ - �� ��ġ
        rb.velocity = dirVec.normalized * dashPower;
        //Debug.Log("대시!");
        
        yield return new WaitForSeconds(dashTime);
        rb.mass = 2f;

        strongAttack = false;

        damage = 6;
        //Debug.Log("대시끝");
        isDash = false;
        canKnockBack = true;
        canMove = true;
        yield return new WaitForSeconds(cooltime);
        canDash = true;
        

    }

    public IEnumerator OnDashEffect()
    {
        for (int i=0; i< 5; i++)
        {
            Effect effect = EffectPoolManager.epm.GetFromPool("400");
            effect.InitEffect(myTransform.position);
            effect.ActionEffect();

            yield return null;
            yield return null;
            yield return null;
        }
    }


    public override void InitEssentialEnemyInfo()
    {
        id_enemy = "004";
    }
}
