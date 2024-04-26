using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System;

public class UiScript : MonoBehaviour
{
    public Button First;
    public GameObject PlayerUi;
    public string First_status;
    public int First_Amount;
    public Text First_text;
    public Image First_Image;
    public string First_Image_Num;
    public Button Second;
    public int Second_Amount;
    public string Second_status;
    public Text Second_text;
    public Image Second_Image;
    public string Second_Image_Num;
    public Button Third;
    public int Third_Amount;
    public string Third_status;
    public Text Third_text;
    public Image Third_Image;
    public string Third_Image_Num;
    public Image Random_Image;
    // public Player Player;
    public Dictionary<string, string> Status_data;
    // Start is called before the first frame update
    
    public AudioSource audioSource;
    public AudioClip sound_levelUp;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource!=null)
        {
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
        sound_levelUp = Resources.Load<AudioClip>("Sound/8.levelup");
        // Player = Player.player;    
        PlayerUi = GameObject.Find("Canvas").transform.Find("PlayerUI").gameObject;
        First.onClick.AddListener(F);
        Second.onClick.AddListener(S);
        Third.onClick.AddListener(T);
    }
    public void F()
    {
        select(First_Image_Num, First_status, First_Amount);
        gameObject.SetActive(false);
        GameManager.gm.PauseGame(false);
        PlayerUi.SetActive(true);
    }
    public void S()
    {
        select(Second_Image_Num, Second_status, Second_Amount);
        gameObject.SetActive(false);
        GameManager.gm.PauseGame(false);
        PlayerUi.SetActive(true);
    }
    public void T()
    {
        select(Third_Image_Num, Third_status, Third_Amount);      
        gameObject.SetActive(false);
        GameManager.gm.PauseGame(false);
        PlayerUi.SetActive(true);
    }
    public void select(string img, string status, int amount)
    {
        //
        audioSource.PlayOneShot(sound_levelUp);
        //
        Dictionary<string, string> Status_data = GameObject.Find("CSVManager").GetComponent<test>().Status_data;
        string fieldName = Status_data[status];
        Player.player.chooseList.Add(img);

        FieldInfo field = typeof(Player).GetField(fieldName, BindingFlags.Instance | BindingFlags.Public);
        if (field != null)
        {
            // Player player = Player.GetComponent<Player>();
            Type type = field.GetValue(Player.player).GetType();
            if (type == typeof(int))
            {
                int Current_Value = (int)field.GetValue(Player.player);
                int New_Value = Current_Value + amount;
                field.SetValue(Player.player, New_Value);
            }
            else if (type == typeof(float))
            {
                float Current_Value = (float)field.GetValue(Player.player);
                float New_Value = Current_Value + amount;
                field.SetValue(Player.player, New_Value);
            }
            if (status == "최대체력")
                Player.player.ChangeHp(amount);
            else if (status == "행운")
                Player.player.Luk = Math.Min(Player.player.Luk, 4);
            else if(status == "투사체 개수")
                Player.player.projNum = Math.Min(Player.player.projNum, 2);
            else if(status == "분열")
                Player.player.splitNum = Math.Min(Player.player.splitNum, 2);
        }
        else
        {
            MethodInfo method = typeof(Player).GetMethod(fieldName, BindingFlags.Instance | BindingFlags.Public);
            if(method != null)
                method.Invoke(Player.player, null);
        }
        Player.player.OnSelecting = false;
        Player.player.GetComponent<PlayerWeapon>().InitWeapons();
    }
}
