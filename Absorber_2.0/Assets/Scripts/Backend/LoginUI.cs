using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class LoginUI : MonoBehaviour
{
    private static LoginUI _instance = null;

    public static LoginUI Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LoginUI();
            }

            return _instance;
        }
    }
    // Start is called before the first frame update
    public Selectable Id;
    public TMP_InputField ID;
    public TMP_InputField PWD;
    public string playerID;
    public string playerPwd;
    public Button Login;
    EventSystem system;
    public GameObject Login_pop;
    void Start()
    {
        system = EventSystem.current;
        Id.Select();
        Login_pop = GameObject.Find("Login_pop");
    }
    public void getInfo()
    {
        playerID = ID.GetComponent<TMP_InputField>().text;
        playerPwd = PWD.GetComponent<TMP_InputField>().text;
    }
    // Update is called once per frame
    void Update()
    {
         if(Input.GetKeyDown(KeyCode.Tab) && Login_pop.gameObject.activeSelf == true)
         {
             Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
             if (next != null)
                 next.Select();
         }
         else if(Input.GetKeyUp(KeyCode.Return) && Login_pop.gameObject.activeSelf == true)
         {
            getInfo();
            Login.onClick.Invoke();
         }
    }
}
