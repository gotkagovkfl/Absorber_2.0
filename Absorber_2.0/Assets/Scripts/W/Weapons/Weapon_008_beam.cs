using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_008_beam : Weapon
{
  // =========== 오버라이드 =============
    // 필수정보초기화
    // ===================================
    public override void InitEssentialWeaponInfo()
    {
        weaponName = "광선";
        id_weapon = "008";

        isSingleTarget = false;
    }


    // =========== 오버라이드 =============
    // 능력치 초기화/값 반영 
    // ===================================
    public override void InitWeapon_custom()
    {
        //계수 초기화
        weight_damage = 1f;
        weight_range = 1f;
        weight_attackSpeed = 1f;
        weight_critDamage = 1.5f;
        
        //주 능력치
        damageT      = 4
    ;
        rangeT       = 20     ;   
        attackSpeedT = 1f ;

        //부 능력치
        penetrationT = 987654321 ;
        scaleT       = 1f  ;
        projLifeTimeT    = 0.5f;
        projSpeedT       = 20f; 
        knockBackPowerT = 0.5f;

        //특 능력치2
        splitNumT    = 0    ;
        projNumT     = 1    ; 

        // 기타 초기화 작업
    }    
    

    // =========== 오버라이드 =============
    // 개별 공격 함수 : 대상에게 일직선의 광선을 발사한다. 
    // ===================================
    public override void Attack_custom()
    {        
        StartCoroutine(Fire());
        



        // Projectile proj = ProjPoolManager.ppm.GetFromPool(id_weapon);

        // proj.InitProj(this, transform.position, target);           
        // proj.RotateProj(Projectile.ProjDir.up);
        
        // proj.SetFollowingTarget(transform, Vector3.zero);
        // proj.FollowTarget();

        // proj.Action();
    } 

    public IEnumerator Fire()
    {
                // 타겟 참조는 역순으로 해야함 (중간에 타겟이 삭제될 수 있기 때문)
        // Debug.Log(list_targets.Count);
        for (int i = 0; i< list_targets.Count; i++)
        {
            if (i<list_targets.Count)// 예외처리 : 없어도 작동은 하는데 거슬림
            {
                audioSource.PlayOneShot(audioSource.clip);
                
                Transform target = list_targets[i];
                if (target==null)
                {
                    break;
                }


                // 효과생성
                string id = id_weapon;
                Vector3 firePoint = transform.position;

                Projectile proj = ProjPoolManager.ppm.GetFromPool(id);
                proj.InitProj(this, firePoint, target);

                proj.RotateProj(Projectile.ProjDir.up);

                // proj.SetFollowingTarget(transform, Vector3.zero);
                // proj.FollowTarget();
                   
                proj.Action();

            }

            yield return new WaitForSeconds(0.05f);
            // yield return null;
            // yield return null;
        }
    }


    
    //============= 오버라이드 ==================
    // 무기 파괴시 딱히 할거 없음
    //=======================================
    public override void onDestroyWeapon()
    {
        base.notAvailable = false;
    }
}
