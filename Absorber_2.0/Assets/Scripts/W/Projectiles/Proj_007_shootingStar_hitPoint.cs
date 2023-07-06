using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj_007_shootingStar_hitPoint : Projectile
{   
    float distGoal = 0.5f;
    
    // =========== 오버라이드 =============
    // 필수 정보 초기화 
    // ===================================    
    public override void InitEssentialProjInfo()
    {
        id_proj = "007_p";

        splitWhenHit= false;
        shrinkOnDes =false;

    }


    // =========== 오버라이드 =============
    // 충돌감지 코루틴 활성화
    // ===================================
    public override void Action_custom()
    {        
        rb.simulated = false;
        StartCoroutine(DetectCollision());
    }

    //
    public IEnumerator DetectCollision()
    {
        WaitForFixedUpdate wf = new WaitForFixedUpdate();
        float sqrDist = 987654321f;
        while (sqrDist > distGoal)
        {
            // target과 거리계산해서 목표 범위 안이면 폭발
            Vector3 dist = target.position - myTransform.position;
            sqrDist = dist.sqrMagnitude;
            // 타겟이 사라지면 자기자신도 사라짐
            Projectile proj = target.GetComponent<Projectile>();
            if (proj !=null && !proj.isAlive)
            {
                // StopCoroutine(DetectCollision());
                ProjDestroy(0);
                break;
            }

            yield return wf;
        }
        // 감지 루프를 벗어나면 폭발
        StarExplode();
    }

    // ====================================
    // 폭발 : 유성과 충돌시 폭발하며 피해 - 꺼져있던 리지드바디 on
    // ===================================
    public void StarExplode()
    {
        audioSource.PlayOneShot(audioSource.clip);
        
        Projectile proj = target.GetComponent<Projectile>();
        if (proj !=null)
        {
            proj.Split();               // 별똥별은 폭발시 분열됨. 
            proj.rb.velocity = new Vector3(0,0,0);
            proj.lifeTime = 0.5f;
            proj.SetLifeTime();
        }       
        rb.simulated = true;    // 콜라이더 on
        //애니메이션 재생후 오브젝트 제거 
        animator.SetTrigger("explode");
        

        lifeTime = 0.75f;
        SetLifeTime();
    }   


    // =========== 오버라이드 =============
    // 분열   : 타격지점은 분열 암함. 
    // ===================================
    public override void Split()
    {

    }



    //=========== Animation Event ===========
    //
    public void SetSortingLayerFront()
    {
        spriter.sortingOrder=4;
    }
    public void SetSortingLayerBack()
    {
        spriter.sortingOrder=0;
    }


    public override void ProjDestroy_custom()
    {
        
    }
}
