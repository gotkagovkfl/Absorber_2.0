using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManagerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.gm.StartGame();
        StageManager.sm.ChangeStage();
        StageManager.sm.LoadStage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
