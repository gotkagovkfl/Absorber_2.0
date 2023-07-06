using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnExp : MonoBehaviour
{
    public GameObject prefab_exp;
    public GameObject ground;



    public void CreateExp()
    {
        float minX = ground.transform.position.x-20;
        float maxX = ground.transform.position.x+20;
    
        float minY = ground.transform.position.y-20;
        float maxY = ground.transform.position.y+20;

        float newX = Random.Range(minX,maxX);
        float newY = Random.Range(minY,maxY);

        Vector3 newPos = new Vector3(newX, newY, 0);

        GameObject exp = Instantiate(prefab_exp, GameObject.Find("Items").transform);
        exp.transform.position = newPos;
    }

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
