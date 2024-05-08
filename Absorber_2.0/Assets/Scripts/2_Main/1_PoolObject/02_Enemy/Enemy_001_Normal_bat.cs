using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_001_Normal_bat : Enemy
{
    protected override void InitEssentialInfo_enemy()
    {
        id_enemy = "001";
    }
    
    public override void InitEnemyStatusCustom()
    {
        hp_max =12;        

        damage = 4;
        movementSpeed = 2.5f;
        attackSpeed = 0.1f; // *****************************************************

        itemProb = 1;
        manaValue = 5;

        hasAttackCustom = false;

        battleType = BattleType.melee;
    }

    protected override void AttackCustom()
    {
        
    }

}
