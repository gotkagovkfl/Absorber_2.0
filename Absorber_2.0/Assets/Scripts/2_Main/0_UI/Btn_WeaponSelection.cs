using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.iOS;

public class Btn_WeaponSelection : MonoBehaviour
{    
    Weapon weapon;

    public void Init(Weapon weapon)
    {
        this.weapon = weapon;
        
        string id = weapon.id_weapon;
        string path = $"UI/Icon_WeaponBtn/{id}";

        GetComponent<Image>().sprite = Resources.Load<Sprite>(path);
        GetComponentInChildren<TextMeshProUGUI>().text = weapon.name;


        GetComponent<Button>().onClick.AddListener(OnClick_btn);
    }

    void OnClick_btn()
    {
        Debug.Log("무기교체 버튼 눌렀어용 " + weapon.id_weapon);
        
        Player.player.GetComponent<PlayerWeapon>().ChangeWeapon(0,weapon.id_weapon);

        MainUI.mainUI.popup_weaponSelection.ClosePopup();
    }
}
