using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//=================== 자식클래스 ========================
// 테스트무기 - 빙빙 : 플레이어 주위를 빙빙 도는 논타겟 무기 : 
//======================================================
public class Weapon_009_petals: Weapon
{
     
    // =========== 오버라이드 =============
    // 필수정보초기화
    // ===================================
    protected override void InitEssentialInfo_weapon()
    {
        base.weaponName = "벚꽃";
        base.id_weapon = "009";

        isSingleTarget = false;
    }

    // =========== 오버라이드 =============
    // 능력치 초기화/값 반영 
    // ===================================
    public override void InitWeapon_custom()
    {
        //계수 초기화
        weight_damage = 2f;
        weight_range = 1f;
        weight_attackSpeed = 1f;
        weight_critDamage = 2f;
        
        //주 능력치
        damageT      = 8 ;
        rangeT       = 10f    ;   
        attackSpeedT = 0.125f   ;

        //부 능력치
        penetrationT = 987654321 ;
        scaleT       = 1f   ;
        projLifeTimeT    = 0.95f/attackSpeedT;     // 
        projSpeedT       = 200f; 
        
        knockBackPowerT = 4f;

        //특 능력치
        splitNumT    = 0   ;
        projNumT     = 3   ; 

        // 기타 초기화 작업
    }   
 

    // =========== 오버라이드 =============
    // 개별 공격 함수
    // ===================================
    public override void Attack_custom()
    {
                 //
        // notAvailable = true;        // 영구지속 
        // 기존 무기 파괴
        onDestroyWeapon();     

        audioSource.PlayOneShot(audioSource.clip);
        // 그리고 투사체 배치 ( 투사체 수에 따라 배치가 달라짐. 기본 2개)
        ArrangeProj();
    } 

    // ===================================
    // 회전하는 투사체 배치 - 투사체 수에 따라 투사체 배치
    // ===================================
    public void ArrangeProj()
    {
        if (projNumT==0)
        {
            return;
        }
        
        float rotationPerUnit = 360/projNumT;       // 투사체 당 각도
        float currRotation = 0;
        for (int i=0;i<base.projNumT;i++)
        {
            string id = id_weapon;
            Projectile proj = ProjPoolManager.ppm.GetFromPool(id);

            proj.SetUp(this);
            proj.SetTarget(Player.Instance.myTransform);       // 플레이어 주위를 회전함 .
            proj.targetProj = proj;
            proj.mainTransform = proj.myTransform;
            proj.rotAngle = currRotation;

            proj.Action();

            currRotation+=rotationPerUnit;
        }
    }


    //============= 오버라이드 ==================
    // 무기 파괴시 생성된 회전의 중심을 파괴해야함
    //=======================================
    public override void onDestroyWeapon()
    {
        notAvailable = false;

        // 현재 생성된 투사체들 파괴
        Projectile[] projs = ProjPoolManager.ppm.transform.GetComponentsInChildren<Projectile>();
        for (int i=0;i< projs.Length;i++)
        {
            if (projs[i].id_proj == id_weapon)
            {
                ProjPoolManager.ppm.TakeToPool(projs[i]);
            }
        }

    }
    
}
