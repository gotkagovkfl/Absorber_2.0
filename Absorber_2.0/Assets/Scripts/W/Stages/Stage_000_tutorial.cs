using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_000_tutorial : Stage
{
    public AudioClip bgm_normal;
    
    public override void InitStageInfo_custom()
    {
        id_stage = "000";

        num_stage = 0;

        bgm_normal = Resources.Load<AudioClip>("Sound/20_Tutorial");
    }

    public override void StartStageRoutine_custom()
    {
        audioSource.clip = bgm_normal;
        audioSource.loop = true;
        audioSource.Play();
    }
}
