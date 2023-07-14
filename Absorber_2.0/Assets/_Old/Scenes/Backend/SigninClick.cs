using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SigninClick : MonoBehaviour
{
    public Button btn;
    public GameObject Login_pop;
    public GameObject Register_pop;
    // Start is called before the first frame update
    private void Start()
    {
        Login_pop = GameObject.Find("Login_pop");
        //Register_pop = GameObject.Find("Register_pop");
    }
    public void onbtnClick()
    {
        // Fade.fade.BtnClickSound();
        
        Login_pop.SetActive(false);
        Register_pop.SetActive(true);
    }


}
