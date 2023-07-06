using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=================================
// testCircle 의 공격효과 (장판)
//================================
public class Proj_006_magicCircle : Projectile
{
    float tickDelay;
    
    
    // =========== 오버라이드 =============
    // 필수 정보 초기화 
    // ===================================    
    public override void InitEssentialProjInfo()
    {
        id_proj = "006";
        shrinkOnDes =false;


    }

    // =========== 오버라이드 =============
    // 마법진은 분열레벨이 틱 속도와 틱 수에 영향을 미침 
    // ===================================
    public override void Action_custom()
    {
        int weight = splitNum+1;

        animator.speed = weight;   
        
        animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length; 
        
        
        tickDelay = animationLength * 1/weight;          // 탄속에 영향을 받음 

        int tickNum = 4 * weight;                        //깜빡임 수 
        
        StartCoroutine(Tick(tickNum));

        lifeTime = tickDelay * tickNum;         // 수명 설정 
    }  


    // ===================================
    // 깜빡거릴때마다 피해입히기 
    // ===================================
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
    // 분열    - 마법진은 분열 안함. 대신 분열레벨이 범위에 영향을 미치도록 해보자 .
    // ===================================
    public override void Split()
    {

    }
 
    public override void ProjDestroy_custom()
    {
        
    }
}
