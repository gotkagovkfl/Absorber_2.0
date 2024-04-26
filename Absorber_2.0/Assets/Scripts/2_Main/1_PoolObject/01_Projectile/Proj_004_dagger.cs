using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//=======================================================
// dagger 의 투사체 : 스프라이트가 오른쪽 방향 보고있음
//=======================================================
public class Proj_004_dagger : Projectile
{
    public bool targetIsLeft = false;   // 타겟이 무기보다 왼쪽에 있는지 (이때 스프라이트를 뒤집어야함)

    Vector3 firstTargetPos;

    Transform transform_sp;

    // =========== 오버라이드 =============
    // 필수 정보 초기화 
    // ===================================    
    protected override void InitEssentialInfo_proj()
    {
        id_proj = "004";
        
        splitWhenHit= false;
        shrinkOnDes =true;
        SetSplitAngles(-35,35,0,0,0,0,0);

    }


    // =========== 오버라이드 =============
    // 애니메이터 정보를 설정함.
    // ===================================
    public override void Action_custom()
    {
        if (Player.player.splitNum != splitNum)
        {
            rb.velocity = direction * speed;
        }        
        
        
        animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length; 
        lifeTime = animationLength;

        if (target != null)
        {
            firstTargetPos = target.position;
        }

        Invoke("Split", animationLength* 0.7f);             // 애니메이션이 반 진행되면 분열 일으킴      
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
            //분열지점 세팅
            splitPoint = myTransform.position;
            // if (target )
            // {
            //     splitPoint = myTransform.position;
            // }
            // else
            // {
            //     splitPoint = firstTargetPos;
            // }

            Vector3 offset = Vector3.up * scale;
            // offset = Vector3.zero;


            // 효과 생성하기 
            for (int i=0;i<2;i++)
            {   
                float splitWeight = 0.5f + 0.1f * splitNum;

                Projectile proj = ProjPoolManager.ppm.GetFromPool(id);
                proj.SetUp(base.damage* splitWeight, base.speed, base.scale *splitWeight, base.projNum,  base.penetration,  base.splitNum-1 , -1f);
                proj.SetSpecialStat(explosionLevel -1 , weight_critDamage , knockBackPower* splitWeight);

                proj.myTransform.position = splitPoint + offset * ((i==0)?1:-1);
                // proj.myTransform.rotation = transform.rotation;
                
                proj.SetTarget(target);                  // 나중에 오류 생길 수도 있긴함. 
                proj.SetDirection(target);
                proj.RotateProj(ProjDir.up);
                // proj.RotateProj( splitAngle[i]  + Random.Range(-10f, 10f));
                proj.Action();
            }
        }
    }
    public override void ProjDestroy_custom()
    {
        
    }
}
