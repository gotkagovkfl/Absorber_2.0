using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class RegisterUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Selectable Name;
    public TMP_InputField NickName;
    public TMP_InputField ID;
    public TMP_InputField PWD;
    public string playerName;
    public string playerID;
    public string playerPwd;
    public Button Register;
    EventSystem system;
    public GameObject Register_pop;
    void Start()
    {
        system = EventSystem.current;
        Name.Select();
        //Register_pop = GameObject.Find("Register_pop");
    }
    public void getInfo()
    {
        playerName = NickName.GetComponent<TMP_InputField>().text;
        playerID = ID.GetComponent<TMP_InputField>().text;
        playerPwd = PWD.GetComponent<TMP_InputField>().text;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Register_pop.gameObject.activeSelf == true)
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
                next.Select();
        }
        else if (Input.GetKeyUp(KeyCode.Return) && Register_pop.gameObject.activeSelf == true)
        {
            getInfo();
            //Debug.Log(playerName + playerID + playerPwd);
            Register.onClick.Invoke();
        }
    }


    
    public void CancelClick()
    {
        // Fade.fade.BtnClickSound();
        Register_pop.SetActive(false);
        GameObject.Find("Canvas").transform.Find("Login_pop").gameObject.SetActive(true);

        // LoginUI.Instance.Login_pop.SetActive(true);
    }
}
