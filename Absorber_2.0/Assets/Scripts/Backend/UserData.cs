using System.Collections.Generic;
using System.Text;
using UnityEngine;

// 뒤끝 SDK namespace 추가
using BackEnd;

public class UserData
{
    public string NickName = Backend.UserNickName; // 유저 닉네임
    public int level = 1;
    public int Atk = 0; // 공격력
    public float Max_Hp = 100; // 최대체력
    public float Range = 0; // 공격 사거리 & 아이템 획득 범위
    public float Speed = 5f; // 이동속도
    public float Attack_Speed = 0; // 공격속도
    public int Def = 0; // 방어력
    public float Drain = 0; // 흡혈
    public int Drain_prob = 0; // 흡혈확률
    public int Avoid_prob = 0; // 회피확률
    public int KillCount = 0; // 처치한 몬스터 수

    public string info = string.Empty;
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    public List<string> equipment = new List<string>();

    // 데이터를 디버깅하기 위한 함수입니다.(Debug.Log(UserData);)
    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine($"NickName : {NickName}");
        result.AppendLine($"level : {level}");
        result.AppendLine($"Atk : {Atk}");
        result.AppendLine($"Max_Hp : {Max_Hp}");
        result.AppendLine($"Range : {Range}");
        result.AppendLine($"Speed : {Speed}");
        result.AppendLine($"Attack_Speed : {Attack_Speed}");
        result.AppendLine($"Def : {Def}");
        result.AppendLine($"Drain : {Drain}");
        result.AppendLine($"Drain_prob : {Drain_prob}");
        result.AppendLine($"Avoid_prob : {Avoid_prob}");
        result.AppendLine($"KillCount : {KillCount}");
        result.AppendLine($"info : {info}");

        return result.ToString();
    }
}

public class BackendGameData
{
    private static BackendGameData _instance = null;

    public static BackendGameData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendGameData();
            }

            return _instance;
        }
    }

    public static UserData userData;

    private string gameDataRowInDate = string.Empty;

    public void GameDataInsert()
    {
        if (userData == null)
        {
            userData = new UserData();
        }

        Debug.Log("데이터를 초기화합니다.");
        userData.level = 1;
        userData.Atk = 0;
        userData.Max_Hp = 100;
        userData.Range = 0;
        userData.Speed = 5f;
        userData.Attack_Speed = 0;
        userData.Def = 0;
        userData.Drain = 0;
        userData.Drain_prob = 0;
        userData.Avoid_prob = 0;
        userData.KillCount = 0;
        Debug.Log("뒤끝 업데이트 목록에 해당 데이터들을 추가합니다.");
        Param param = new Param();
        param.Add("레벨", userData.level);
        param.Add("닉네임", userData.NickName);
        param.Add("공격력", userData.Atk);
        param.Add("최대체력", userData.Max_Hp);
        param.Add("사거리", userData.Range);
        param.Add("이동속도", userData.Speed);
        param.Add("공격속도", userData.Attack_Speed);
        param.Add("방어력", userData.Def);
        param.Add("흡혈", userData.Drain);
        param.Add("흡혈확률", userData.Drain_prob);
        param.Add("회피확률", userData.Avoid_prob);
        param.Add("KillCount", userData.KillCount);
        param.Add("info", userData.info);

        Debug.Log("게임정보 데이터 삽입을 요청합니다.");
        var bro = Backend.GameData.Insert("USER_DATA", param);

        if (bro.IsSuccess())
        {
            Debug.Log("게임정보 데이터 삽입에 성공했습니다. : " + bro);

            //삽입한 게임정보의 고유값입니다.
            gameDataRowInDate = bro.GetInDate();
        }
        else
        {
            Debug.LogError("게임정보 데이터 삽입에 실패했습니다. : " + bro);
        }
    }

    public void GameDataGet()
    {
        Debug.Log("게임 정보 조회 함수를 호출합니다.");
        var bro = Backend.GameData.GetMyData("USER_DATA", new Where());
        if (bro.IsSuccess())
        {
            Debug.Log("게임 정보 조회에 성공했습니다. : " + bro);


            LitJson.JsonData gameDataJson = bro.FlattenRows(); // Json으로 리턴된 데이터를 받아옵니다.

            // 받아온 데이터의 갯수가 0이라면 데이터가 존재하지 않는 것입니다.
            if (gameDataJson.Count <= 0)
            {
                Debug.LogWarning("데이터가 존재하지 않습니다.");
            }
            else
            {
                gameDataRowInDate = gameDataJson[0]["inDate"].ToString(); //불러온 게임정보의 고유값입니다.

                userData = new UserData();

                userData.level = int.Parse(gameDataJson[0]["레벨"].ToString());
                userData.NickName = gameDataJson[0]["닉네임"].ToString();
                userData.Atk = int.Parse(gameDataJson[0]["공격력"].ToString());
                userData.Max_Hp = int.Parse(gameDataJson[0]["최대체력"].ToString());
                userData.Range = float.Parse(gameDataJson[0]["사거리"].ToString());
                userData.Speed = float.Parse(gameDataJson[0]["이동속도"].ToString());
                userData.Attack_Speed = float.Parse(gameDataJson[0]["공격속도"].ToString());
                userData.Def = int.Parse(gameDataJson[0]["방어력"].ToString());
                userData.Drain = float.Parse(gameDataJson[0]["흡혈"].ToString());
                userData.Drain_prob = int.Parse(gameDataJson[0]["흡혈확률"].ToString());
                userData.Avoid_prob = int.Parse(gameDataJson[0]["회피확률"].ToString());
                userData.KillCount = int.Parse(gameDataJson[0]["KillCount"].ToString());
                userData.info = gameDataJson[0]["info"].ToString();
                Debug.Log(userData.ToString());
            }
        }
        else
        {
            Debug.LogError("게임 정보 조회에 실패했습니다. : " + bro);
        }
    }

    public void LevelUp()
    {
        Debug.Log("레벨을 1 증가시킵니다.");
        userData.level += 1;
        userData.info = "내용을 변경합니다.";
    }

    // 게임정보 수정하기
    public void GameDataUpdate()
    {
        if (userData == null)
        {
            Debug.LogError("서버에서 다운받거나 새로 삽입한 데이터가 존재하지 않습니다. Insert 혹은 Get을 통해 데이터를 생성해주세요.");
            return;
        }

        Param param = new Param();
        param.Add("레벨", userData.level);
        param.Add("info", userData.info);

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(gameDataRowInDate))
        {
            Debug.Log("내 제일 최신 게임정보 데이터 수정을 요청합니다.");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }
        else
        {
            Debug.Log($"{gameDataRowInDate}의 게임정보 데이터 수정을 요청합니다.");

            bro = Backend.GameData.UpdateV2("USER_DATA", gameDataRowInDate, Backend.UserInDate, param);
        }

        if (bro.IsSuccess())
        {
            Debug.Log("게임정보 데이터 수정에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("게임정보 데이터 수정에 실패했습니다. : " + bro);
        }
    }
}