using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginClick : MonoBehaviour
{
    public GameObject LoginManager;
    public Button btn;
    public GameObject Error_pop;
    public TMP_Text Error_text;
    public void Start()
    {
        LoginManager = GameObject.Find("LoginManager");
    }
    // Start is called before the first frame update
    public void onbtnClick()
    {  
        Fade.fade.BtnClickSound();
        StartCoroutine(onbtnClick_c());
    }

    IEnumerator onbtnClick_c()
    {
        LoginManager.GetComponent<LoginUI>().getInfo();
        string ID = LoginManager.GetComponent<LoginUI>().playerID;
        string PWD = LoginManager.GetComponent<LoginUI>().playerPwd;
        BackendLogin.Instance.CustomLogin(ID, PWD);
        if (BackendLogin.Instance.LoginSuccess)
        {
            Fade.fade.FadeOut();
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("TestLobby");
        }
        else
        {
            Error_pop.SetActive(true);
            Error_text.text = "잘못된 아이디/비밀번호 입니다.";
        }
    }
}
