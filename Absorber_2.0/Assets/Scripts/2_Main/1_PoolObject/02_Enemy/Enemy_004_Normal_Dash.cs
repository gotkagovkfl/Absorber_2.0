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

    protected override void InitEssentialInfo_enemy()
    {
        id_enemy = "004";
    }

    
    public override void InitEnemyStatusCustom()
    {
        hp_max = 50;
        hp_curr = 50;

        damage = 6;

        itemProb = 15;
        manaValue = 25;

        movementSpeed = 2.75f;
        attackSpeed = 0.2f;
        cooltime = 10f;
        dashPower = 12f;
        isDash = false;
        canDash = true;

        hasAttackCustom = true;

        battleType = BattleType.melee;
    }

    protected override void AttackCustom()
    {
        StartCoroutine(Dash());
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
            Effect effect = EffectPoolManager.instance.GetFromPool("400");
            effect.InitEffect(myTransform.position);
            effect.ActionEffect();

            yield return null;
            yield return null;
            yield return null;
        }
    }



}
