using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//=================== Weapon ========================
// sawed-off : 단총신 산탄총
// 
//======================================================
public class Weapon_002_sawedOff : Weapon
{

    float spreadAngle =10;       // 개별 탄 퍼짐 각도 

    public float spreadAngleT
    {
        get
        {
            return spreadAngle - 0.5f * projNumT;
        }
    }

    
    // =========== 오버라이드 =============
    // 필수정보초기화
    // ===================================
    protected override void InitEssentialInfo_weapon()
    {
        base.weaponName = "샷건";
        base.id_weapon = "002";

        isSingleTarget = true;
        isRotatable = true;
    }

    // =========== override =============
    // Init basic weapon status
    // ===================================
    public override void InitWeapon_custom()
    {
        //계수 초기화
        weight_damage = 1.25f;
        weight_range = 1f;
        weight_attackSpeed = 1f;
        weight_critDamage = 1.5f;
        
        //주 능력치
        damageT      = 5f ;
        rangeT       = 10;   
        attackSpeedT = 0.8f;

        //부 능력치
        penetrationT = 0;
        scaleT       = 1f;
        projLifeTimeT   = 0.6f;     // 탄알 수명이 짧음
        projSpeedT     = 20f;  

        knockBackPowerT = 8f;

        //특 능력치
        splitNumT    = 0 ;
        projNumT     = 4; 

        //기타 초기화 작업
        
        
    }

    // =========== override =============
    // attack 
    // ===================================
    public override void Attack_custom()
    {
        audioSource.PlayOneShot(audioSource.clip);
        
        StartCoroutine( ArrangeProj() );
    }

    // ===================================
    // 탄 퍼짐 
    // ===================================
    public IEnumerator ArrangeProj()
    {
        

        Transform target = list_targets[0];     // 타겟 위치정보
        
        Transform transform_muzzle = transform.GetChild(0);             // 총구 위치 정보 

        float TotalspreadAngle = spreadAngleT * ( projNumT -1 );        // 탄퍼짐 각도
        float currRotation = -TotalspreadAngle/2;         // 총알 배치 시작 각도

        for (int i=0;i<projNumT;i++)
        {
            // 총알생성
            Projectile proj =  ProjPoolManager.instance.GetFromPool(id_weapon);
            proj.InitProj(this, transform_muzzle.position, target);
            proj.RotateProj(Projectile.ProjDir.up);
            proj.RotateProj(currRotation + Random.Range(-1.5f,1.5f)); //탄퍼짐 + 디테일
            proj.Action();

            currRotation += spreadAngleT;
        }
        yield return new WaitForFixedUpdate();
    }

    public override void onDestroyWeapon()
    {

    }
}
