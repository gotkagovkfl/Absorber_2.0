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

    // ü�� 30% �̸��� �� ����������Ʈ �߻�
    void Update()
    {
        Debug.Log(player.hp_curr);
        Debug.Log(player.hp_max);
        if (player.hp_curr < player.hp_max * 0.3 && player.hp_curr > 0)
        {
            bloodEffect.SetBool("fatal", true);         
        }
        else
        {
            bloodEffect.SetBool("fatal", false);
        }
    }
}
