using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj_101_sanctuaryProj : Projectile
{
    // id 초기화
    protected override void InitEssentialInfo_proj()
    {
        id_proj = "101";

        splitWhenHit= false;
        shrinkOnDes =false;

    }
    

    // 파괴시 행동
    public override void ProjDestroy_custom()
    {
        
    }

    // ==========================
    // sanctuary는 피해량의 일부를 회복함 
    // ==========================
    public override void OnHit(Collider2D other, float dmg, Vector3 hitPoint)
    {
        // float healAmount = dmg * 0.2f; 
        // Player.Instance.ChangeHp(healAmount);
    }
    
    // =========================
    // sanctuary는 일정 시간 동안 적에게 피해를 입힘 
    // =========================
    public override void Action_custom()
    {
        
    }

    // 얜 분열 안함
    public override void Split()
    {
        if(base.splitNum>0)
        {

            
        }
    }
}
