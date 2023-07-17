using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Something_0004_bleedingEffect : Something
{

    protected override void InitEssentialInfo_something()
    {
        _id_something = "0004";
    }

    // 개별 능력치 초기화 
    public override void InitSomething_custom(Vector3 targetPos)
    {        
        pos = targetPos;
        offset = new Vector3( Random.Range(-0.7f,0.7f), Random.Range(-0.7f,0.7f));;
 
        speed = 0f;
        lifeTime = 0.25f;

    }
    
    // 개별 초기화 
    public override void ActionSomething_custom()
    {
        // rb.velocity = dir * speed;
    }
}
