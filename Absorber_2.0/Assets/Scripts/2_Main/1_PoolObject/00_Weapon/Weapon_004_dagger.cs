using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_004_dagger : Weapon
{
    public GameObject prefab_proj;
    
    Vector3 attackDir;  // 공격 방향
    
    Transform hand;


    // =========== 오버라이드 =============
    // 필수정보초기화
    // ===================================
    protected override void InitEssentialInfo_weapon()
    {
        base.weaponName = "단검";
        base.id_weapon = "004";


        isSingleTarget = true;
        isRotatable = false;
    }

    // =========== 오버라이드 =============
    // 능력치 초기화/값 반영 
    // ===================================
    public override void InitWeapon_custom()
    {
        //계수 초기화
        weight_damage = 1.5f;
        weight_range = 1f;
        weight_attackSpeed = 0.8f;
        weight_critDamage = 2f;

        //주 능력치
        damageT      = 8    ;
        rangeT       = 5     ;   
        attackSpeedT = 2f ;

        //부 능력치
        penetrationT = 987654321;
        scaleT       = 1f;
        projLifeTimeT    = -1f;
        projSpeedT       = 10f; 
        knockBackPowerT = 2f;

        //특 능력치
        splitNumT    = 0    ;
        projNumT     = 1    ; 
        // 기타 초기화 작업
    }    

    // ========= 오버라이드 ==============
    // 개별 공격 함수 - 이펙트 프리팹 생성
    // ===================================
    public override void Attack_custom()
    {   
        // 회전 후 고정 
        target = list_targets[0];


        // 타겟이 무기보다 왼쪽에 있는 경우 스프라이트 뒤집기
        Vector3 dir = target.position - handTransform.position;
        targetOnLeft = dir.x<0;    // 타겟이 왼쪽에 있는지 
        // spriter.flipX = targetOnLeft; // 타겟이 왼쪽에 있으면 플립;
        handTransform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        
        
        StartCoroutine(Stab());
    }


    // ==================================
    // 근접공격 코루틴 - 투사체수만큼 공격 (list_targets에 있는 적들을 순서대로 공격)
    // ===================================
    IEnumerator Stab()
    {
        Transform target = list_targets[0];
        for (int i=1;i<projNumT+1;i++)
        {   
            audioSource.PlayOneShot(audioSource.clip);
            
            animator.SetTrigger("attack");  // 자연스러운 애니메이션

            Vector3 firePoint = Player.player.transform.position + Vector3.up * 2 * (i%2);
            bool flip = (i%2==0)? true:false;
            spriter.flipX = flip;
            if (targetOnLeft) 
            {
                spriter.flipX = !spriter.flipX;
            }
                

            attackDir = (target.position - firePoint).normalized;

            // 효과생성
            string id = id_weapon;
            Projectile proj = ProjPoolManager.ppm.GetFromPool(id);
            proj.InitProj(this, firePoint, target);    
            proj.RotateProj(Projectile.ProjDir.up);
            proj.Action();
            
            yield return new WaitForSeconds(0.2f);
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
