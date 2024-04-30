using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//===========================================
// 사파이어 : 자석
//==============================================
public class DropItem_002_sapphire: DropItem
{
    
    //================== 오버라이드 =========================
    // 필수정보 초기화
    //==============================================
    protected override void InitEssentialInfo_item()
    {
        id_dropItem = "002";
    }
    
    
    
    
    //================== 오버라이드 =========================
    // 사파이어 아이템 획득 효과 - 모든 아이템을 캡처한다. 
    //==============================================
    public override void PickupEffect()
    {
        //Debug.Log("magnet");

        DropItem[] items = ItemPoolManager.instance.GetComponentsInChildren<DropItem>();
        foreach(var item in items)
        {
            item.captured = true;
        }
    }
}
