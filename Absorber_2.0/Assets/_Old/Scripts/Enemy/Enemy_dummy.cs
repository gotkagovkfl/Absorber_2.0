using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_dummy : Enemy
{
    
    protected override void InitEssentialInfo_enemy()
    {
        id_enemy = "123";
    }
    
    
    protected override void DieCustom()
    {
        float animationLength = base.animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;  // 애니메이션 길이 측정

        Destroy(gameObject, animationLength);
    }

    public override void InitEnemyStatusCustom()
    {
        base.hp_curr = 10000000000;    
        hp_max =10000000000;

        base.movementSpeed = 0;
        base.attackSpeed = 0;
    }


    protected override void MoveCustom()
    {
        
    }

    protected override void AttackCustom()
    {
        
    }
}
