using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=========================================
// 폭발 특수효과 : 
//==========================================
public class Proj_100_explosion : Projectile
{
    // =========== 오버라이드 =============
    // 필수 정보 초기화 
    // ===================================    
    protected override void InitEssentialInfo_proj()
    {
        id_proj = "100";

        splitWhenHit= false;
        shrinkOnDes =false;


        
    }


    // =========== 오버라이드 =============
    // 초기설정 후 액션 (행동 개시) - 
    // ===================================
    public override void Action_custom()
    {
        // animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length; 
        audioSource.PlayOneShot(audioSource.clip);
        
        lifeTime = 0.25f;
    }


    // =========== 오버라이드 =============
    // 분열 : 폭발은 분열하지 않음
    // ===================================
    public override void Split()
    {
        // 분열 횟수가 남아있으면
        if(base.splitNum>0)
        {

        }
    }

    // =========================================
    //
    // ===========================================
    public override void ProjDestroy_custom()
    {
        
    }
}
