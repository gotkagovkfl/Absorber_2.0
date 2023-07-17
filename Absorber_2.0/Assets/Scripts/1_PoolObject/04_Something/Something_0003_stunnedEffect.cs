using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Something_0003_stunnedEffect : Something
{
    protected override void InitEssentialInfo_something()
    {
        _id_something = "0003";
    }

    // 개별 능력치 초기화 
    public override void InitSomething_custom(Vector3 targetPos)
    {        
        pos = targetPos;
        offset = Vector3.zero;
 
        speed = 0f;
        lifeTime = 5;
    }
    
    // 개별 초기화 
    public override void ActionSomething_custom()
    {
        offset = new Vector3 ( 0, targetToFollow.position.y - pos.y);
        // rb.velocity = dir * speed;
    }

    void Update()
    {
        if (targetToFollow !=null)
        {
            myTransform.position = targetToFollow.position  + offset;
        }
        
        if (enemy_d.isDead)
        {
            _isDead = true;
        }
    }
}
