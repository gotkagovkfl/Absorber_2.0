using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Reflection;
using System;

//===================================================
// 플레리어 상태 관리자 : 외부 요소에 의한 플레이어 능력치를 제어하고, 넉백, 기절 등 디버프 및 강화 버프 등을 적용한다. 
//===================================================
public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager psm;

    public enum ChangeType {add, mul, set}

    //==================================================================================
    
    // ======================================
    // 플레이어의 스탯을 일정 시간동안 변화시킨다.
    // ======================================
    public void ChangeStat(string stat, int value, float time, ChangeType type)
    {
        FieldInfo field = typeof(Player).GetField(stat);
        if (field == null)  
        {
            return;
        }
        Type t = field.GetValue(Player.Instance).GetType();

        if (t == typeof(int))
        {
            Debug.Log(t);
            int originalValue = (int) (field.GetValue(Player.Instance));


            // type 에 따라 새 능력치 값 결정
            float newValue= originalValue;
            switch( type )
            {
                case ChangeType.add:
                    newValue = originalValue + value;
                    break;
                case ChangeType.mul:
                    newValue = originalValue * value;
                    break;
                case ChangeType.set:
                    newValue = value;
                    break;
            }
            // 결정한 새 능력치 값 적용
            field.SetValue(Player.Instance, newValue);
            
            // time 후에 다시 원래 값으로 복구 
            StartCoroutine(SetDuration(field, originalValue, time));
        }
    }
    
    public IEnumerator SetDuration(FieldInfo field, int originalValue, float time)
    {
        yield return new WaitForSeconds( time ) ;
        field.SetValue(Player.Instance, originalValue);
    }
    

    // ======================================
    // 플레이어 스턴 
    // ======================================
    public void Stunned(float time)
    {
        Player.Instance.canMove = false;
        Player.Instance.canAttack = false;

        StartCoroutine( SetDuration_stun(time));
    }

    public IEnumerator SetDuration_stun(float time)
    {
        yield return new WaitForSeconds(time);

        Player.Instance.canMove = true;
        Player.Instance.canAttack = true;
    }
    
    
    //============================================
    void Awake()
    {
        psm = this;
    }

    void Start()
    {

    }

   
    



}
