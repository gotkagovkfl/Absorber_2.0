using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PlayerStatus 
{
  public int level = 1;         // 레벨
  public int atk;               // 공격력
  public int hp_curr;           // 체력
  public int hp_max;            // 최대체력
  public float exp_curr;        // 현제 경험치
  public float exp_max;         // 레벨업 필요 경험치
  public int exp_plus;            //경험치 획득량 증가 ( % )
  public int range_plus;        // 사거리 및 범위 증가량
  public float movementSpeed;   // 이동속도
  public int movementSpeed_plus;  // 이속 증가량 (%)
  public int attackSpeed_plus;  //공속 증가량
  public int def;               // 방어력
  public int def_onStop;      // 정지시 방어력 추가 
  public bool haveSheild;     // 보호막 보유중인지, 
  public bool isInvincible;   // 무적 상태인지
  public int drain_amount;    // 흡혈량
  public int drain_prob;      // 흡혈확률
  public int penetration;     // 관통레벨
  public int avoid_prob;    // 회피확률
  public float avoid_time;    // 회피시간
  // public int avoid_select_count;  //뭐야이건?
  public float amount_healingItem; // 고기 회복량
  public int crit_prob;           // 치명타 확률
  public int reinforce_prob; // 강화공격 확률
  public int life;          // 목숨 
  // public int avoid_atk;     // 회피공격? -삭제  
  // public bool canGetExp_onHit;    // 피격시 경험치 획득 가능 여부
  public int  exp_amount_onHit; //  피격시 경험치 획득량

  // public float projScale;   // 투사체 크기
  // public float projLifeTime;  //투사체 수명
  // public float projSpeed;   //투사체 속도

  //
  public int projNum;   // 투사체 수
  public int split;    // 분열 수
  public int explosionLevel;    //폭발레벨
  public int bleedingLevel;   //출혈 레벨
  public int sanctuaryLevel;    //성역 레벨

  public int luck {get; private set;} // 행운

  // 능력치 초기화  - 나머지는 0, false 로 리셋
  public PlayerStatus()
  {
    hp_max = 100;
    hp_curr = hp_max;
    exp_max = 20;
    movementSpeed = 4f;
    amount_healingItem = 5;
    life = 1;
  }



  // 레벨업 
  public void LevelUp()
  {
    exp_curr -= exp_max;
    Increase_maxExp_onLevelUp();
    level ++;
  }

  //공격력증가
  public void Increase_atk(int value)
  {
    atk+= value;
  }

  //공격력감소
  public void Decrease_atk(int value)
  {
    atk -= value;
  }

  // 현재체력 증가
  public void Increase_currHp(int value)
  {
    hp_curr += value;
    hp_curr = Mathf.Min(hp_curr, hp_max); 
  }

  // 현재체력감소 
  public void Decrease_currHp(int value)
  {
    hp_curr -= value;
    hp_curr = Mathf.Max(0, hp_curr);
  }

  // 최대체력 증가
  public void Increase_maxHp(int value)
  {
    hp_max += value;
    Increase_currHp(value);
  }

  // 최대체력감소
  public void Decrease_maxHp(int value)
  {
    hp_max -= value;
    hp_curr = Mathf.Min(hp_curr, hp_max); // 현재체력 조정
  }

  // 현재 경험치 증가
  public void Increase_currExp(float value)
  {
    exp_curr += value * (1 + exp_plus * 0.01f);
  }

  // 레벨업시 경험치 목표량 증가
  public void Increase_maxExp_onLevelUp()
  {
    exp_max *= 1.18f;
  }

  // 경험치획득비율증가
  public void Increase_expPlus(int value)
  {
    exp_plus += value;
  }

  // 경험치획득비율감소
  public void Decrease_expPlus(int value)
  {
    exp_plus -= value;
    exp_plus = Mathf.Max(-90, exp_plus);    // 하한 설정 
  }

  // 범위증가
  public void Increase_range(int value)
  {
    range_plus += value;
  }

  // 범위감소
  public void Decrease_range(int value)
  {
    range_plus -= value;
    range_plus = Mathf.Max(-90, range_plus);    // 하한 설정 
  }

  // 이속증가량 증가
  public void Increase_movementSpeedPlus(int value)
  {
    movementSpeed_plus += value;
  }

  // 이속증가량 감소
  public void Decrease_movementSpeedPlus(int value)
  {
    movementSpeed_plus -= value;

    movementSpeed_plus = Mathf.Max(-90 , movementSpeed_plus); // 하한 설정
  }

  // 공속 증가량 증가
  public void Increase_attackSpeedPlus(int value)
  {
    attackSpeed_plus += value;
  }
  // 공속 증가량 증가
  public void  Decrease_attackSpeedPlus(int value)
  {
    attackSpeed_plus -= value;

    attackSpeed_plus = Mathf.Max( -90, attackSpeed_plus  ); // 하한 설정
  }

  //방어력 증가
  public void Increase_def(int value)
  {
    def+= value;
  }

  //방어력 감소
  public void Decrease_def(int value)
  {
    def -= value;
  }
  
  // 정지상태 방어력 증가
  public void Increase_stopDef(int value)
  {
    def_onStop += value;
  }

  // 쉴드 획득
  public void Get_shield()
  {
    haveSheild = true;
  }

  // 쉴드 잃음
  public void Lose_shield()
  {
    haveSheild = false;
  }

  // 무적상태 됨
  public void Get_invincible()
  {
    isInvincible = true;
  }

  // 무적상태 잃음
  public void Lose_invincible()
  {
    isInvincible = false;
  }

  // 흡혈량 증가
  public void Increase_drainAmount(int value)
  {
    drain_amount += value;
  }

  // 흡혈량 감소
  public void Decrease_drainAmount(int value)
  {
    drain_amount -= value;
    drain_amount = Mathf.Max( 0, drain_amount);
  }

  // 흡혈확률 증가
  public void Increase_drainProb(int value)
  {
    drain_prob += value;
  }

  // 흡혈확률 감소
  public void Decrease_drainProb(int value)
  {
    drain_prob -= value;
  }

  // 관통 증가
  public void Increase_penetration(int value)
  {
    penetration += value;
  }

  // 관통감소
  public void Decrease_penetration(int value)
  {
    penetration -= value;

    penetration = Mathf.Max( 0 , penetration);
  }

  // 회피 확률 증가
  public void Increase_avoidProb(int value)
  {
    avoid_prob += value;
  }

  // 회피 확률 감소
  public void Decrease_avoidProb(int value)
  {
    avoid_prob -= value;
  }

  
  // public void Increase_avoidTime(int value)
  // {
  //   avoid_time += value;
  // }

  // 힐링 아이템 회복량 증가
  public void Increase_healingItemValue(int value)
  {
    amount_healingItem +=value;
  }

// 힐링 아이템 회복량 감소
  public void Decrease_healingItemValue(int value)
  {
    amount_healingItem -= value;
  }

// 크리 확률 증가
  public void Increase_critProb(int value)
  {
    crit_prob += value;
  }

// 크리 확률 감소
  public void Decrease_critProb(int value)
  {
    crit_prob -= value;
  }

// 강화 공격 확률 증가
  public void Increase_reinforceProb(int value)
  {
    reinforce_prob += value;
  }

// 강화 공격 확률 감소
  public void Decrease_reinforceProb(int value)
  {
    reinforce_prob -= value;
  }

  //목숨 증가
  public void Increase_life(int value)
  {
    life += value;
  }

  // 목숨 감소
  public void Decrease_life(int value)
  {
    life -= value;
  }

  //피격시 경험치 획득량 증가
  public void Increase_onHitExp(int value)
  {
    exp_amount_onHit += value;
  }

//피격시 경험치 획득량 감소
  public void Decrease_onHitExp(int value)
  {
    exp_amount_onHit -= value;

    exp_amount_onHit = Mathf.Max( 0, exp_amount_onHit);
  }

  // 투사체 수 증가량 증가
  public void Increase_projNum(int value)
  {
    projNum += value;
  }

  // 투사체 수 증가량 감소
  public void Decrease_projNum(int value)
  {
    projNum -= value; 
    projNum = Mathf.Max( 0, projNum);
  }

// 분열 증가
  public void Increase_split(int value)
  {
    split += value;
  }

  // 분열 감소
  public void Decrease_split(int value)
  {
    split -= value;

    split = Mathf.Max(0, split);
  }

  // 폭발레벨 증가
  public void Increase_explosionLevel(int value)
  {
    explosionLevel += value;
  }

  // 폭발레벨 감소
  public void Decrease_explosionLevel(int value)
  {
    explosionLevel -= value;
    explosionLevel = Mathf.Max(0, explosionLevel);
  }

  // 출혈 레벨 증가
  public void Increase_bleedingLevel(int value)
  {
    bleedingLevel += value;
  }

  // 출혈 레벨 감소
  public void Decrease_bleedingLevel(int value)
  {
    bleedingLevel -= value;
    bleedingLevel = Mathf.Max(0, bleedingLevel);
  }

  // 성역 레벨 증가
  public void Increase_santuaryLevel(int value)
  {
    sanctuaryLevel += value;
  }

  // 성역 레벨 감소 
  public void Decrease_santuaryLevel(int value)
  {
    sanctuaryLevel -= value;
    sanctuaryLevel = Mathf.Max(0, sanctuaryLevel);
  }

  public void Increase_luck(int value)
  {
    luck+= value;
    luck = Math.Min(luck, 4);
  }

  public void Decrease_luck(int value)
  {
    luck-= value;
    luck = Mathf.Max(0, luck);
  }





// 디버그용 
public void Set_split(int value)
{
  split = value;
}

public void Set_projNum(int value)
{
  projNum = value;
}
public void Set_explosion(int value)
{
  explosionLevel = value;
}
public void Set_bleeding(int value)
{
  bleedingLevel = value;
}

public void Set_sancutary(int value)
{
  sanctuaryLevel = value;
}












}
