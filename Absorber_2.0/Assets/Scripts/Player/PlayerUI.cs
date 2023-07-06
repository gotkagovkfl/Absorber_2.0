using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

//=======================================================================
// Player능력치와 관련된 UI를 보여줌. : Hp, Exp, Level, state, skillCoolTime.....
//=======================================================================
public class PlayerUI : MonoBehaviour
{
    public Transform transform_playerUI;

    public void UpdateUIPos()
    {
        Vector3 playerPos = Camera.main.WorldToScreenPoint(Player.Instance.myTransform.position);

        transform_playerUI.position = playerPos + Vector3.up;
    }

    public void VanishPlayerUI()
    {

    }

    public IEnumerator VanishPlayerUI_c()
    {
        yield return null;
    }

    //=================================== HP 바=======================================
    public Slider slider_hp;
    public Slider slider_hp_delay;
    public TextMeshProUGUI text_hp;
    public Coroutine coroutine_delay;

    public AudioSource audioSource;
    public AudioClip sound_fatal;

    //================================================================================
    public void SetHpBar()
    {
        slider_hp.maxValue = Player.Instance.Max_Hp;
        slider_hp_delay.maxValue = Player.Instance.Max_Hp;
        slider_hp.value = Player.Instance.Hp;
        text_hp.text = ((Player.Instance.Hp).ToString() + "/" +Player.Instance.Max_Hp.ToString());
        
        if (coroutine_delay != null)
        {
            StopCoroutine(coroutine_delay);
        }
        coroutine_delay = StartCoroutine( SetHpBar_delay() );

    }
    
    // 보스 체력 변동시 체력바 세팅 딜레이 
    public IEnumerator SetHpBar_delay()
    {
        float gap = slider_hp_delay.value - slider_hp.value;
        
        yield return new WaitForSeconds(0.5f);
        for (int i=0;i<10;i++)
        {
            slider_hp_delay.value =  slider_hp_delay.value - gap * 0.1f;
            
            yield return new WaitForSeconds(0.05f);
        }
        
    }



    //=================================== 경험치 바 =======================================
    public TextMeshProUGUI text_playerLevel;

    public Slider slider_mp;
    
    public Text text_mp;   

    public void SetMpBar()
    {
        slider_mp.maxValue = Player.Instance.Exp;
        slider_mp.value = Player.Instance.Cur_Exp;
    }

    public void SetLevelText()
    {
        text_playerLevel.text = Player.Instance.Level.ToString();
    }

    
    //=================================== 대시 바 =======================================
    public Slider slider_dash;
    public GameObject image_canDash;
    
    public Text text_dash;   


    public void SetDashBar()
    {
        slider_dash.maxValue = Player.Instance.dashingCooldown;
        slider_dash.value = slider_dash.maxValue;
        image_canDash.SetActive(true);
    }

    public void OnUseDash()
    {
        StartCoroutine(UpdateDashCoolTime());
    }

    public IEnumerator UpdateDashCoolTime()
    {
        slider_dash.value = 0;
        image_canDash.SetActive(false);

        while( slider_dash.value <slider_dash.maxValue)
        {
            slider_dash.value += Time.deltaTime;
            yield return null;
        }

        image_canDash.SetActive(true);

    }

    //===============================================================================================
    //============================================================================================
    public Animator bloodEffect;

    public void SetBloodOverlay()
    {
        if (Player.Instance.Hp < Player.Instance.Max_Hp * 0.3 && Player.Instance.Hp > 0)
        {
            bloodEffect.SetBool("fatal", true);
            audioSource.clip = sound_fatal;
            audioSource.Play();
        }
        else
        {
            bloodEffect.SetBool("fatal", false);
            audioSource.Stop();
        }
        
    }    



//==========================================================================================================================
    // Start is called before the first frame update
    void Start()
    {
        transform_playerUI = GameObject.Find("PlayerUI").transform;
        
        // 초기화
        slider_hp = GameObject.Find("Slider_HP").GetComponent<Slider>();
        slider_hp_delay = slider_hp.transform.Find("Slider_HP_delay").GetComponent<Slider>();
        slider_mp = GameObject.Find("Slider_MP").GetComponent<Slider>();
        slider_dash  =GameObject.Find("Slider_Dash").GetComponent<Slider>();

        bloodEffect = GameObject.Find("BloodOverlay").GetComponent<Animator>();
        sound_fatal = Resources.Load<AudioClip>("Sound/15_heartbeat");

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = true;


        //
        text_playerLevel = GameObject.Find("Text_PlayerLevel").GetComponent<TextMeshProUGUI>();
        text_hp = GameObject.Find("Text_HP").GetComponent<TextMeshProUGUI>();
        
        image_canDash = GameObject.Find("Image_CanDash");

        //======================================
        SetHpBar();
        SetMpBar();
        SetDashBar();
        SetLevelText();

        bloodEffect.SetBool("fatal", false); 

    }

    void FixedUpdate()
    {
        UpdateUIPos();
    }
}
