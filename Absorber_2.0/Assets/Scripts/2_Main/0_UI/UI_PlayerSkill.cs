using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BackEnd.Socketio;


public class UI_PlayerSkill : MonoBehaviour
{
    bool initialized = false;
    
    PlayerSkill playerSkill;
    KeyCode keyCode;
    
    [SerializeField] Slider slider_skill;         // 스킬 이미지 - 아이콘 설정 및 쿨타임일 때 음영조정
    [SerializeField] Image image_skill;         // 스킬 이미지 - 아이콘 설정 및 쿨타임일 때 음영조정
    [SerializeField] TextMeshProUGUI text_key;  //  키 표시
    [SerializeField] TextMeshProUGUI text_coolTime; // 쿨타임 표시   
    
    bool isOnCoolTime;

    //================================================================


    void Awake()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        
        if (!initialized)
            return;
        


        bool isAvailable = playerSkill.IsAvailable();
        
        if (!isAvailable)
        {
            // 
            image_skill.color = Color.grey;
        }
        else
        {
            image_skill.color = Color.white;
        }
    }







    //================================================================


    public void Init(KeyCode keyCode, PlayerSkill playerSkill)
    {
        this.keyCode = keyCode;
        this.playerSkill = playerSkill;


        text_key.text = keyCode.ToString();
        image_skill.sprite = ResourceManager.GetSkillIcon(playerSkill.id);

        slider_skill.maxValue = playerSkill.coolTime;
        slider_skill.value = 0;

        text_coolTime.gameObject.SetActive(false);


        gameObject.SetActive(true);
        initialized = true;
    }

    public void OnUseSkill()
    {
        StartCoroutine( OnUseSkill_c());
        
    }

    IEnumerator OnUseSkill_c()
    {
        // 초기 세팅
        text_coolTime.gameObject.SetActive(true);
        slider_skill.maxValue = playerSkill.coolTime;
        
        // 남은 시간과 슬라이더 표시 
        while ( !playerSkill.isCoolTimeOk )
        {
            var coolTime_remain = playerSkill.coolTime_remain;
            text_coolTime.text = $"{coolTime_remain:0.0}";
            slider_skill.value = (float)coolTime_remain;
            yield return null;
        }

        // 종료 세팅  - 쿨타임 텍스트 off, 스킬 이미지 밝게, 
        text_coolTime.gameObject.SetActive(false);
        slider_skill.value = 0;
    }

    
}
