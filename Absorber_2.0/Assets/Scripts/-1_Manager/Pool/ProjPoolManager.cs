using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ============================================================
// 플레이어 투사체 풀 매니저 
//==============================================================
public class ProjPoolManager : PoolManager<Projectile>
{
    public static ProjPoolManager instance;  // 투사체 풀 매니저 객체 
    
    // protected override void Awake()
    // {
    //     base.Awake();
    //     ppm = this;
    // }
    protected override void Init_custom()
    {
        id_category = PoolType.proj;
        instance = this;
    }

    // public override void SetDir()
    // {
    //     dir = "Prefabs/W/Projectiles";
    // }

    //============================================================================================

    // 투사체 풀링 사전에 추가 : id 중복되면 안됨 ㅋ
    // public override string GetId(Projectile proj)
    // {
    //     return proj.id_proj;
    // }

    //=========================== get & take ==================
    // Pool한 obj 별 초기화    
    public override void GetFromPool_custom(Projectile proj)
    {
        proj.active = true;
        proj.isAlive = true;
    }


    //=========================================================
    // 풀에 반납
    //========================================================
    public override void TakeToPool_custom(Projectile proj)
    {
        if (!proj.isAlive)
        {
            return;   
        }
        //
        proj.active = false;
        proj.isAlive = false;
        proj.CancelInvoke("ProjDestroy");
        // proj.myTransform.position = transform.position;     // 총알 모으기
    }



}

