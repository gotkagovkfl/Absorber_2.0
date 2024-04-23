using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Something_0002_enterPortal : Something
{
    protected override void InitEssentialInfo_something()
    {
        _id_something = "0002";
    }

    // 개별 능력치 초기화 
    public override void InitSomething_custom(Vector3 targetPos)
    {        
        pos = targetPos;
        offset = Vector3.zero;
 
        speed = 0f;
        lifeTime = 2;
    }
    
    // 개별 초기화 
    public override void ActionSomething_custom()
    {
        // rb.velocity = dir * speed;
    }
}
