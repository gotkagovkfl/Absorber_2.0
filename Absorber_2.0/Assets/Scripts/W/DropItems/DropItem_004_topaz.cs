using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem_004_topaz : DropItem
{
    //================== 오버라이드 =========================
    // 필수정보 초기화
    //==============================================
    public override void InitEssentialItemInfo()
    {
        id_dropItem = "004";
    }
    
    
    
    
    //================== 오버라이드 =========================
    // 토파즈 획득 효과 : 맵 전체에 폭발 
    //==============================================
    public override void PickupEffect()
    {
        // 폭발 효과
        Projectile proj = ProjPoolManager.ppm.GetFromPool("100");        

        proj.SetUp( 10 + Player.Instance.Atk * 4, 0,  8,  0, -99, 0 , -1);        
        proj.SetSpecialStat(0,2,10);     
        proj.myTransform.position = myTransform.position;
        proj.Action();

    }
}
