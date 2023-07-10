using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 분열을 위해 에너미 타입 설정 (소형/대형) 
// 현재 소형은 일반 적, 대형은 엘리트 or 보스 => 나중에는 세부적으로 조정
// 나중에는 Enemy 스크립트 내부로 옮기자
public class EnemyType : MonoBehaviour
{
    public enum Type  {small, large}

    public Type enemyType;

    public void InitType()
    {
        if (GetComponent<Enemy>() != null)
        {
            enemyType = Type.small;
        }
        else
        {
            enemyType = Type.large;
        }  
    }
}

