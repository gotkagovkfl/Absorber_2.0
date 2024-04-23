using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj_008_beam : Projectile
{    
    float tickDelay;
    
    // =========== 오버라이드 =============
    // 필수 정보 초기화 
    // ===================================    
    protected override void InitEssentialInfo_proj()
    {
        id_proj = "008";

        splitWhenHit= false;
        shrinkOnDes =true;

    }


    // =========== 오버라이드 =============
    // 초기설정 후 액션 (행동 개시) - 광선은 분열레벨에 따라 공격 횟수가 많아진다. 
    // ===================================
    public override void Action_custom()
    {        
        animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length; 
        lifeTime = animationLength;         // 수명 설정 

        int tickNum = (splitNum+1)*2;    
        
        tickDelay =  animationLength / tickNum * 0.5f;

        //깜빡임 수 
        StartCoroutine( Tick(tickNum) );

        
    }

    public IEnumerator Tick(int tickNum)
    {  
        for (int i=0;i<tickNum;i++)
        {
            rb.simulated = true;
            yield return new WaitForFixedUpdate();

            rb.simulated = false;
            yield return new WaitForSeconds(tickDelay);
        }


    }


    // =========== 오버라이드 =============
    // 분열   
    // ===================================
    public override void Split()
    {
        // 분열 횟수가 남아있으면
        if(base.splitNum>0)
        {
            
            
        }
    }

    public override void ProjDestroy_custom()
    {
        
    }
}
