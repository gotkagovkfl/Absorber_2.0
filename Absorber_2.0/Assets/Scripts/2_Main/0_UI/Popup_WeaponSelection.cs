using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_WeaponSelection : MonoBehaviour
{
    [SerializeField] RectTransform r_list;
    [SerializeField] GameObject prefab_btn_weapon;


    public void InitPopup()
    {
        // 미리 만들어져있는 버튼 부수기
        foreach(var btn in r_list.GetComponentsInChildren<Btn_WeaponSelection>())
        {
            Destroy(btn.gameObject);
        }
        
        
        // 버튼 만들기 
        foreach( var kv in PrefabManager.dic_prefabs[PoolType.weapon])
        {
            Weapon weapon = kv.Value.GetComponent<Weapon>();
            
            if (weapon.id_weapon.Equals("000"))
            {
                continue;
            }

            Btn_WeaponSelection btn = Instantiate(prefab_btn_weapon,r_list).GetComponent<Btn_WeaponSelection>();
            btn.Init(weapon);
        }

    }

    public void OpenPopup()
    {
        gameObject.SetActive(true);
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }


}
