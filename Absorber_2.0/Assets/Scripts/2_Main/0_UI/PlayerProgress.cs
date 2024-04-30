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

    // dash
    [SerializeField] Slider slider_dash;
    [SerializeField] GameObject dashIndicator;


    Transform t_player; // 캐싱
    Vector3 offset;
    
    //==================================================================
    void Start()
    {
        t_player = Player.player.t_player;

        offset = new Vector3(0, 5, 0);
        transform.position = Camera.main.WorldToScreenPoint( t_player.position + offset);


        SetHpBar();
        SetMpBar();
        SetDashBar();
        SetLevelText();

        //
        GameEvent.ge.onChange_hp.AddListener(SetHpBar);
        GameEvent.ge.onChange_exp.AddListener(SetMpBar);
        GameEvent.ge.onChange_level.AddListener(SetLevelText);
        GameEvent.ge.onDash.AddListener(OnDash);
    }


    void FixedUpdate()
    {
        // transform.position = Camera.main.WorldToScreenPoint( t_player.position);
    }

    //==================================================================

    public void SetHpBar()
    {
        int hp_max = Player.player.Max_Hp;
        int hp_curr = Player.player.Hp;

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
        slider_mp.maxValue = Player.player.Exp;
        slider_mp.value = Player.player.Cur_Exp;
    }

    void SetLevelText()
    {
        text_level.text = Player.player.Level.ToString();
    }

    //------------------------------------------------------------------------------
    void OnDash()
    {

    }

    void SetDashBar()
    {
        // slider_dash.maxValue = Player.player.dashingCooldown;
        slider_dash.value = slider_dash.maxValue;
        dashIndicator.SetActive(true);
    }

}
