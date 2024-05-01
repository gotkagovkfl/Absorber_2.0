using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=============================================
// 장판형 무기 - 지면에 방패를 내리 꽂아 주변에 충격을 준다. 
//=============================================
public class Weapon_005_Aegis : Weapon
{

    // =========== 오버라이드 =============
    // 필수정보초기화
    // ===================================
    protected override void InitEssentialInfo_weapon()
    {
        base.weaponName = "방패";
        base.id_weapon = "005";

        isSingleTarget = false;
    }

    // =========== 오버라이드 =============
    // 능력치 초기화/값 반영                        -- 투사체 수만큼 
    // ===================================
    public override void InitWeapon_custom()
    {
        //계수 초기화
        weight_damage = 2f;
        weight_range = 1f;
        weight_attackSpeed = 1f;
        weight_critDamage = 1.5f;
        
        //주 능력치
        damageT      = 10f ; 
        rangeT       = 6    ;  
        attackSpeedT = 1f ; 

        //부 능력치
        penetrationT = 987654321;   
        scaleT       = 1.5f;    
        projLifeTimeT    = -1f;
        projSpeedT       = 20f; 
        knockBackPowerT = 4f;

        //특 능력치
        splitNumT    = 0 ;
        projNumT     = 1 ; 

        // 기타 초기화 작업
        // onDestroyWeapon();
        // list_targets.Clear();  // 그리고 타겟 리스트 초기화

        // attackSpeedT += projNumT;     // 공속이 투사체수 영향받음
        
    }    

    // =========== override =============
    // 개별 공격 함수 - 플레이이어 위치에 장판 생성( 한번 생성되면 끝) 
    // ===================================
    public override void Attack_custom()
    {
        audioSource.PlayOneShot(audioSource.clip);
        StartCoroutine(Shockwave());
    }  


    // ===================================
    // testCircle은 투사체 수가 공속에 영향을 줌
    // ===================================
    IEnumerator Shockwave()
    {
        for (int i=0;i<projNumT;i++)
        {
            audioSource.PlayOneShot(audioSource.clip);
            
            string id = id_weapon;
            Projectile proj = ProjPoolManager.instance.GetFromPool(id);
            proj.SetUp(this);
            proj.myTransform.position = new Vector3(transform.position.x, Player.player.t_player.position.y);

            
            proj.Action();

        
            yield return new WaitForSeconds(0.1f);
        }
    }


    //============= 오버라이드 ==================
    // 무기 파괴시 생성된 장판을 파괴해야함.
    //=======================================
    public override void onDestroyWeapon()
    {

        // Destroy(proj);
    }
}
