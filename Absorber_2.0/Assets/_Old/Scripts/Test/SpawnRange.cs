using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRange : MonoBehaviour
{
    public GameObject o;
    int thisTime= 0;
    Collider2D[] colls;
    // Start is called before the first frame update
    void Start()
    {
        colls = GetComponents<Collider2D>();
        // foreach(var i in colls)
        // {
        //     Debug.Log(i.bounds.size.x);
        //     Debug.Log(i.bounds.size.y);
        //     Debug.Log("===================");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        // Instantiate(o, GetRandPos(), Quaternion.identity);
        
        // GetRandPos();
    }

    public Vector3 GetRandPos()
    {
        Vector3 ret =  new Vector3(colls[thisTime].bounds.center.x,  colls[thisTime].bounds.center.y);
        
        float boundX = colls[thisTime].bounds.size.x;
        float boundY = colls[thisTime].bounds.size.y;

        ret +=  new Vector3( Random.Range(-boundX/2, boundX/2), Random.Range(-boundY/2, boundY/2)  );

        thisTime = (thisTime+1)%2;
    
        return ret;
    }

}
