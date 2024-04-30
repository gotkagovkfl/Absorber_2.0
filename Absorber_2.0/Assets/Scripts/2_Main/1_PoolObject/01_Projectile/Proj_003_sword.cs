using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=======================================================
// Sword 의 투사체 : 스프라이트가 오른쪽 방향 보고있음
//===================================================================
public class Proj_003_sword : Projectile
{
    
    
    // public bool targetIsRight = true;   // 타겟이 무기보다 오른쪽에 있는지 - 스프라이트 방향 결정하기 위함.

    // Vector3 firstTargetPos;         // 값이 정해지지 않는다면 어차피 0이니 오류 발생안할듯


    // =========== 오버라이드 =============
    // 필수 정보 초기화 
    // ===================================    
    protected override void InitEssentialInfo_proj()
    {
        id_proj = "003";
        splitWhenHit= true;
        shrinkOnDes =true;
        SetSplitAngles(-35,35,0,0,0,0,0); // 수정

    }

    // =========== 오버라이드 =============
    // 애니메이터 정보를 설정함.
    // ===================================
    public override void Action_custom()
    {
        //분열각도 세부조정
        if (splitNum == 1)
        {
            SetSplitAngles(0,0,0,0,0,0,0);
        }
        else if (splitNum ==2)
        {
            SetSplitAngles(-30,30,0,0,0,0,0);
        }
        else if (splitNum >=3)
        {
            SetSplitAngles(-15,0,15,0,0,0,0);
        }
        
        


        animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length; 
        rb.velocity = myTransform.up * speed;      // 검기가 날라감 
    } 

    // =========== 오버라이드 =============
    // 분열   
    // ===================================
    public override void Split()
    {
        // 분열 횟수가 남아있으면
        if(base.splitNum>0)
        {
            splitPoint = hitPoint;

            string id = id_proj;
            float splitWeight = 0.5f + splitNum * 0.15f;
            
            int splitCount = (splitNum>=3)?3:splitNum;
            // 분열 
            for (int i=0;i<splitCount;i++)
            {
                // 효과 생성하기 
                Projectile proj = ProjPoolManager.instance.GetFromPool(id);

                // 맞고 살아있으면 공격 불가 대상으로 지정
                if( !target_hit.GetComponent<Enemy>().isDead)
                {
                    proj.target_unattackable = target_hit;     // 현재 타격 대상을 공격불가로 지정 (분열되자마자 피해입히는 것을 방지하기 위해) 다음 충돌에는 풀어줄수있도록함.                     
                }
            
                
                proj.SetUp(damage* splitWeight, speed, scale *splitWeight*splitWeight, projNum,  penetration,  splitNum-1 , lifeTime);
                proj.SetSpecialStat(explosionLevel -1 , weight_critDamage , knockBackPower* splitWeight);

                proj.myTransform.position = splitPoint;
                proj.myTransform.rotation = myTransform.rotation;       // 각도는 기본 투사체의 각도

                proj.SetTarget(target);                
                proj.RotateProj( splitAngle[i] + Random.Range(-8f, 8f));
                proj.Action();
            }
        }
    }
    public override void ProjDestroy_custom()
    {
        
    }
}
