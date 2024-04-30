using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_014_Epic_Dash : Enemy
{
    public float dashPower = 15f; // ���� ��
    float dashTime = 1f; // ���� ���ӽð�
    float cooltime = 10f; // ���� ��Ÿ��
    bool isDash = false; // �������ΰ�
    bool canDash = true; // ���� ���ɿ���


    protected override void InitEssentialInfo_enemy()
    {
        id_enemy = "014";
    }

    public override void InitEnemyStatusCustom()
    {
        hpFull = 130;
        hp = hpFull;

        damage = 10;

        speed = 3.5f;
        attackSpeed = 0.25f;
        cooltime = 10f;
        dashPower = 15f;
        isDash = false;
        canDash = true;


        itemProb = 33;
        manaValue = 90;
    }

    public override void MoveCustom()
    {
        Vector3 dirVec = base.target.transform.position - transform.position; // ���� = Ÿ�� ��ġ - �� ��ġ
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + nextVec);
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
        damage = 20;

        Vector3 dirVec = base.target.transform.position + new Vector3(Random.Range(-2f,2f), Random.Range(-2f,2f)) - transform.position;  // ���� = Ÿ�� ��ġ - �� ��ġ
        rb.velocity = dirVec.normalized * dashPower;
        //Debug.Log("대시!");
        
        yield return new WaitForSeconds(dashTime);
        rb.mass = 2f;
        strongAttack = false;
        damage = 10;
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
            Effect effect = EffectPoolManager.instance.GetFromPool("400");
            effect.InitEffect(myTransform.position);
            effect.ActionEffect();

            yield return null;
            yield return null;
            yield return null;
        }
    }

}
