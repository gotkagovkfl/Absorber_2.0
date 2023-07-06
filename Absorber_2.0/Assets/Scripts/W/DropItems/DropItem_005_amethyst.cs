using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem_005_amethyst : DropItem
{
    //================== 오버라이드 =========================
    // 필수정보 초기화
    //==============================================
    public override void InitEssentialItemInfo()
    {
        id_dropItem = "005";
    }
    
    
    
    //================== 오버라이드 =========================
    // 자수정 아이템 효과 : 맵 전체 스턴 
    //==============================================
    public override void PickupEffect()
    {
        // 마비 effect
        Effect effect = EffectPoolManager.epm.GetFromPool("005");
        effect.InitEffect(myTransform.position);
        effect.ActionEffect();


        // 모든 적 마비
        Enemy[] enemies = EnemyPoolManager.epm.GetComponentsInChildren<Enemy>();
        foreach(var enemy in enemies)
        {
            EffectPoolManager.epm.CreateText(enemy.center.position, "STUNNED!", Color.gray, 2);
            //
            effect = EffectPoolManager.epm.GetFromPool("009");
            effect.InitEffect(enemy.myTransform.position);
            effect.SetTarget(enemy.center);
            effect.SetDependency(enemy);
            effect.ActionEffect();
            //
            enemy.Stunned( 5f );

            // EffectPoolManager.epm.CreateHitEffect(enemy.myTransform.position);       // 총알 위치에 이펙트 생성 
            
            // enemy.Damaged(effectValue);
        }
    }
}
