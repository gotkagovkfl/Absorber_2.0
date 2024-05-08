using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class PlayerProgress : MonoBehaviour
{
    // hp
    [SerializeField] Slider slider_hp;
    [SerializeField] TextMeshProUGUI text_hp;

    // mp
    [SerializeField] Slider slider_mp;
    [SerializeField] TextMeshProUGUI text_level;

    // // dash
    // [SerializeField] Slider slider_dash;
    // [SerializeField] GameObject dashIndicator;


    // Transform t_player; // 캐싱
    // Vector3 offset;
    
    //==================================================================
    IEnumerator Start()
    {
        // gameObject.SetActive(false);     // 오브젝트 비활하면 아래 실행안됨 ㄷㄷ;


        yield return new WaitUntil( ()=>Player.initialized );

    
        // t_player = Player.player.t_player;

        // offset = new Vector3(0, 5, 0);
        // transform.position = Camera.main.WorldToScreenPoint( t_player.position + offset);


        SetHpBar();
        SetMpBar();
        // SetDashBar();
        SetLevelText();

        //
        GameEvent.ge.onChange_hp.AddListener(SetHpBar);
        GameEvent.ge.onChange_exp.AddListener(SetMpBar);
        GameEvent.ge.onChange_level.AddListener(SetLevelText);
        // GameEvent.ge.onDash.AddListener(OnDash);


        gameObject.SetActive(true);
        Debug.Log("플레이어 진행바 세팅 완료 ");
    }


    // void FixedUpdate()
    // {
        // transform.position = Camera.main.WorldToScreenPoint( t_player.position);
    // }

    //==================================================================

    public void SetHpBar(int value)
    {
        int hp_max = Player.playerStatus.hp_max;
        int hp_curr = Player.playerStatus.hp_curr;

        slider_hp.maxValue = hp_max;
        
        slider_hp.value = hp_curr;
        text_hp.text = $"{hp_curr}/{hp_max}";
    }


    public void SetHpBar()
    {
        int hp_max = Player.playerStatus.hp_max;
        int hp_curr = Player.playerStatus.hp_curr;

        slider_hp.maxValue = hp_max;
        
        slider_hp.value = hp_curr;
        text_hp.text = $"{hp_curr}/{hp_max}";
        
        // slider_hp_delay.maxValue = Player.player.Max_Hp;
        // if (coroutine_delay != null)
        // {
        //     StopCoroutine(coroutine_delay);
        // }
        // coroutine_delay = StartCoroutine( SetHpBar_delay() );

    }
    
    // 보스 체력 변동시 체력바 세팅 딜레이 
    // public IEnumerator SetHpBar_delay()
    // {
    //     float gap = slider_hp_delay.value - slider_hp.value;
        
    //     yield return new WaitForSeconds(0.5f);
    //     for (int i=0;i<10;i++)
    //     {
    //         slider_hp_delay.value =  slider_hp_delay.value - gap * 0.1f;
            
    //         yield return new WaitForSeconds(0.05f);
    //     }
        
    // }

    //-------------------------------------------------------------------------------
    void SetMpBar()
    {
        slider_mp.maxValue = Player.player.exp_max;
        slider_mp.value = Player.player.exp_curr;
    }

    void SetLevelText()
    {
        text_level.text = $"Lv.{Player.player.level}";
    }

    //------------------------------------------------------------------------------
}
