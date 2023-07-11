using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
public class Ranking_Insert : MonoBehaviour
{
    public Button btn;
    public void onBtnClick()
    {
        if(GameManager.gm.gameClear)
            BackendRank.Instance.RankInsert((int)Mathf.Round(GameManager.gm.Score) * (int)Mathf.Max(1,(330 - GameManager.gm.Stage1_PlayerTime)) );
        else
            BackendRank.Instance.RankInsert(GameManager.gm.Score);
    }
}
           