using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem_005_amethyst : DropItem
{
    //================== 오버라이드 =========================
    // 필수정보 초기화
    //==============================================
    protected override void InitEssentialInfo_item()
    {
        id_dropItem = "005";
    }
    
    
    
    //================== 오버라이드 =========================
    // 자수정 아이템 효과 : 맵 전체 스턴 
    //==============================================
    public override void PickupEffect()
    {
        // 마비 effect - 이거 투사체로 바꿀 예정.
        // Effect effect = EffectPoolManager.instance.GetFromPool("005");
        // effect.InitEffect(myTransform.position);
        // effect.ActionEffect();


        // 모든 적 마비
        Enemy[] enemies = EnemyPoolManager.instance.GetComponentsInChildren<Enemy>();
        foreach(var enemy in enemies)
        {

            //
            enemy.Stunned( 5f, true );
            
            // EffectPoolManager.instance.CreateHitEffect(enemy.myTransform.position);       // 총알 위치에 이펙트 생성 
            
            // enemy.Damaged(effectValue);
        }
    }
}
