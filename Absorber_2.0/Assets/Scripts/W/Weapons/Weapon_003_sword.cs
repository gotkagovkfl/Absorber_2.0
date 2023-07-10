using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=================== 자식클래스 ========================
// 테스트무기 - 근접 : 가장 가까운 적을 직접 타격하는 근접무기
//======================================================
public class Weapon_003_sword: Weapon
{    
    Vector3 attackDir;  // 공격 방향
        


    // =========== 오버라이드 =============
    // 필수정보초기화
    // ===================================
    protected override void InitEssentialInfo_weapon()
    {
        base.weaponName = "검";
        base.id_weapon = "003";

        
        isSingleTarget = false;
        isRotatable = false;
    }

    // =========== 오버라이드 =============
    // 능력치 초기화/값 반영 
    // ===================================
    public override void InitWeapon_custom()
    {
        //계수 초기화
        weight_damage = 1.8f;
        weight_range = 1f;
        weight_attackSpeed = 1f;
        weight_critDamage = 1.5f;
        
        //주 능력치
        damageT      = 8;
        rangeT       = 10    ;   
        attackSpeedT = 1.5f ;

        //부 능력치
        penetrationT = 987654321 ;
        scaleT       = 1f ;
        projLifeTimeT    = 3f;
        projSpeedT       = 12f; 
        knockBackPowerT = 2.5f;
        

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
        // handTransform.rotation = Quaternion.identity;
        StartCoroutine(Slash());
    }


    // ==================================
    // 근접공격 코루틴 - 투사체수만큼 공격 (list_targets에 있는 적들을 순서대로 공격)
    // ===================================
    IEnumerator Slash()
    {
        // 타겟 참조는 역순으로 해야함 (중간에 타겟이 삭제될 수 있기 때문)
        for (int i = list_targets.Count-1; i>=0;i--)
        {
            audioSource.PlayOneShot(audioSource.clip);

            // 회전 후 고정 
            target = list_targets[0];
            // 타겟이 무기보다 왼쪽에 있는 경우 스프라이트 뒤집기
            Vector3 dir = target.position - handTransform.position;
            handTransform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            
            animator.SetTrigger("attack");      // 자연스러운 모션을 위함
            if (i<list_targets.Count)// 예외처리 : 없어도 작동은 하는데 거슬림
            {
                Transform target = list_targets[i];
                if (target==null)
                {
                    break;
                }
                attackDir = (target.position - Player.Instance.myTransform.position).normalized;

                // 효과생성
                string id = id_weapon;
                Vector3 firePoint = Player.Instance.myTransform.position + Vector3.up; 

                Projectile proj = ProjPoolManager.ppm.GetFromPool(id);
                proj.InitProj(this, firePoint, target);

                proj.RotateProj(Projectile.ProjDir.up);
                float extraAngle = Random.Range(-5f, 5f);   
                proj.RotateProj(extraAngle);    // 디테일 : 오차각도 5도
                proj.Action();

            }

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
