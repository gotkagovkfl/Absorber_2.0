using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

using TMPro;

//====================================================================================
//  게임 결과 등을 화면에 표시한다. (  게임 클리어 / 게임 오버 )
//====================================================================================
public class PauseUI : MonoBehaviour
{

    public GameObject Pic_GetAbility;
    public Image[] Abilities = new Image[63];

    public GameObject infos;

    public TMP_Text Player_Level;
    public TMP_Text Player_KillCount;
    public TMP_Text Player_Weapon;
    public TMP_Text Player_GetItem;
    public TMP_Text Player_Score;

    public TMP_Text Player_Hp;
    public TMP_Text Player_Damage;
    public TMP_Text Player_Attack_Speed;
    public TMP_Text Player_Range;
    public TMP_Text Player_Speed;
    public TMP_Text Player_Crit;

    // Start is called before the first frame update
    void Awake()
    {        
        Pic_GetAbility = GameObject.Find("Pic_GetAbility");

        for (int i = 0; i < Pic_GetAbility.transform.childCount; i++)
            Abilities[i] = Pic_GetAbility.transform.GetChild(i).GetComponent<Image>();

        infos = GameObject.Find("Infos").gameObject;
    }


    //====================================================================================
    // GameManager의 게임클리어 여부에 따라 게임 결과를 설정한다.  
    //====================================================================================
    public void SetStatus()
    {
        Player_Level.text = "Level " + Player.Instance.Level;
        Player_KillCount.text = "Kill " + GameManager.gm.KillCount.ToString();
        Player_Score.text = "Score " + GameManager.gm.Score.ToString();
        Player_Weapon.text = "Weapon\n" + Player.Instance.GetComponent<PlayerWeapon>().currWeapon[0].ToString().Remove(0,11).Replace(" (UnityEngine.GameObject)", "").Replace("(Clone)","");
        Player_Hp.text = "Hp " + Player.Instance.Max_Hp;
        Player_Damage.text = "Damage " + Player.Instance.Atk;
        Player_Attack_Speed.text = "Attack_Speed " + (Player.Instance.Attack_Speed + Player.Instance.Attack_Speed * Player.Instance.Attack_Speed_Plus / 100f);
        Player_Range.text = "Range " + (Player.Instance.Range + Player.Instance.Range * Player.Instance.Range_Plus / 100f);
        Player_Speed.text = "Speed " + (Player.Instance.Speed + Player.Instance.Speed * Player.Instance.Speed_Plus / 100f);
        Player_Crit.text = "Crit " + Player.Instance.Crit + "%";
        for (int i = 0; i < Player.Instance.chooseList.Count; i++)
        {
            Abilities[i].sprite = Resources.Load<Sprite>("Pictogram/" + Player.Instance.chooseList[i]);
            Abilities[i].color = new Color(1, 1, 1, 1);
        }
    }
}
