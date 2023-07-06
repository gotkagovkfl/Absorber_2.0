using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_dummy : Enemy
{
    public override void DieCustom()
    {
        float animationLength = base.animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;  // 애니메이션 길이 측정

        Destroy(gameObject, animationLength);
    }

    public override void InitEnemyStatusCustom()
    {
        base.hp = 10000000000;    
        hpFull =10000000000;

        base.speed = 0;
        base.attackSpeed = 0;
    }

    public override void InitEssentialEnemyInfo()
    {
        id_enemy = "123";
    }

    public override void MoveCustom()
    {
        
    }

    protected override void AttackCustom()
    {
        
    }
}
