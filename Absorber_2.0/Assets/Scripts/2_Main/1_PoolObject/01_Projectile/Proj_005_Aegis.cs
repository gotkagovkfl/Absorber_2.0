using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj_005_Aegis : Projectile
{
    public Transform circle;
    Vector3[] scales = new Vector3[4];
    
    // =========== 오버라이드 =============
    // 필수 정보 초기화 
    // ===================================    
    protected override void InitEssentialInfo_proj()
    {
        id_proj = "005";

        splitWhenHit= true;
        shrinkOnDes =false;

    }


    // =========== 오버라이드 =============
    // 애니메이터 정보를 설정함.
    // ===================================
    public override void Action_custom()
    {
        scales[0] = new Vector3(1.6f,1f,1f);
        scales[1] = new Vector3(3.6f,2.25f,1f);
        scales[2] = new Vector3(5.25f,3.25f,1f);
        scales[3] = new Vector3(6.4f,4f,1f);
        
        circle = transform.GetChild(0);

        // 애니메이션 클립 (충격파) 길이 따기
        animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length; 
        lifeTime = animationLength;

    
        StartCoroutine(Extend());

    } 
    // ========================-==========
    // 충격파 확장 : 크기를 키우고 콜라이더를 재설정 
    // ===================================
    IEnumerator Extend()
    {
        float delay = animationLength*0.25f;    // 4번 크기를 바꾸기 위함 (애니메이션 프레임 수에 따라)
        
        for (int i=0;i<4;i++)
        { 
            circle.localScale = scales[i];

            yield return new WaitForSeconds(delay);   
        }
    }


    // =========== override =============
    // split  - 아이기스는 피해입힌 대상의 위치에 충격파 생성
    // ===================================
    public override void Split()
    {
        // 분열 횟수가 남아있으면
        if(base.splitNum>0 && target_hit !=null)
        {
            // //분열지점 세팅 
            splitPoint = hitPoint;
            splitPoint.y = target_hit.position.y; // 디테일
       
            // 효과 생성하기 
            string id = id_proj;
            Projectile proj = ProjPoolManager.instance.GetFromPool(id);
            proj.transform.position = splitPoint;
            
            float splitWeight = 0.45f + 0.15f * (splitNum+1);
            proj.SetUp(base.damage* splitWeight, base.speed, base.scale *splitWeight, base.projNum,  base.penetration,  base.splitNum-1 , -1f);
            proj.SetSpecialStat(explosionLevel -1 , weight_critDamage , knockBackPower* splitWeight);
            proj.Action();
        }
    }  

    public override void ProjDestroy_custom()
    {
        
    }
}
