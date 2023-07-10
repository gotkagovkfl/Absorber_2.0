using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_001_AutoAim : MonoBehaviour
{
    public Transform myTransform;
    
    // Start is called before the first frame update
    void Awake()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosRaw =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        myTransform.position =new Vector3(mousePosRaw.x, mousePosRaw.y, 0); 
    }
}
