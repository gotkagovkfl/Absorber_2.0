using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodEffect : MonoBehaviour
{
    public Image bloodScreen;
    public Animator bloodEffect;
    public Player player;

    void OnEnable()
    {
        player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
    }

    void Start()
    {
        bloodEffect.SetBool("fatal", false); 
    }

    // 체력 30% 미만일 때 블러드이펙트 발생
    void Update()
    {
        Debug.Log(player.Hp);
        Debug.Log(player.Max_Hp);
        if (player.Hp < player.Max_Hp * 0.3 && player.Hp > 0)
        {
            bloodEffect.SetBool("fatal", true);         
        }
        else
        {
            bloodEffect.SetBool("fatal", false);
        }
    }
}
