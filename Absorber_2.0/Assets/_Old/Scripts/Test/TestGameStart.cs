using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestGameStart : MonoBehaviour
{

    public void StartGame()
    {
        // Fade.fade.BtnClickSound();
        StartCoroutine(onbtnClick_c());
    }

    IEnumerator onbtnClick_c()
    {
        // Fade.fade.FadeOut();
    
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SampleScene");
    }
}
