using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_011_Epic_bat : Enemy
{
    protected override void InitEssentialInfo_enemy()
    {
        id_enemy = "011";
    }
    
    public override void InitEnemyStatusCustom()
    {
        hp_max = 35;
        hp_curr = hp_max;

        damage = 8;
        movementSpeed = 4f;
        attackSpeed = 0.1f; // *****************************************************


        itemProb = 10;
        manaValue = 20;


        battleType = BattleType.melee;
    }

    protected override void AttackCustom()
    {
        
    }
    protected override void MoveCustom()
    {

    }

    protected override void DieCustom()  // *****************************************************
    {
        // obj.SetActive(false);
        // GetComponent<Collider2D>().enabled = true;
        // base.hp = 10;                // *****************************************************
        // base.DropItem();
    }


}
