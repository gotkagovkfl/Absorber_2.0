using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using System.Text;
public class BackendRank
{

    private static BackendRank _instance = null;
    public string rankUUID = "777c6210-dddb-11ed-81d1-5d1bbad2ea4e";
    public static BackendRank Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendRank();
            }

            return _instance;
        }
    }
    public StringBuilder info;
    public void RankInsert(int score)
    {
        // [변경 필요] '복사한 UUID 값'을 '뒤끝 콘솔 > 랭킹 관리'에서 생성한 랭킹의 UUID값으로 변경해주세요.
        // 예시 : "4088f640-693e-11ed-ad29-ad8f0c3d4c70"

        string tableName = "USER_DATA";
        string rowInDate = string.Empty;

        // 랭킹을 삽입하기 위해서는 게임 데이터에서 사용하는 데이터의 inDate값이 필요합니다.
        // 따라서 데이터를 불러온 후, 해당 데이터의 inDate값을 추출하는 작업을 해야합니다.
        //Debug.Log("데이터 조회를 시도합니다.");
        var bro = Backend.GameData.GetMyData(tableName, new Where());

        if (bro.IsSuccess() == false)
        {
            //Debug.LogError("데이터 조회 중 문제가 발생했습니다 : " + bro);
            return;
        }

        //Debug.Log("데이터 조회에 성공했습니다 : " + bro);
        int ExistingScore = 0;
        if (bro.FlattenRows().Count > 0)
        {
            rowInDate = bro.FlattenRows()[0]["inDate"].ToString();
            ExistingScore = int.Parse(Backend.URank.User.GetMyRank(BackendRank.Instance.rankUUID).GetFlattenJSON()["rows"][0]["score"].ToString());
        }
        else
        {
            //Debug.Log("데이터가 존재하지 않습니다. 데이터 삽입을 시도합니다.");
            ExistingScore = 0;
            var bro2 = Backend.GameData.Insert(tableName);

            if (bro2.IsSuccess() == false)
            {
                //Debug.LogError("데이터 삽입 중 문제가 발생했습니다 : " + bro2);
                return;
            }

            //Debug.Log("데이터 삽입에 성공했습니다 : " + bro2);

            rowInDate = bro2.GetInDate();
        }

        //Debug.Log("내 게임 정보의 rowInDate : " + rowInDate); // 추출된 rowIndate의 값은 다음과 같습니다.

        Param param = new Param();
        param.Add("KillCount", score);

        // 추출된 rowIndate를 가진 데이터에 param값으로 수정을 진행하고 랭킹에 데이터를 업데이트합니다.
        //Debug.Log("랭킹 삽입을 시도합니다.");
        //Debug.Log(ExistingScore);
        if (ExistingScore == 0 || ExistingScore < score)
        {
            var rankBro = Backend.URank.User.UpdateUserScore(rankUUID, tableName, rowInDate, param);

            if (rankBro.IsSuccess() == false)
            {
                //Debug.LogError("랭킹 등록 중 오류가 발생했습니다. : " + rankBro);
                return;
            }

            //Debug.Log("랭킹 삽입에 성공했습니다. : " + rankBro);
        }
    }

    public void RankGet()
    {
        string rankUUID = "777c6210-dddb-11ed-81d1-5d1bbad2ea4e"; // 예시 : "4088f640-693e-11ed-ad29-ad8f0c3d4c70"
        var bro = Backend.URank.User.GetRankList(rankUUID);

        if (bro.IsSuccess() == false)
        {
            //Debug.LogError("랭킹 조회중 오류가 발생했습니다. : " + bro);
            return;
        }
        //Debug.Log("랭킹 조회에 성공했습니다. : " + bro);

       // Debug.Log("총 랭킹 등록 유저 수 : " + bro.GetFlattenJSON()["totalCount"].ToString());
        info = new StringBuilder();
        foreach (LitJson.JsonData jsonData in bro.FlattenRows())
        {
            info.AppendLine(string.Format("{0,-13}", jsonData["rank"].ToString()) + string.Format("{0,-12}", jsonData["nickname"].ToString()) + string.Format("{0,-10}", jsonData["score"].ToString()));
            info.AppendLine();
            //Debug.Log(info);
        }
    }
}