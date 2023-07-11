using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public GameObject prefab_dummy;

    public GameObject dummy;
    
    public void SpawnDummy()
    {
        if (dummy != null)
        {
            return;
        }

        GameObject sm = GameObject.Find("SpawnManager");


        Vector3 pos = new Vector3( -5, 0,0 );
        dummy = Instantiate(prefab_dummy,sm.transform);

        dummy.transform.position = pos;
    }
}
