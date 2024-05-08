using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public static MainUI mainUI {get;private set;}
    
    [SerializeField] public Popup_Pause popup_pause;
    [SerializeField] public Popup_WeaponSelection popup_weaponSelection;

    [SerializeField] public Popup_LevelUp popup_levelUp;


    IEnumerator Start()
    {
        yield return new WaitUntil(()=>Player.initialized);
        
        popup_pause = transform.Find("Popup_Pause").GetComponent<Popup_Pause>();
        popup_pause.InitPopup();

        popup_weaponSelection = transform.Find("Popup_WeaponSelection").GetComponent<Popup_WeaponSelection>();
        popup_weaponSelection.InitPopup();

        popup_levelUp = transform.Find("Popup_LevelUp").GetComponent<Popup_LevelUp>();
        popup_levelUp.InitPopup();

        mainUI = this;
    }
}
