using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//==================================================================
/// <summary>
/// 게임을 시작하기 위한 작업
/// </summary>
//==================================================================
public abstract class InitWork
{
    public string workName;
    
    //----------------------------------------------------
    public InitWork()
    {
        Init_custom();
    }

    //---------------------------------------------------
    /// <summary>
    /// 초기화 작업을 초기화한다. 
    /// </summary>
    protected abstract void Init_custom();


    /// <summary>
    /// 초기화 작업을 진행한다. - 보통 네트워킹을 사용할 것 같기 때문에, IEnumerator
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerator WorkProgress();
}




//=======================================================================
/// <summary>
/// 테스트 초기화 작업 - n초 뒤 작업이 완료된다. 
/// </summary>
// =====================================================================
public class InitWork_TestInit : InitWork
{
    float interval;
    
    protected override void Init_custom()
    {
        interval = Random.Range(0.1f,1f);

        workName = "테스트 웤 - "+interval; 
    }

    public override IEnumerator WorkProgress()
    {
        yield return new WaitForSeconds(  interval  );
    }
}
