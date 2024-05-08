using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public interface ILevelUpOption 
{
    string id {get; }
    public int grade {get;}

    
    public string description {get; }
        

    


    public void OnSelect();
}

//============================= grade1 =============================================
#region Grade 1
//==================
// 최대체력 증가
//===================
public class LevelUpOption_100 : ILevelUpOption
{
    public string id => "100";

    public int grade =>1;
    public string description => $"최대체력 {value} 증가";


    int value =3;


    public void OnSelect()
    {
        Player.playerStatus.Increase_maxHp(value);
    }
}


//==================
// 공격력 증가 
//===================
public class LevelUpOption_101 : ILevelUpOption
{
    public string id => "101";

    public int grade =>1;
    public string description => $"공격력 {value} 증가";



    int value =2;

    public void OnSelect()
    {
        Player.playerStatus.Increase_atk(value);
    }
}

//==================
// 방어력 증가
//===================
public class LevelUpOption_102 : ILevelUpOption
{
    public string id => "102";

    public int grade =>1;
    public string description => $"방어력 {value} 증가";


    int value = 10;

    public void OnSelect()
    {
        Player.playerStatus.Increase_def(value);
    }
}

//==================
// 공격속도 증가
//===================
public class LevelUpOption_103 : ILevelUpOption
{
    public string id => "103";

    public int grade =>1;
    public string description => $"공격속도 증가량 {value}%p 증가";


    int value = 10;

    public void OnSelect()
    {
        Player.playerStatus.Increase_attackSpeedPlus(value);
    }
}

//==================
// 범위 증가 
//===================
public class LevelUpOption_104 : ILevelUpOption
{
    public string id => "104";

    public int grade =>1;
    public string description => $"범위 증가량 {value}%p 증가";


    int value = 10;

    public void OnSelect()
    {
        Player.playerStatus.Increase_range(value);
    }
}

//==================
// 이동속도 증가 
//===================
public class LevelUpOption_105 : ILevelUpOption
{
    public string id => "105";

    public int grade =>1;
    public string description => $"이동속도 증가량 {value}%p 증가";

    int value = 10;

    public void OnSelect()
    {
        Player.playerStatus.Increase_movementSpeedPlus(value);
    }
}

//==================
// 경험치 획득량 증가 
//===================
public class LevelUpOption_106 : ILevelUpOption
{
    public string id => "106";

    public int grade =>1;
    public string description => $"경험치 획득량 {value}%p 증가";



    int value = 10;

    public void OnSelect()
    {
        Player.playerStatus.Increase_expPlus(value);
    }
}

//==================
// 치명타 확률 증가 
//===================
public class LevelUpOption_107 : ILevelUpOption
{
    public string id => "107";

    public int grade =>1;
    public string description => $"치명타확률 {value}%p 증가";


    int value = 5;

    public void OnSelect()
    {
        Player.playerStatus.Increase_critProb(value);
    }
}

//==================
// 강화 공격 확률 증가 
//===================
public class LevelUpOption_108 : ILevelUpOption
{
    public string id => "108";

    public int grade =>1;
    public string description => $"강화공격 확률 {value}%p 증가";



    int value = 10;

    public void OnSelect()
    {
        Player.playerStatus.Increase_reinforceProb(value);
    }
}

//==================
// 회피확률 증가 
//===================
public class LevelUpOption_109 : ILevelUpOption
{
    public string id => "109";

    public int grade =>1;
    public string description => $"회피확률 {value}%p 증가";


    int value = 5;

    public void OnSelect()
    {
        Player.playerStatus.Increase_avoidProb(value);
    }
}

#endregion
//============================= grade2 =============================================
#region Grade 2
//==================
// 최대체력 증가
//===================
public class LevelUpOption_200 : ILevelUpOption
{
    public string id => "200";

    public int grade =>2;
    public string description => $"최대체력 {value} 증가";


    int value =6;


    public void OnSelect()
    {
        Player.playerStatus.Increase_maxHp(value);
    }
}


//==================
// 공격력 증가 
//===================
public class LevelUpOption_201 : ILevelUpOption
{
    public string id => "201";

    public int grade =>2;
    public string description => $"공격력 {value} 증가";



    int value =3;

    public void OnSelect()
    {
        Player.playerStatus.Increase_atk(value);
    }
}

//==================
// 방어력 증가
//===================
public class LevelUpOption_202 : ILevelUpOption
{
    public string id => "202";

    public int grade =>2;
    public string description => $"방어력 {value} 증가";


    int value = 20;

    public void OnSelect()
    {
        Player.playerStatus.Increase_def(value);
    }
}

//==================
// 공격속도 증가
//===================
public class LevelUpOption_203 : ILevelUpOption
{
    public string id => "203";

    public int grade =>2;
    public string description => $"공격속도 증가량 {value}%p 증가";


    int value = 20;

    public void OnSelect()
    {
        Player.playerStatus.Increase_attackSpeedPlus(value);
    }
}

//==================
// 범위 증가 
//===================
public class LevelUpOption_204 : ILevelUpOption
{
    public string id => "204";

    public int grade =>2;
    public string description => $"범위 증가량 {value}%p 증가";


    int value = 20;

    public void OnSelect()
    {
        Player.playerStatus.Increase_range(value);
    }
}

//==================
// 이동속도 증가 
//===================
public class LevelUpOption_205 : ILevelUpOption
{
    public string id => "205";

    public int grade =>2;
    public string description => $"이동속도 증가량 {value}%p 증가";

    int value = 20;

    public void OnSelect()
    {
        Player.playerStatus.Increase_movementSpeedPlus(value);
    }
}

//==================
// 경험치 획득량 증가 
//===================
public class LevelUpOption_206 : ILevelUpOption
{
    public string id => "206";

    public int grade =>2;
    public string description => $"경험치 획득량 {value}%p 증가";



    int value = 20;

    public void OnSelect()
    {
        Player.playerStatus.Increase_expPlus(value);
    }
}

//==================
// 치명타 확률 증가 
//===================
public class LevelUpOption_207 : ILevelUpOption
{
    public string id => "207";

    public int grade =>2;
    public string description => $"치명타확률 {value}%p 증가";


    int value = 10;

    public void OnSelect()
    {
        Player.playerStatus.Increase_critProb(value);
    }
}

//==================
// 강화 공격 확률 증가 
//===================
public class LevelUpOption_208 : ILevelUpOption
{
    public string id => "208";

    public int grade =>2;
    public string description => $"강화공격 확률 {value}%p 증가";



    int value = 20;

    public void OnSelect()
    {
        Player.playerStatus.Increase_reinforceProb(value);
    }
}

//==================
// 회피확률 증가 
//===================
public class LevelUpOption_209 : ILevelUpOption
{
    public string id => "209";

    public int grade =>2;
    public string description => $"회피확률 {value}%p 증가";


    int value = 10;

    public void OnSelect()
    {
        Player.playerStatus.Increase_avoidProb(value);
    }
}

//



//==================
// 행운 증가 
//===================
public class LevelUpOption_210 : ILevelUpOption
{
    public string id => "210";

    public int grade =>2;
    public string description => $"행운 {value} 증가";


    int value = 1;

    public void OnSelect()
    {
        Player.playerStatus.Increase_luck(value);
    }
}


//==================
// 투사체 수  증가 
//===================
public class LevelUpOption_211 : ILevelUpOption
{
    public string id => "211";

    public int grade =>2;
    public string description => $"투사체수 {value} 증가";


    int value = 1;

    public void OnSelect()
    {
        Player.playerStatus.Increase_projNum(value);
    }
}

//==================
// 관통  증가 
//===================
public class LevelUpOption_212 : ILevelUpOption
{
    public string id => "212";

    public int grade =>2;
    public string description => $"관통 {value} 증가";


    int value = 1;

    public void OnSelect()
    {
        Player.playerStatus.Increase_penetration(value);
    }
}

//==================
// 성역 레벨  증가 
//===================
public class LevelUpOption_213 : ILevelUpOption
{
    public string id => "213";

    public int grade =>2;
    public string description => $"성역 레벨 {value} 증가";


    int value = 1;

    public void OnSelect()
    {
        Player.playerStatus.Increase_santuaryLevel(value);
    }
}

//==================
// 폭발 레벨  증가 
//===================
public class LevelUpOption_214 : ILevelUpOption
{
    public string id => "214";

    public int grade =>2;
    public string description => $"공격이 폭발할 확률 {25*value}%p 증가";


    int value = 1;

    public void OnSelect()
    {
        Player.playerStatus.Increase_explosionLevel(value);
    }
}

//==================
// 출혈레벨 증가 
//===================
public class LevelUpOption_215 : ILevelUpOption
{
    public string id => "215";

    public int grade =>2;
    public string description => $"공격이 출혈을 발생시킬 확률이 {value*25}%p 증가";


    int value = 1;

    public void OnSelect()
    {
        Player.playerStatus.Increase_bleedingLevel(value);
    }
}



//==================
// 랜덤 스탯 증가 
//===================
public class LevelUpOption_216 : ILevelUpOption
{
    public string id => "216";

    public int grade =>2;
    public string description => $"랜덤 스탯 증가";


    int value = 1;

    public void OnSelect()
    {
        int s = 10; int e = 25;
        int rand = Random.Range(s,e+1);

        string className = $"LevelUpOption_{rand}";

        // 
        // new Class();
        // class.OnSelect();
    }
}

#endregion


//============================= grade3 =============================================
#region Grade 3

//==================
// 최대체력 증가
//===================
public class LevelUpOption_300 : ILevelUpOption
{
    public string id => "300";

    public int grade =>3;
    public string description => $"최대체력 {value} 증가";


    int value =10;


    public void OnSelect()
    {
        Player.playerStatus.Increase_maxHp(value);
    }
}


//==================
// 공격력 증가 
//===================
public class LevelUpOption_301 : ILevelUpOption
{
    public string id => "301";

    public int grade =>3;
    public string description => $"공격력 {value} 증가";



    int value =4;

    public void OnSelect()
    {
        Player.playerStatus.Increase_atk(value);
    }
}

//==================
// 방어력 증가
//===================
public class LevelUpOption_302 : ILevelUpOption
{
    public string id => "302";

    public int grade =>3;
    public string description => $"방어력 {value} 증가";


    int value = 30;

    public void OnSelect()
    {
        Player.playerStatus.Increase_def(value);
    }
}

//==================
// 공격속도 증가
//===================
public class LevelUpOption_303 : ILevelUpOption
{
    public string id => "303";

    public int grade =>3;
    public string description => $"공격속도 증가량 {value}%p 증가";


    int value = 30;

    public void OnSelect()
    {
        Player.playerStatus.Increase_attackSpeedPlus(value);
    }
}

//==================
// 범위 증가 
//===================
public class LevelUpOption_304 : ILevelUpOption
{
    public string id => "304";

    public int grade =>3;
    public string description => $"범위 증가량 {value}%p 증가";


    int value = 30;

    public void OnSelect()
    {
        Player.playerStatus.Increase_range(value);
    }
}

//==================
// 이동속도 증가 
//===================
public class LevelUpOption_305 : ILevelUpOption
{
    public string id => "305";

    public int grade =>3;
    public string description => $"이동속도 증가량 {value}%p 증가";

    int value = 30;

    public void OnSelect()
    {
        Player.playerStatus.Increase_movementSpeedPlus(value);
    }
}

//==================
// 경험치 획득량 증가 
//===================
public class LevelUpOption_306 : ILevelUpOption
{
    public string id => "306";

    public int grade =>3;
    public string description => $"경험치 획득량 {value}%p 증가";



    int value = 30;

    public void OnSelect()
    {
        Player.playerStatus.Increase_expPlus(value);
    }
}

//==================
// 치명타 확률 증가 
//===================
public class LevelUpOption_307 : ILevelUpOption
{
    public string id => "307";

    public int grade =>3;
    public string description => $"치명타확률 {value}%p 증가";


    int value = 15;

    public void OnSelect()
    {
        Player.playerStatus.Increase_critProb(value);
    }
}

//==================
// 강화 공격 확률 증가 
//===================
public class LevelUpOption_308 : ILevelUpOption
{
    public string id => "308";

    public int grade =>3;
    public string description => $"강화공격 확률 {value}%p 증가";



    int value = 30;

    public void OnSelect()
    {
        Player.playerStatus.Increase_reinforceProb(value);
    }
}

//==================
// 회피확률 증가 
//===================
public class LevelUpOption_309 : ILevelUpOption
{
    public string id => "309";

    public int grade =>3;
    public string description => $"회피확률 {value}%p 증가";


    int value = 15;

    public void OnSelect()
    {
        Player.playerStatus.Increase_avoidProb(value);
    }
}

//==================
// 분열레벨 증가 
//===================
public class LevelUpOption_310 : ILevelUpOption
{
    public string id => "310";

    public int grade =>3;
    public string description => $"분열 레벨 {value} 증가";


    int value = 1;

    public void OnSelect()
    {
        Player.playerStatus.Increase_split(value);
    }
}

//==================
// 쉴드 획득 
//===================
public class LevelUpOption_311 : ILevelUpOption
{
    public string id => "311";

    public int grade =>3;
    public string description => $"20초마다 모든 공격을 1회 막는 쉴드를 생성";


    int value = 1;

    public void OnSelect()
    {
        Player.playerStatus.Get_shield();
    }
}
//==================
// 피격시 경험치 획득 
//===================
public class LevelUpOption_312 : ILevelUpOption
{
    public string id => "312";

    public int grade =>3;
    public string description => $"피격시 {value}%의 경험치 획득";


    int value = 10;

    public void OnSelect()
    {
        Player.playerStatus.Increase_onHitExp(value);
    }
}

//==================
// 정지시 방어력 100 증가
//===================
public class LevelUpOption_313 : ILevelUpOption
{
    public string id => "313";

    public int grade =>3;
    public string description => $"정지시 방어력{value} 증가";


    int value = 100;

    public void OnSelect()
    {
        Player.playerStatus.Increase_stopDef(value);
    }
}


#endregion

//============================= grade4 =============================================
#region Grade 4

//==================
// 최대체력 증가
//===================
public class LevelUpOption_400 : ILevelUpOption
{
    public string id => "400";

    public int grade =>4;
    public string description => $"최대체력 {value} 증가";


    int value =15;


    public void OnSelect()
    {
        Player.playerStatus.Increase_maxHp(value);
    }
}
//==================
// 공격력 증가 
//===================
public class LevelUpOption_401 : ILevelUpOption
{
    public string id => "401";

    public int grade =>4;
    public string description => $"공격력 {value} 증가";



    int value =5;

    public void OnSelect()
    {
        Player.playerStatus.Increase_atk(value);
    }
}

//==================
// 방어력 증가
//===================
public class LevelUpOption_402 : ILevelUpOption
{
    public string id => "402";

    public int grade =>4;
    public string description => $"방어력 {value} 증가";


    int value = 40;

    public void OnSelect()
    {
        Player.playerStatus.Increase_def(value);
    }
}

//==================
// 공격속도 증가
//===================
public class LevelUpOption_403 : ILevelUpOption
{
    public string id => "403";

    public int grade =>4;
    public string description => $"공격속도 증가량 {value}%p 증가";


    int value = 40;

    public void OnSelect()
    {
        Player.playerStatus.Increase_attackSpeedPlus(value);
    }
}

//==================
// 범위 증가 
//===================
public class LevelUpOption_404 : ILevelUpOption
{
    public string id => "404";

    public int grade =>4;
    public string description => $"범위 증가량 {value}%p 증가";


    int value = 40;

    public void OnSelect()
    {
        Player.playerStatus.Increase_range(value);
    }
}

//==================
// 이동속도 증가 
//===================
public class LevelUpOption_405 : ILevelUpOption
{
    public string id => "405";

    public int grade =>4;
    public string description => $"이동속도 증가량 {value}%p 증가";

    int value = 40;

    public void OnSelect()
    {
        Player.playerStatus.Increase_movementSpeedPlus(value);
    }
}

//==================
// 경험치 획득량 증가 
//===================
public class LevelUpOption_406 : ILevelUpOption
{
    public string id => "406";

    public int grade =>4;
    public string description => $"경험치 획득량 {value}%p 증가";



    int value = 40;

    public void OnSelect()
    {
        Player.playerStatus.Increase_expPlus(value);
    }
}

//==================
// 치명타 확률 증가 
//===================
public class LevelUpOption_407 : ILevelUpOption
{
    public string id => "407";

    public int grade =>4;
    public string description => $"치명타확률 {value}%p 증가";


    int value = 20;

    public void OnSelect()
    {
        Player.playerStatus.Increase_critProb(value);
    }
}

//==================
// 강화 공격 확률 증가 
//===================
public class LevelUpOption_408 : ILevelUpOption
{
    public string id => "408";

    public int grade =>4;
    public string description => $"강화공격 확률 {value}%p 증가";



    int value = 40;

    public void OnSelect()
    {
        Player.playerStatus.Increase_reinforceProb(value);
    }
}

//==================
// 회피확률 증가 
//===================
public class LevelUpOption_409 : ILevelUpOption
{
    public string id => "409";

    public int grade =>4;
    public string description => $"회피확률 {value}%p 증가";


    int value = 20;

    public void OnSelect()
    {
        Player.playerStatus.Increase_avoidProb(value);
    }
}

//==================
// 목숨 +1
//================
public class LevelUpOption_410 : ILevelUpOption
{
    public string id => "410";

    public int grade =>4;
    public string description => $"목숨 {value} 증가";


    int value = 1;

    public void OnSelect()
    {
        Player.playerStatus.Increase_life(value);
    }
}
#endregion