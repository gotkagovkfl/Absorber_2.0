using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Something_0120_sanctuary : Something
{
    bool inCircle;

    float currTime;
    float lastHealTime;
    float lastAttackTime;
    
    int asPlus;
    
    //
    protected override void InitEssentialInfo_something()
    {
        _id_something = "0120";
    }

    // 개별 능력치 초기화 
    public override void InitSomething_custom(Vector3 targetPos)
    {
        pos = targetPos;
        offset = Vector3.zero;
 
        speed = 0f;
        lifeTime = 15f;

        //--------------
        inCircle  = false;

        lastAttackTime = -3f;
        lastHealTime = -3f;

        asPlus = Player.player.sanctuaryLevel * 50;
    }

    // 개별 초기화 
    public override void ActionSomething_custom()
    {
        // rb.velocity = dir * speed;
    }


    //===========================
    // 플레이어가 영역 안으로 들어오면
    //==========================
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inCircle = true;
            
            // Player.player.Attack_Speed_Plus += asPlus;

            Player.playerStatus.Increase_attackSpeedPlus(asPlus);
            // 공속 증가?
        }
    }

    //===========================
    // 플레이어가 영역 밖으로 나가면
    //==========================
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inCircle = false;

            // Player.player.Attack_Speed_Plus -= asPlus;
            Player.playerStatus.Decrease_attackSpeedPlus(asPlus);
        }
    }


    //===============
    // LightBeam
    //===============
    public void LightBeam()
    {
        if (currTime >= lastHealTime + 2f)
        {
            // 힐 이펙트 생성
            // Effect effect = EffectPoolManager.epm.GetFromPool("101_b");
            // effect.InitEffect(myTransform.position);
            // effect.ActionEffect();
            SomethingPoolManager.instance.CreateSomething("0121",myTransform.position);

            //
            //힐하고 공격 
            Attack();
            Player.player.ChangeHp(3 * Player.player.sanctuaryLevel );

            //
            lastHealTime = currTime;
        }
    }

    //===============
    // attack - 성역안 적 공격 
    //===============
    public void Attack()
    {
        if (currTime >= lastAttackTime + 1f)
        {
            // 성역 공격 투사체 생성
            Projectile proj = ProjPoolManager.instance.GetFromPool("101");
            proj.SetUp(  3 * Player.player.sanctuaryLevel  ,0, 1, 1, 987654321, 0, 0.7f);
            proj.SetSpecialStat( Player.player.explosionLevel, 1.5f, 15);

            proj.myTransform.position = myTransform.position;
            proj.Action();
            //
            lastAttackTime = currTime;
        }
    }

    //===============
    // 성역 안에 있을 때 일정 시간마다 빛줄기 
    //===============
    void Update()
    {
        currTime = Time.time;
        if (inCircle)
        {
            // 일정시간마다 빛줄기
            LightBeam();
        }
    }
}
