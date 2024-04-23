using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Something_6101_crown : Something
{
    protected override void InitEssentialInfo_something()
    {
        _id_something = "6101";
    }

    // 개별 능력치 초기화 
    public override void InitSomething_custom(Vector3 targetPos)
    {
        pos = targetPos;

        offset = Vector3.zero;
 
        speed = 0f;
        lifeTime = -1;
    }

    // 개별 초기화 
    public override void ActionSomething_custom()
    {
        StageManager.sm.currStage.bossSpawnObject = gameObject;
        StageManager.sm.currStage.bossSpawnPoint = transform.position;
        StageManager.sm.currStage.transform.Find("BossSpawnPoint").position = transform.position;
    }
}
