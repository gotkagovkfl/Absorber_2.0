using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_002_WeaponSwap : MonoBehaviour
{

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.im.OpenInventory(true);
        }
    }
}
