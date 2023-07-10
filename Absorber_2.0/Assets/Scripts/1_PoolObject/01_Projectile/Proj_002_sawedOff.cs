using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj_002_sawedOff : Projectile
{
    
    // =========== 오버라이드 =============
    // 필수 정보 초기화 
    // ===================================    
    protected override void InitEssentialInfo_proj()
    {
        id_proj = "002";
        
        splitWhenHit= true;
        shrinkOnDes =true;
        SetSplitAngles(0,-135,135,-45,45,-90,90);

    }

    // =========== override =============
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
        
        rb.velocity = transform.up * speed;
    }


    // =========== 오버라이드 =============
    // 분열   
    // ===================================
    public override void Split()
    {
        // 분열 횟수가 남아있으면
        if(base.splitNum>0)
        {
            string id = id_proj;
            float splitWeight = 0.6f + 0.1f * splitNum;
            //분열지점 세팅
            base.splitPoint = transform.position;

            int splitCount = (splitNum>=3)?7:(splitNum*2+1);   //7/5/3/0;


            for (int i=0;i<splitCount;i++)
            {
                // 분열각도에 맞게 발사
                Projectile proj = ProjPoolManager.ppm.GetFromPool(id);

                // 맞고 살아있으면 공격 불가 대상으로 지정
                if( !target_hit.GetComponent<Enemy>().isDead)
                {
                    proj.target_unattackable = target_hit;     // 현재 타격 대상을 공격불가로 지정 (분열되자마자 피해입히는 것을 방지하기 위해) 다음 충돌에는 풀어줄수있도록함.                     
                }


                proj.transform.position = splitPoint;               // 위치는 splitPoint
                proj.transform.rotation = transform.rotation;       // 각도는 기본 투사체의 각도

                proj.SetUp(damage* splitWeight, speed, scale *splitWeight, projNum, penetration, splitNum-1 , lifeTime);
                proj.SetSpecialStat(explosionLevel -1 , weight_critDamage , knockBackPower* splitWeight);
                proj.RotateProj(splitAngle[i] + Random.Range(-10f, 10f) ); 
                proj.Action();
            }
        }
    }
    public override void ProjDestroy_custom()
    {
        
    }
} 
