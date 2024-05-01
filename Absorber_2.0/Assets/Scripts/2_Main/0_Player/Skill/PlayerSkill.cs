using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class PlayerSkill 
{
    public string id;
    public string skillName;

    public int useCount; 
    public float lastUseTime;   // 스킬 마지막 사용시간 

    public float coolTime;      //쿨타임 ( 초 ) 

    public float coolTime_remain => lastUseTime +coolTime - Time.time;
    public bool isCoolTimeOk => coolTime_remain <= 0;

    public float duration;      // 지속시간이 있는 경우, 
    

    AudioClip sfx;



    // 사용 조건
    public bool IsAvailable()
    {
        // && Time.timeScale == 1       // 이거 뭔지 모르겠음 

        return isCoolTimeOk && IsAvailable_custom();
    }
    protected abstract bool IsAvailable_custom();
    

    public void UseSkill()
    {
        //
        lastUseTime = Time.time; //시간기록
        PlaySFX();          //효과음
        ShowEffect();       // 이펙트
        UseSkill_custom();  // 효과

        if (duration>0)
        {
            Player.player.StartCoroutine( OnFinishDuration() );
        }
    }

    // 사용효과
    protected abstract void UseSkill_custom();

    // 효과음 재생
    void PlaySFX()
    {
        
    }

    // 사용 이펙트
    protected abstract void ShowEffect();

    // 지속시간이 끝난 후 처리 
    IEnumerator OnFinishDuration()
    {
        yield  return new WaitForSeconds( duration);
        OnFinishDuration_custom();  
    }


    protected abstract void OnFinishDuration_custom();

}
