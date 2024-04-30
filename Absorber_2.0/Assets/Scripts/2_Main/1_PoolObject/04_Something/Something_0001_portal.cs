using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Something_0001_portal : Something
{   
    protected override void InitEssentialInfo_something()
    {
        _id_something = "0001";
    }

    //
    public override void InitSomething_custom(Vector3 targetPos)
    {
        
    }

    //========================
    // 액션 : 포탈이 완전히 활성화되기 전에는 통과할 수 없음
    //========================
    public override void ActionSomething_custom()
    {
        rb.simulated = false;
        StartCoroutine( RigidOn() );
    }

    //==================================
    // 포탈 콜라이더 지연 활성화
    //=================================
    public IEnumerator RigidOn()
    {
        yield return new WaitForSeconds(3f);
        rb.simulated = true;
    }
    

    //===========================
    // 포탈 진입시 이벤트
    //===========================
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // audioSource.PlayOneShot( audioSource.clip );
            
            // 포탈 진입 이펙트 
            SomethingPoolManager.instance.CreateSomething("0002", Player.player.t_player.position );
            
            // 스테이지 전환 및 포탈 1초후에 파괴 (1초는 페이드 아웃 시간)
            StageManager.sm.GoToNextStage();

            //
            _isDead = true;
        }
    }

}
