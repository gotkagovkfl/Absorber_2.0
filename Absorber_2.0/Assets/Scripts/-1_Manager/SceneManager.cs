using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class SceneHandler : MonoBehaviour
{
    //====================
    // 비동기 씬 호출 : sceneName에 해당하는 씬을 비동기적으로 로드한다. 
    //===================
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadScene_async(sceneName));


    }

    IEnumerator LoadScene_async(string sceneName)
    {       
        //Scene currScene = SceneManager.GetActiveScene();
        
        // 비동기 씬호출
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // 연출매니저가 없으면, 연출 대기 안함. 
        if (DirectingManager.directingManager != null)
        {
            asyncLoad.allowSceneActivation = false;
            EventHandler.eventHandler.onFinished_sceneChangeDirecting.AddListener(() => { asyncLoad.allowSceneActivation = true; });
        }


        //asyncLoad.allowSceneActivation = true;
        // 씬 로드될때까지 할 일 (현재는 비어있음)
        while ( !asyncLoad.allowSceneActivation )
        {
            yield return null;
        }
            
        //asyncLoad = SceneManager.UnloadSceneAsync(currScene.name);

        //yield return asyncLoad;
    }
}
