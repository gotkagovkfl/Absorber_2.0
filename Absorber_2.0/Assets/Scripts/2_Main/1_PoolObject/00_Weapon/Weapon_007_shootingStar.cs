using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_007_shootingStar : Weapon
{
    // 능력치

    float offsetY = 20f;            // 별똥별이 소환될 y좌표 오프셋

    // =========== 오버라이드 =============
    // 필수정보초기화
    // ===================================
    protected override void InitEssentialInfo_weapon()
    {
        base.weaponName = "별똥별";
        base.id_weapon = "007";

        base.isSingleTarget = false;
    }


    // =========== 오버라이드 =============
    // 능력치 초기화/값 반영 
    // ===================================
    public override void InitWeapon_custom()
    {
        //계수 초기화
        weight_damage = 2.25f;
        weight_range = 1f;
        weight_attackSpeed = 1f;
        weight_critDamage = 1.5f;

        
        //주 능력치
        damageT      = 15
    ;
        rangeT       = 10  ;   
        attackSpeedT = 0.5f ;

        //부 능력치
        penetrationT = 987654321 ;
        scaleT       = 1f  ;
        projLifeTimeT    = -1f;
        projSpeedT       = 20f; 
        knockBackPowerT = 10f;

        //특 능력치
        splitNumT    = 0    ;
        projNumT     = 1    ; 

        // 기타 초기화 작업
        offsetY = 20f;            // 별똥별이 소환될 y좌표 오프셋

    }    
    

    // =========== 오버라이드 =============
    // 개별 공격 함수 : 우선 공격 목표지점을 생성하고, 일정 시간후 해당 지점에 운석을 떨어뜨린다.
    // ===================================
    public override void Attack_custom()
    {        
        StartCoroutine(Coroutine_Fire());
    } 


    // ==================================
    // 별똥별 공격 코루틴 - 타겟들에 대해 타겟지점 생성
    // ===================================
    IEnumerator Coroutine_Fire()
    {
        
        // 타겟 참조는 역순으로 해야함 (중간에 타겟이 삭제될 수 있기 때문)
        for (int i = 0; i<list_targets.Count;i++)
        {
            if (i<list_targets.Count)// 예외처리 : 없어도 작동은 하는데 거슬림
            {
                audioSource.PlayOneShot(audioSource.clip);
                
                Transform target = list_targets[i];
                
                Fire(target);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    // ================================
    // 별똥별 떨구기  떨구기
    // ===================================
    void Fire(Transform target)
    {          
        // 깨끗하게 하니 오브젝트가 이상하게 생성되는 버그가 있어서 이렇게 더럽게 해놨음.
        // 좌표 설정
        Vector3 firePoint = target.position + new Vector3(Random.Range(-10,10),offsetY); // 하늘에서 유성소환,  x offset은 화면 크기를 참고할것


        // 필요 오브젝트들 생성 후 셋업
        Projectile proj_hitPoint = ProjPoolManager.instance.GetFromPool(id_weapon+"_p");
        proj_hitPoint.SetUp(this);

        Projectile proj = ProjPoolManager.instance.GetFromPool(id_weapon);
        proj.SetUp(this);
        
        // 위치설정
        Vector3 targetHitPoint = target.position;
    
        // 최대 사거리 
        if(!Player.player.autoAim)
        {
            if ( !InRange(targetHitPoint) )
            {
                targetHitPoint = GetMaximumRangePos(targetHitPoint);
                // EffectPoolManager.epm.CreateText(firePoint, " Out", Color.green );
            }
        }
        proj_hitPoint.myTransform.position = targetHitPoint;
        proj.myTransform.position = firePoint;
        
        // 타겟, 방향 설정
        proj_hitPoint.SetTarget(proj.myTransform);

        proj.SetTarget(proj_hitPoint.myTransform);
        proj.SetDirection(proj_hitPoint.myTransform);
         
        // 액션
        proj_hitPoint.Action();

        proj.RotateProj(Projectile.ProjDir.up);
        proj.Action();
        proj.active = false;
    }


    
    //============= 오버라이드 ==================
    // 무기 파괴시 딱히 할거 없음
    //=======================================
    public override void onDestroyWeapon()
    {

    }
}
