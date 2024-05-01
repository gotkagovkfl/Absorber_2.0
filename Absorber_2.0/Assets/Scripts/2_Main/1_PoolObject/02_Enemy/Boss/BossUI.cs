using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public Enemy boss;
    public Slider slider_bossHp;
    public Slider slider_bossHp_delay;

    public Coroutine coroutine_delay;

    //=======================================================================
    // 보스 체력바 활성화/비활성화
    public void ActiveHpBar(bool flag)
    {
        slider_bossHp.gameObject.SetActive(flag);
    }

    // 보스 체력 초기화
    public void InitHpBar()
    {
        ActiveHpBar(true);

        slider_bossHp.maxValue = boss.hp_max;
        // slider_bossHp.value = boss.hp;

        slider_bossHp_delay.maxValue = boss.hp_max;
        // slider_bossHp_delay.value = boss.hp;
        
        StartCoroutine(FillHpBar_start());
    }

    // 보스 등장시 체력바 채우기 
    public IEnumerator FillHpBar_start()
    {
        for (int i=1;i<=10;i++)
        {
            slider_bossHp.value         = boss.hp_max * 0.1f * i;
            slider_bossHp_delay.value   = boss.hp_max * 0.1f * i;

            yield return null;
            yield return null;
            yield return null;
        }
    }
    
    // 보스 체력 변동시 체력바 세팅
    public void SetHpBar()
    {
        slider_bossHp.value = boss.hp_curr;

        if (coroutine_delay != null)
        {
            StopCoroutine(coroutine_delay);
        }
        coroutine_delay = StartCoroutine( SetHpBar_delay() );
    }
    
    // 보스 체력 변동시 체력바 세팅 딜레이 
    public IEnumerator SetHpBar_delay()
    {
        float gap = slider_bossHp_delay.value - slider_bossHp.value;
  
        yield return new WaitForSeconds(0.5f);
        for (int i=0;i<10;i++)
        {
            slider_bossHp_delay.value =  slider_bossHp_delay.value - gap * 0.1f;
            
            yield return new WaitForSeconds(0.05f);
        }
        
    }

    //
    void Start()
    {
        boss = GetComponent<Enemy>();

        slider_bossHp = GameObject.Find("Canvas").transform.Find("BossHp").GetComponent<Slider>();
        slider_bossHp_delay = slider_bossHp.transform.Find("BossHp_delay").GetComponent<Slider>();

        ActiveHpBar(false);
    }
}
