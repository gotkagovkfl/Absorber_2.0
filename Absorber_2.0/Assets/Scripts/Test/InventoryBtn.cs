using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBtn : MonoBehaviour
{
    public GameObject weapon;

    public void OnClick()
    {
        InventoryManager.im.ChangeWeapon(weapon);
    }
}
