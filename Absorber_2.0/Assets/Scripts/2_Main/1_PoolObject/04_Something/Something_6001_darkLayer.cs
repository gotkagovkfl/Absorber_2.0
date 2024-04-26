using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Something_6001_darkLayer : Something
{
    protected override void InitEssentialInfo_something()
    {
        _id_something = "6001";
    }

    // 개별 능력치 초기화 
    public override void InitSomething_custom(Vector3 targetPos)
    {
        pos = targetPos;

        offset = Vector3.zero;
 
        speed = 0f;
        lifeTime = -1;
    }

    // 개별 초기화 
    public override void ActionSomething_custom()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if (rb==null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        Vector3 dir = new Vector3( -Player.player.inputVector.x,0);
        rb.velocity = dir * Player.player.Speed * 0.6f;
    }
}
