using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_000_tutorial : Stage
{
    public AudioClip bgm_normal;

    protected override void InitEssentialInfo_stage()
    {
        id_stage = "000";

        name_stage = "듀토리얼";

        // bgm_normal = Resources.Load<AudioClip>("Sound/20_Tutorial");
    }

    public override void StartStageRoutine_custom()
    {        
        // audioSource.clip = bgm_normal;
        // audioSource.loop = true;
        // audioSource.Play();
    }
}
