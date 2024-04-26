using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Aim : MonoBehaviour
{    
    static Transform t_aim;
    Camera mainCamera;

    [SerializeField] GameObject autoText;


    public static Vector3 GetAimPos 
    {
        get
        {
            Vector3 ret = Vector3.zero;

            if (t_aim)
            {
                ret=t_aim.position;
            }
            
            return ret;
        }
     }


    //======================================================

    void Start()
    {
        t_aim = transform;      // 캐싱
        mainCamera = Camera.main;   //캐싱
        
        GameEvent.ge.onChange_aimMode.AddListener(OnChanageAimMode);   // 타게팅모드변경시 작업
    }

    void Update()
    {
        t_aim.position = Input.mousePosition;

        // Debug.Log(Aim.GetAimPos);
    }


    void OnChanageAimMode(bool isAuto)
    {
        autoText.SetActive(isAuto);
    }
}
