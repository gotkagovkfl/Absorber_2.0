using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerSkills : MonoBehaviour
{
    [SerializeField] GameObject prefab_playerSkill;
    
    [SerializeField] Dictionary<KeyCode,UI_PlayerSkill> playerSkills=new();


    IEnumerator Start()
    {
        
        yield return new WaitUntil(()=>Player.initialized);

        Init();
    }


    public void Init()
    {
        // 제대로 초기화 하기 위해 미리 만들어진 스킬 정보 ui 파괴
        foreach( var s in GetComponentsInChildren<UI_PlayerSkill>())
        {
            Destroy(s.gameObject);
        }


        foreach( var kv in Player.player.skills)
        {
            KeyCode keyCode = kv.Key;
            PlayerSkill playerSkill = kv.Value;

            UI_PlayerSkill ps =  Instantiate(prefab_playerSkill, transform).GetComponent<UI_PlayerSkill>();

            ps.Init(keyCode,playerSkill);

            playerSkills.Add(keyCode, ps);
        }        


        //

        GameEvent.ge.onUseSkill.AddListener( OnUseSkill );
    }


    void OnUseSkill(KeyCode keyCode)
    {
        playerSkills[keyCode].OnUseSkill();
    }

}
