using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;

public class Aim : MonoBehaviour
{    
    public static Transform t_aim;
    Camera mainCamera;

    [SerializeField] GameObject autoText;

    //======================================================

    void Start()
    {
        t_aim = transform;      // 캐싱
        mainCamera = Camera.main;   //캐싱
        
        GameEvent.ge.onChange_aimMode.AddListener(OnChanageAimMode);   // 타게팅모드변경시 작업
    }

    void LateUpdate()
    {
        Vector3 mousePosRaw =  mainCamera.ScreenToWorldPoint(Input.mousePosition);
        t_aim.position =new Vector3(mousePosRaw.x, mousePosRaw.y, 0); 
    }


    void OnChanageAimMode(bool isAuto)
    {
        autoText.SetActive(isAuto);
    }
}
