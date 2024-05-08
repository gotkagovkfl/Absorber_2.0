using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_777_Dummy : Enemy
{
    protected override void InitEssentialInfo_enemy()
    {
        id_enemy = "777";
    }
    
    // =========== 오버라이드 =============
    // 상자는 움직이지 않음
    // ===================================
    public override void InitEnemyStatusCustom()
    {     
        hp_max =987654321;

        damage = 0;

        movementSpeed = 0;      //움직이지않음
        attackSpeed = 0;    //공격하지 않음

        
        //
        itemProb = 0f;      //아이템 박스는 죽을 때 다른 적과 달리 확률적으로 회복아이템을 생성하지 않ㅇ므 .
        manaValue = 500f;

        canKnockBack = false;

        battleType = BattleType.none;
    }

    // =========== 오버라이드 =============
    // 상자는 공격하지 않음
    // ===================================
    protected override void AttackCustom()
    {
        
    }

}
