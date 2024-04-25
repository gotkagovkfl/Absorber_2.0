using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.SceneManagement;
using UnityEngine.Events;

using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class SceneHanlder : MonoBehaviour
{
    public static SceneHanlder sh;
    
    [SerializeField] TextMeshProUGUI text_test;
    

    [Header("씬 로딩 연출")]
    [SerializeField] Image img_fade;
    Sequence seq_fade;
    


    //===================================================================================================

    void Awake()
    {
        if(sh == null)
        {
            sh = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    //==========================================================================================================



    
    //====================
    // 비동기 씬 호출 : sceneName에 해당하는 씬을 비동기적으로 로드한다. 
    //===================
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadScene_async(sceneName));
    }

    IEnumerator LoadScene_async(string sceneName)
    {       
        // 비동기 씬호출
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // 허락할 때만 씬 이동하기로.
        asyncLoad.allowSceneActivation = false;

        // 페이드 인 연출
        PlayAnim_fade(true, 0.5f);

        int i=0;
        // 씬 로드될때까지 할 일 (현재는 비어있음)  - 
        while ( !asyncLoad.allowSceneActivation )
        {
            text_test.SetText( $"test:  {i}");
            i++;
            yield return null;


            if (!seq_fade.IsActive())
            {
                PlayAnim_fade(false, 0.5f);     // true 직후에 씬 로드 될테니, 페이드 아웃하면됨. 
                asyncLoad.allowSceneActivation = true;
            }
        }
    }


    //==================================================================================

    public static void LoadScene_lobby()
    {
        sh?.LoadScene("01_Lobby");
    }








    //==============================================================================
    /// <summary>
    /// 씬 전환간 페이드 인/아웃 연출을 재생한다. 
    /// </summary>
    void PlayAnim_fade(bool isFadeIn, float duration)
    {
        float alpha_init = (isFadeIn)?0f:1f;
        float alpha_final = (isFadeIn)?1f:0f;


        img_fade.color = new Color(0,0,0, alpha_init);      //초기 색깔 지정


        seq_fade = DOTween.Sequence()
            .OnStart( ()=>{img_fade.raycastTarget=true;}      )
            .OnKill( ()=>{img_fade.raycastTarget=false;}      )     // 씬 전환 연출간 
            .AppendInterval(0.5f)
            .Append(img_fade.DOFade( alpha_final,duration))
            .Play();   
    }



}
