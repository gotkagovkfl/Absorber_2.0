using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Something_6100_stageDust : Something
{
    protected override void InitEssentialInfo_something()
    {
        _id_something = "6100";
    }

    // 개별 능력치 초기화 
    public override void InitSomething_custom(Vector3 targetPos)
    {
        pos = targetPos;

        offset = Vector3.zero;
 
        speed = 0f;
        lifeTime = -1;

        EventManager.em.onStageChange.AddListener( ()=> _isDead = true );   // 특수효과 제거
    }

    // 개별 초기화 
    public override void ActionSomething_custom()
    {
        // rb.velocity = dir * speed;
    }
    //=================================================================
}
