using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_012_Epic_Ranger : Enemy
{
    public Transform direction;
    Vector2 movement;
    Vector3 dirVec;

    public Transform firePoint;
    public float distance;

    protected override void InitEssentialInfo_enemy()
    {
        id_enemy = "012";
    }

    public override void InitEnemyStatusCustom()
    {
        hp_max = 40;
        damage = 6;
        hp_curr = hp_max;
        movementSpeed = 2.5f;
        attackSpeed = 0.75f;

        itemProb = 15;
        manaValue = 30;

        firePoint = transform.Find("FirePoint");


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
        proj.SetUp(damage*2, 8, 1.25f, 0, 0, 5f);
        proj.transform.position = firePoint.position;
        //proj.RotateProj(Projectile_Enemy.ProjDir.up);
       
        proj.SetDirection(target.transform);
        proj.RotateProj();
        float extraAngle = Random.Range(-5f,5f);
        proj.RotateProj(extraAngle);
        proj.Action();
    }  

    protected override void DieCustom()
    {
    }

    protected override void MoveCustom()
    {

       
        

    }


}
