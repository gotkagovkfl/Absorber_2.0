using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=============================================
//장판형 무기 : 적의 위치에 일정시간 지속하는 마법진을 소한하여 마법진 위의 적에게 지속적인 피해를 입힌다. 
//=============================================
public class Weapon_006_magicCircle : Weapon
{


    // =========== 오버라이드 =============
    // 필수정보초기화
    // ===================================
    protected override void InitEssentialInfo_weapon()
    {
        base.weaponName = "마법진";
        base.id_weapon = "006";

        base.isSingleTarget = false;
    }

    // =========== 오버라이드 =============
    // 능력치 초기화/값 반영                        -- 투사체 수만큼 
    // ===================================
    public override void InitWeapon_custom()
    {
        //계수 초기화
        weight_damage = 1.25f;
        weight_range = 1f;
        weight_attackSpeed = 1f;
        weight_critDamage = 1.5f;
        
        //주 능력치
        damageT      = 6; 
        rangeT       = 6    ;  
        attackSpeedT = 0.7f ; 

        //부 능력치
        penetrationT = 987654321;   
        scaleT       = 1f;    
        projLifeTimeT    = -1f;
        projSpeedT       = 1f; 
        knockBackPowerT = 1f;

        //특 능력치
        splitNumT    = 0 ;
        projNumT     = 1 ; 

        // 기타 초기화 작업 
    }    

    // =========== 오버라이드 =============
    // 개별 공격 함수 - 적 위치에 장판 생성
    // ===================================
    public override void Attack_custom()
    {                
        StartCoroutine(SpawnCircle());
    }  

    // ===================================
    // 원 생성 - 적위치에 생성
    // ===================================
    IEnumerator SpawnCircle()
    {
        for (int i = 0; i<list_targets.Count;i++)
        {
            if (i<list_targets.Count)// 예외처리 : 없어도 작동은 하는데 거슬림
            {
                audioSource.PlayOneShot(audioSource.clip);
                
                Transform target = list_targets[i];
                if (target==null)
                {
                    break;
                }

                Vector3 firePoint  = target.position;

                // 최대 사거리 
                if(!Player.Instance.autoAim)
                {
                    if ( !InRange(firePoint) )
                    {
                        firePoint = GetMaximumRangePos(firePoint);
                        // EffectPoolManager.epm.CreateText(firePoint, " Out", Color.green );
                    }
                }

                // 효과생성
                string id = id_weapon;
                float xOffset = Random.Range(-1f,1f);
                float yOffset = Random.Range(-1f,1f);
                Vector3 offset = new Vector3(xOffset, yOffset, 0);      // offset 은 좀더 자연스러움을 위함 
                offset = Vector3.zero;

                Projectile proj = ProjPoolManager.ppm.GetFromPool(id);
                proj.InitProj(this,firePoint + offset, target);
                proj.Action();
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    //============= 오버라이드 ==================
    // 무기 파괴시 생성된 장판을 파괴해야함.
    //=======================================
    public override void onDestroyWeapon()
    {
        base.notAvailable = false;
    }
}
