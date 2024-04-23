using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DG.Tweening;

public class InitUI : MonoBehaviour
{
    [SerializeField] Slider slider_loadingBar; 
    [SerializeField] TextMeshProUGUI text_loading;

    Sequence seq_loading;

    int targetSliderValue;

    public void Init()
    {
        Debug.Log("초기화 화면 UI 초기화 ");

        // GameEvent.onStartInitWork.AddListener(OnStartInitWork);
        // GameEvent.onFinishInitWork.AddListener(OnFinishInitWork);
    }

    public void Init_loadingBar(int count_initWorks)
    {
        slider_loadingBar.maxValue = count_initWorks;
        slider_loadingBar.value = 0;
    }


    /// <summary>
    /// 해당 작업 시작시 - 작업명을 표시한다.  
    /// </summary>
    /// <param name="currWork"></param>
    public void OnStartInitWork(InitWork currWork)
    {
        text_loading.text = currWork.workName;
    }

    /// <summary>
    /// 현재 초기화 작업이 끝나면 - 진행도 바를 세팅한다. 
    /// </summary>
    public void OnFinishInitWork()
    {
        targetSliderValue+=1;
        
        PlayAnim_updateloadingBar();
    }

    void PlayAnim_updateloadingBar()
    {
        if (seq_loading.IsActive())
        {
            seq_loading.Kill();
        }

        seq_loading = DOTween.Sequence()
            .OnKill( ()=>{ 
                if (slider_loadingBar.value>=slider_loadingBar.maxValue)
                {
                    text_loading.text = "게임 시작중..";
                };
            })
            .Append(slider_loadingBar.DOValue(targetSliderValue, 0.3f))
            .Play();

    }
}
