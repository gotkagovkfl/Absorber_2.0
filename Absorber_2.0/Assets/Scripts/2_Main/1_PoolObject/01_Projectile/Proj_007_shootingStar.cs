using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj_007_shootingStar :Projectile
{
   // =========== 오버라이드 =============
    // 필수 정보 초기화 
    // ===================================    
    protected override void InitEssentialInfo_proj()
    {
        id_proj = "007";

        splitWhenHit= false;
        shrinkOnDes =true;

    }


    // =========== 오버라이드 =============
    // 초기설정 후 액션 (행동 개시) - 적 방향으로 머리를 회전시킨후 이동한다.  
    // ===================================
    public override void Action_custom()
    {
        GetComponent<TrailRenderer>().enabled = true;
        rb.velocity = myTransform.up * base.speed;
    }


    // =========== 오버라이드 =============
    // 분열   : 별똥별은 타격지점에서 랜덤한 방향으로 분열함. 
    // ===================================
    public override void Split()
    {
        // 분열 횟수가 남아있으면
        if(splitNum>0)
        {
            //분열지점 세팅
            string id = id_proj;
            base.splitPoint = myTransform.position;
            float splitWeight = 0.5f;

            int splitCount = splitNum*2;  //6/4/2

            for (int i=0;i<splitCount;i++)
            {
                Projectile proj = ProjPoolManager.instance.GetFromPool(id);       // 풀에서 생성
                
                lifeTime = 1f;
                // 풀에서 생성한 투사체 세팅 
                proj.SetUp(damage* splitWeight, speed, scale *splitWeight, projNum, penetration, 0 , lifeTime);    // 탄환 특성에 따라 능력치에 가중치가 붙음
                proj.SetSpecialStat(explosionLevel -1 , weight_critDamage , knockBackPower* splitWeight);
                proj.myTransform.position = splitPoint;               // 위치는 splitPoint

                float angle = Random.Range(0,360);  // 랜덤각도
                proj.RotateProj(angle);         // 분열각도에 맞게 발사
                proj.Action();
            }
        }
    }


    public override void ProjDestroy_custom()
    {
        GetComponent<TrailRenderer>().enabled = false;
    }
}
