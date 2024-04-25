using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;

public class SceneLoading : MonoBehaviour
{
     [Header("Fade In/Out")]
    [SerializeField] Image img_fade;
    Sequence seq_fade;

    //=================================================

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }


    //====================================================================


    /// <summary>
    /// 씬 전환간 페이드 인/아웃 연출을 재생한다. 
    /// </summary>
    void PlayAnim_fade(bool isFadeIn, float duration)
    {
        float alpha_init = (isFadeIn)?0f:1f;
        float alpha_final = (isFadeIn)?1f:0f;


        img_fade.color = new Color(0,0,0, alpha_init);      //초기 색깔 지정


        seq_fade = DOTween.Sequence()
            .AppendInterval(0.5f)
            .Append(img_fade.DOFade( alpha_final,duration))
            .Play();   
    }
}
