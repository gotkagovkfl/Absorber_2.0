using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using TMPro;

public class BackendLogin
{
    private static BackendLogin _instance = null;

    public static BackendLogin Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendLogin();
            }

            return _instance;
        }
    }
    public bool LoginSuccess = false;
    public bool SignupSuccess = false;
    public bool CustomSignUp(string id, string pw)
    {
        if (SignupSuccess)
            return true;
        //Debug.Log("회원가입을 요청합니다.");
        
        var bro = Backend.BMember.CustomSignUp(id, pw);

        if (bro.IsSuccess())
        {
            //Debug.Log("회원가입에 성공했습니다. : " + bro);
            SignupSuccess = true;
        }
        else
        {
            //Debug.LogError("회원가입에 실패했습니다. : " + bro);
            SignupSuccess = false;
        }
        return SignupSuccess;
    }

    public bool CustomLogin(string id, string pw)
    {
        //Debug.Log("로그인을 요청합니다.");

        var bro = Backend.BMember.CustomLogin(id, pw);

        if (bro.IsSuccess())
        {
            //Debug.Log("로그인이 성공했습니다. : " + bro);
            LoginSuccess = true;
        }
        else
        {
            //Debug.LogError("로그인에 실패했습니다. : " + bro);
            LoginSuccess = false;
        }
        return LoginSuccess;
    }

    public void UpdateNickname(string nickname)
    {
        //Debug.Log("닉네임 변경을 요청합니다.");

        var bro = Backend.BMember.UpdateNickname(nickname);

        if (bro.IsSuccess())
        {
            //Debug.Log("닉네임 변경에 성공했습니다 : " + bro);
        }
        else
        {
            //Debug.LogError("닉네임 변경에 실패했습니다 : " + bro);
        }
    }
}