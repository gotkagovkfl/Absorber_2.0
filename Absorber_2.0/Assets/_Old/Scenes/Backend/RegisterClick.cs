using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BackEnd;
using TMPro;

public class RegisterClick : MonoBehaviour
{
    public Button btn;
    public GameObject Error_pop;
    public TMP_Text Error_text;
    GameObject RegisterManager;
    // Start is called before the first frame update
    public void Start()
    {
        RegisterManager = GameObject.Find("RegisterManager");        

    }
    public void onbtnClick()
    {
        RegisterManager.GetComponent<RegisterUI>().getInfo();
        string ID = RegisterManager.GetComponent<RegisterUI>().playerID;
        string PWD = RegisterManager.GetComponent<RegisterUI>().playerPwd;
        string NAME = RegisterManager.GetComponent<RegisterUI>().playerName;
        if (BackendLogin.Instance.CustomSignUp(ID, PWD))
        {
            BackendReturnObject bro2 = Backend.BMember.CheckNicknameDuplication(NAME);
            if (bro2.IsSuccess())
            {
                BackendLogin.Instance.UpdateNickname(NAME);
                StartCoroutine(onbtnClick_c());
            }
            else
            {
                Error_pop.SetActive(true);
                Error_text.text = "중복된 닉네임 입니다.";
            }
        }
        else
        {
            Error_pop.SetActive(true);
            Error_text.text = "중복된 아이디 혹은 부적절한 아이디/비밀번호 입니다.";
        }
    }
    
    IEnumerator onbtnClick_c()
    {
        Fade.fade.BtnClickSound();

        // Fade.fade.FadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("TestLobby");
    }


}
