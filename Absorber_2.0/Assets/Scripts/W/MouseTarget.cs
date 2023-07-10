using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTarget : MonoBehaviour
{
    public static MouseTarget mt;
    
    public Transform myTransform;
    
    // Start is called before the first frame update
    void Awake()
    {
        myTransform = transform;
  
        mt = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mousePosRaw =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        myTransform.position = new Vector3(mousePosRaw.x, mousePosRaw.y, 0); 
    }
}
