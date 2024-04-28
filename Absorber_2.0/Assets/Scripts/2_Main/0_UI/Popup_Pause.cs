using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Pause : MonoBehaviour
{
    // Start is called before the first frame update

    public void InitPopup()
    {
        GameEvent.ge.onPause.AddListener(OpenPopup);       
        GameEvent.ge.onResume.AddListener(ClosePopup);       
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
