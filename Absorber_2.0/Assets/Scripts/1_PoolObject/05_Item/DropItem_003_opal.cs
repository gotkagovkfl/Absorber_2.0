using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//===========================================
// 오팔 : 능력치 강화
//==============================================
public class DropItem_003_opal : DropItem
{
    //================== 오버라이드 =========================
    // 필수정보 초기화
    //==============================================
    protected override void InitEssentialInfo_item()
    {
        id_dropItem = "003";
    }
    
    
    //================== 오버라이드 =========================
    // 오팔 아이템 효과 - 주 능력치 중 한가지를 강화한다. 
    //==============================================
    public override void PickupEffect()
    {
        // 공격력, 공격속도, 이동속도, 방어력                   
        int num = Random.Range(0,4);

        string stat = "";
        switch(num)
        {
            case 0:
                stat = "Atk";
                break;
            case 1:
                stat = "Attack_Speed_Plus";
                break;
            case 2:
                stat = "Speed_Plus";
                break;
            case 3:
                stat = "Def";
                break;            
        }
        // EffectPoolManager.epm.CreateText(myTransform.position, stat, Color.white, 2);   // temp
        // PlayerStateManager.psm.ChangeStat(stat, 2 , 10f, PlayerStateManager.ChangeType.mul);        
    }
}
