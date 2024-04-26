using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class PlayerProgress : MonoBehaviour
{
    // hp
    [SerializeField] Slider slider_hp;

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
    }


    void FixedUpdate()
    {
        // transform.position = Camera.main.WorldToScreenPoint( t_player.position);
    }

    //==================================================================
}
