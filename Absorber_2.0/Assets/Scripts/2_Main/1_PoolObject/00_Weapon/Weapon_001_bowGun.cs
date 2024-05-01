using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//=================== 자식클래스 ========================
// 테스트무기 - 원거리 : 가장 가까운 적에게 총알을 발사하는 원거리 무기
//======================================================
public class Weapon_001_bowGun : Weapon
{
 

    // =========== 오버라이드 =============
    // 필수정보초기화
    // ===================================
    protected override void InitEssentialInfo_weapon()
    {
        weaponName = "석궁";
        id_weapon = "001";

        isSingleTarget = true;
        isRotatable = true;
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
        weight_critDamage = 2f;
        
        //주 능력치
        damageT      = 9 ;
        rangeT       = 10;   
        attackSpeedT = 1f;

        //부 능력치
        penetrationT = 2;
        scaleT       = 1f;
        projLifeTimeT   = 3f;   
        projSpeedT     = 15f;  
        knockBackPowerT = 4f;

        //특 능력치
        splitNumT    = 0 ;
        projNumT     = 1; 

        

        //기타 초기화 작업
        
    }

    // =========== 오버라이드 =============
    // 개별 공격 함수
    // ===================================
    public override void Attack_custom()
    {                
        // 총알 생성
        StartCoroutine(Fire());
    } 

    // ==================================
    // 총알 생성 코루틴 - 투사체수만큼 총알생성 (정확히 일직선으로 나가지 않게 조정)
    // ===================================
    IEnumerator Fire()
    {
        yield return new WaitForSeconds(animationLength*0.6f);   // 자연스러운 연출을 위함 

        Transform target = list_targets[0];
        for (int i=0;i<projNumT;i++)
        {
            audioSource.PlayOneShot(audioSource.clip);
            
            Transform transform_muzzle = transform.GetChild(0);             // 총구 위치 정보 

            // 총알 생성 
            Projectile proj =  ProjPoolManager.instance.GetFromPool(id_weapon);
            proj.InitProj(this, transform_muzzle.position, target);           // 새로 생긴 투사체 초기화 
            proj.RotateProj(Projectile.ProjDir.up);
            float extraAngle = Random.Range(-2.5f, 2.5f);   
            proj.RotateProj(extraAngle);    // 디테일 : 오차각도 5도
            proj.Action();

            // 각도 함수 필요
            yield return new WaitForSeconds(0.05f);
        }
    }


    //============= 오버라이드 ==================
    // 무기 파괴시 딱히 할거 없음
    //=======================================
    public override void onDestroyWeapon()
    {

    }
}
