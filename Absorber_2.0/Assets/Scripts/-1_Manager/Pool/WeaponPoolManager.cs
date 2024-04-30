using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPoolManager : PoolManager<Weapon>
{
    public static WeaponPoolManager instance;  // 투사체 풀 매니저 객체 



    // //==================================================================================================
    // protected override void Awake()
    // {
    //     base.Awake();
    //     wpm = this;
    // }

    protected override void Init_custom()
    {
        id_category = PoolType.weapon;
        instance = this;
    }


    // public override void SetDir()
    // {
    //     dir = "Prefabs/Weapons";
    // }

    //============================================================================================

    // // 투사체 풀링 사전에 추가 : id 중복되면 안됨 ㅋ
    // public override string GetId(Weapon weapon)
    // {
    //     return weapon.id_weapon;
    // }

    //=========================== get & take ==================
    // Pool한 obj 별 초기화    
    public override void GetFromPool_custom(Weapon weapon)
    {

    }


    //=========================================================
    // 풀에 반납
    //========================================================
    public override void TakeToPool_custom(Weapon weapon)
    {

    }

}
