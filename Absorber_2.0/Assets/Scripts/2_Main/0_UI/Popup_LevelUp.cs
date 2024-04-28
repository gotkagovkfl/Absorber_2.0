using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_LevelUP : MonoBehaviour
{

    public void InitPopup()
    {

    }

    void OpenPopup()
    {
        gameObject.SetActive(true);
    }

    void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
