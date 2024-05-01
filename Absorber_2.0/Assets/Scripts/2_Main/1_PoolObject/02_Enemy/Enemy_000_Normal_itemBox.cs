using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_000_Normal_itemBox : Enemy
{
    protected override void InitEssentialInfo_enemy()
    {
        id_enemy = "000";
    }
    
    // =========== 오버라이드 =============
    // 상자는 움직이지 않음
    // ===================================
    public override void InitEnemyStatusCustom()
    {     
        hp_max =15;

        damage = 0;
        range = 0;
        movementSpeed = 0;      //움직이지않음
        attackSpeed = 0;    //공격하지 않음


        //
        itemProb = 0f;      //아이템 박스는 죽을 때 다른 적과 달리 확률적으로 회복아이템을 생성하지 않ㅇ므 .
        manaValue = 15f;

        canKnockBack = false;
        hasAttackCustom = false;

        


        battleType = BattleType.none;
    }

    // =========== 오버라이드 =============
    // 상자는 공격하지 않음
    // ===================================
    protected override void AttackCustom()
    {
        
    }

    // =========== 오버라이드 =============
    // 상자는 움직이지 않음
    // ===================================
    protected override void MoveCustom()
    {

    }


    // =========== 오버라이드 =============
    // 죽으면 아이템 드랍 
    // ===================================
    protected override void DieCustom()
    {
        // lucky mana
        int num = Random.Range(0,100); // 확률
        // lucky 
        DropItem defaultItem;
        DropItem luckyItem;


        // default dropItem 
        if (num <50)    // big mana
        {
            defaultItem = ItemPoolManager.instance.SpawnItem("000", manaValue * 3, myTransform.position);
        }
        else            // ruby - heal Item
        {
            defaultItem = ItemPoolManager.instance.SpawnItem("001", 0, myTransform.position);  
        }
        
        
        // 추가로 드랍할 아이템 선택
        num = Random.Range(0,100); // 확률

        // luckyItem = ItemPoolManager.instance.SpawnItem("005", manaValue * 3, myTransform.position);
        // luckyItem = ItemPoolManager.instance.SpawnItem("002", manaValue * 3, myTransform.position);
        // luckyItem = ItemPoolManager.instance.SpawnItem("003", manaValue * 3, myTransform.position);
        // luckyItem = ItemPoolManager.instance.SpawnItem("004", manaValue * 3, myTransform.position);
        // luckyItem = ItemPoolManager.instance.SpawnItem("005", manaValue * 3, myTransform.position);
        
        // // 20 % 확률로 럭키 아이템 생성 
        if (num <= 7)  // sapphire -  magnet
        {   
            luckyItem = ItemPoolManager.instance.SpawnItem("002", manaValue, myTransform.position); 
        }
        // else if (num <= 10) // opal - strengthen
        // {
            // luckyItem = ItemPoolManager.instance.SpawnItem("003", manaValue, myTransform.position);
        // }
        else if (num <= 14 )    // topaz- explosion
        {
            luckyItem = ItemPoolManager.instance.SpawnItem("004", manaValue * 3, myTransform.position);
        }
        else if (num <= 21)    // amethyst - paralysis
        {
            luckyItem = ItemPoolManager.instance.SpawnItem("005", manaValue * 3, myTransform.position);
        }
    }


}
