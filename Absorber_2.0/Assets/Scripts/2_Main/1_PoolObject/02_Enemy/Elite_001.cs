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


    protected override void InitEssentialInfo_enemy()
    {
        id_enemy = "100";
    }
    
    public override void InitEnemyStatusCustom()
    {
        hp_max = 1000;
        damage = 10;
        hp_curr = 1000;
        movementSpeed = 2.8f;
        attackSpeed = 0.01f;
        range = 1;
        itemProb = 100;
        manaValue = 550;

        //firePoint = transform.Find("FirePoint");
        prefabBullet = Resources.Load<GameObject>("Prefabs/Boss/Elite1_bunB");

        battleType = BattleType.melee;
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

    protected override void DropItem()
    {
        base.DropItem();
        
        Instantiate( Resources.Load<GameObject>("Prefabs/W/Stages/Object_666_crown"), myTransform.position, Quaternion.identity);
    }

    // protected override void MoveCustom()
    // {

    // }


}
