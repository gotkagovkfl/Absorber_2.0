using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossAppear : MonoBehaviour
{

    public GameObject boss;

    public void showBoss()
    {
        boss.SetActive(true);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().speed = 0.75f;
        

        boss = GameObject.Find("Boss_001");
        boss.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
