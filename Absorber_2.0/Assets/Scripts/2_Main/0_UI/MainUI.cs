using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] Popup_Pause popup_pause;


    void Start()
    {
        popup_pause = transform.Find("Popup_Pause").GetComponent<Popup_Pause>();
        popup_pause.InitPopup();
    }
}
