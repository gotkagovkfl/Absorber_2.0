using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Return : MonoBehaviour
{
    public void ReturnToLobby()
    {
        SceneManager.LoadScene("TestLobby");
    }
}
