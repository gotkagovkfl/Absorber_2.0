using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Lobby : MonoBehaviour
{
    
    [SerializeField] Button btn_gameStart;
    [SerializeField] Button btn_quit;


    void Start()
    {
        btn_gameStart.onClick.AddListener(SceneHandler.LoadScene_main);
        btn_quit.onClick.AddListener( Quit );

    }


    void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); 
        #endif
    }





}
