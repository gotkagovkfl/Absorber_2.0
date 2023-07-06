using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=============================================
// testRange에서 발사되는 총알
// =============================================
public class Proj_001_bowGun : Projectile
{   
    // =========== 오버라이드 =============
    // 필수 정보 초기화 
    // ===================================    
    public override void InitEssentialProjInfo()
    {
        id_proj = "001";

        splitWhenHit= true;
        shrinkOnDes =true;
        
    }


    // =========== 오버라이드 =============
    // 초기설정 후 액션 (행동 개시) - 적 방향으로 머리를 회전시킨후 이동한다.  
    // ===================================
    public override void Action_custom()
    {
        //분열각도 세부조정
        if (splitNum == 1)
        {
            SetSplitAngles(0,-120,120,-45,45,-90,90);
        }
        else if (splitNum ==2)
        {
            SetSplitAngles(0,-72,72,-144,144,-90,90);
        }
        else if (splitNum >=3)
        {
            SetSplitAngles(0,-135,135,-45,45,-90,90);
        }
        
        rb.velocity = myTransform.up * base.speed;
    }




    // =========== 오버라이드 =============
    // 분열   
    // ===================================
    public override void Split()
    {
        // 분열 횟수가 남아있으면
        if(base.splitNum>0)
        {
            //분열지점 세팅
            string id = id_proj;
            base.splitPoint = myTransform.position;
            float splitWeight = 0.6f + splitNum * 0.1f;

            int splitCount = (splitNum>=3)?7:(splitNum*2+1);   //7/5/3/0;

            for (int i=0;i<splitCount;i++)
            {
                Projectile proj = ProjPoolManager.ppm.GetFromPool(id);       // 풀에서 생성
                
                // 맞고 살아있으면 공격 불가 대상으로 지정
                if( !target_hit.GetComponent<Enemy>().isDead)
                {
                    proj.target_unattackable = target_hit;     // 현재 타격 대상을 공격불가로 지정 (분열되자마자 피해입히는 것을 방지하기 위해) 다음 충돌에는 풀어줄수있도록함.                     
                }
                
                // 풀에서 생성한 투사체 세팅 
                proj.SetUp(damage* splitWeight, speed, scale *splitWeight, projNum, penetration, splitNum-1 , lifeTime);    // 탄환 특성에 따라 능력치에 가중치가 붙음
                proj.SetSpecialStat(explosionLevel -1, weight_critDamage, knockBackPower * splitWeight);
                proj.myTransform.position = splitPoint;               // 위치는 splitPoint
                proj.myTransform.rotation = myTransform.rotation;       // 각도는 기본 투사체의 각도
                proj.RotateProj(splitAngle[i] + Random.Range(-10f, 10f));         // 분열각도에 맞게 발사
                proj.Action();
            }
        }
    }

    public override void ProjDestroy_custom()
    {
        
    }
}
