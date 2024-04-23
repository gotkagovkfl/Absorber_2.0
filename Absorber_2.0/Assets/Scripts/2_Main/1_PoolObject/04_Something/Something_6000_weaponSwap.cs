using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Something_6000_weaponSwap : Something
{
    protected override void InitEssentialInfo_something()
    {
        _id_something = "6000";
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
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.im.OpenInventory(true);
        }
    }
}
