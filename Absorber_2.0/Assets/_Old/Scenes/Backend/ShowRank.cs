using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using System.Text;
using TMPro;
using LitJson;

public class ShowRank : MonoBehaviour
{
    public Button btn;
    public TMP_Text text;
    public GameObject RankingBoard;
    public TMP_Text MyRank;
    // Start is called before the first frame update
    public void RankingBtnClick()
    {
        Fade.fade.BtnClickSound();
        
        //RankingBoard = GameObject.Find("RankingBoard");
        RankingBoard.SetActive(true);
        try
        {
            JsonData MyRankInfo = Backend.URank.User.GetMyRank(BackendRank.Instance.rankUUID).GetFlattenJSON();
            MyRank.text = string.Format("{0,-14}", MyRankInfo["rows"][0]["rank"].ToString()) + string.Format("{0,-12}", MyRankInfo["rows"][0]["nickname"].ToString()) + string.Format("{0,-8}", MyRankInfo["rows"][0]["score"].ToString());
        }
        catch
        {
            string MyRanking =  string.Format("{0,-13}", "X") + string.Format("{0,-12}", Backend.UserNickName) + string.Format("{0,-7}", "0");
            MyRank.text = MyRanking;
        }
        BackendRank.Instance.RankGet();
        StringBuilder Ranking = BackendRank.Instance.info;
        text.text = Ranking.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
