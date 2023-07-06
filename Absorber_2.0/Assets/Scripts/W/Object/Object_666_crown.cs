using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_666_crown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StageManager.sm.currStage.bossSpawnObject = gameObject;
        StageManager.sm.currStage.bossSpawnPoint = transform.position;
        StageManager.sm.currStage.transform.Find("BossSpawnPoint").position = transform.position;

    }
}
