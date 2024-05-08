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
        hp_max = 18;
        damage = 3;
        hp_curr = 15;
        movementSpeed = 1.8f;
        attackSpeed = 0.5f;
        range = 10;
        itemProb = 5;
        manaValue = 8;

        firePoint = transform.Find("FirePoint");

        hasAttackCustom = true;

        battleType = BattleType.range;
    }
    protected override void AttackCustom()
    {
        //GameObject proj = Instantiate(prefabBullet, firePoint.position, Quaternion.identity);
        StartCoroutine(Fire());
        
    }

    public IEnumerator Fire()
    {
        yield return new WaitForSeconds(1f);
        
        Projectile_Enemy proj = EnemyProjPoolManager.instance.GetFromPool("000");
        proj.SetUp(damage*2, 6, 1, 0, 0, 3.5f);
        proj.transform.position = firePoint.position;
        //proj.RotateProj(Projectile_Enemy.ProjDir.up);
       
        proj.SetDirection(target.transform);
        proj.RotateProj();
        float extraAngle = Random.Range(-5f,5f);
        proj.RotateProj(extraAngle);
        proj.Action();
    }


}
