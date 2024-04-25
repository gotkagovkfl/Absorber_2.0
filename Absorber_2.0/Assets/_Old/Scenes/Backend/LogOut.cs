using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.SceneManagement;
public class LogOut : MonoBehaviour
{
  public void LogoutBtnClick()
    {
        Backend.BMember.Logout();
        // SceneManager.LoadScene("TestLogin");
    }
}
