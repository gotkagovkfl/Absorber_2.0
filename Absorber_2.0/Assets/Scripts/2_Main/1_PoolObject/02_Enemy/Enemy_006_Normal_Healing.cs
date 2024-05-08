using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_006_Normal_Healing : Enemy
{
    public GameObject heal_zone; // ��ƼŬ�̳� ������ �� �����ֱ�
    public float radius = 5; // heal_zone ������
    public int heal_hp = 3; // �󸶳� ȸ��
    public float distance;
    Vector3 dirVec;

    protected override void InitEssentialInfo_enemy()
    {
        id_enemy = "006";
    }


    public override void InitEnemyStatusCustom()
    {
        hp_max = 25;
        hp_curr = hp_max;

        damage = 2;
        attackSpeed = 0.1f;
        movementSpeed = 3f;
        heal_hp = 5;

        itemProb = 50;
        manaValue = 18;
        //lastAttackTime = -8f;
        battleType = BattleType.range;
    }


    protected override void AttackCustom()
    {
        StartCoroutine(HealEnemy());
    }

    IEnumerator HealEnemy()
    {
        yield return new WaitForSeconds(0.9f);
        //var hitCollider = Physics2D.OverlapCircleAll(transform.position, radius);
        //Debug.Log("주변 몬스터 체력회복");
        //foreach (var hit in hitCollider)
        //{
        //    var ply = hit.GetComponent<Enemy>();
        //    if (ply)
        //    {
        //        var closePoint = hit.ClosestPoint(transform.position);
        //        var dis = Vector3.Distance(closePoint, transform.position);

        //        if(ply.hp + heal_hp >= ply.hpFull)
        //        {
        //            ply.hp = ply.hpFull;
        //        }

        //    }
        //}
        Projectile_Enemy proj = EnemyProjPoolManager.instance.GetFromPool("002");
        proj.SetUp(heal_hp, 0, 1, -99, 0, 5);
        proj.myTransform.position = myTransform.position;
        proj.caster = myTransform;
        proj.Action();
    }


}
