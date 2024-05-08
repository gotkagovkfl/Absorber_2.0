using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_001 : Enemy
{
    BossAnimationController_001 bac;
    Boss_Bullet bullet;

    BossUI bossUI;

    bool bulletCheck = false;
    public float distance;

    int baseHp;
    int baseAtk;
    int baseDef;

    bool bossDied;

    //
    public bool[] hpCheck = new bool[5];

    public AudioClip sound_bossHit;
    
    protected override void InitEssentialInfo_enemy()
    {
        id_enemy = "b_001";
    }
    
    public override void InitEnemyStatusCustom()
    {
        bac = GetComponent<BossAnimationController_001>();
        bullet = GetComponent<Boss_Bullet>();
        bossUI = GetComponent<BossUI>();

        sound_bossHit = Resources.Load<AudioClip>("Sound/18_Boss_hit");
        //

        baseHp = 7500;
        baseAtk = 25;
        baseDef = 5; 
        //
        isBoss = true;          //**************************

        hp_max = baseHp + Random.Range(-500,500);      
        damage = baseAtk + Random.Range(-5,5);
        // def =  baseDef + Random.Range(-1, 1);
        movementSpeed = 3;

        attackSpeed = 0f; 

        itemProb = 0;
        manaValue = 0;      // 어차피 스테이지 1하고 끝나니까

        strongAttack = true;
        canKnockBack = false;

        bossDied = false;


        battleType = BattleType.melee;
    }

    // 맞을때 보스 체력바 설정 
    public override void Damaged_custom()
    {
        bossUI.SetHpBar();

        float ratioHp = hp_curr/hp_max;
        if (ratioHp <= 0.8f && !hpCheck[4])
        {
            hpCheck[4] = true;
            audioSource.PlayOneShot(sound_bossHit);
        }
        else if (ratioHp<= 0.6f && !hpCheck[3])
        {
            hpCheck[3] = true;
            audioSource.PlayOneShot(sound_bossHit);
        }
        else if (ratioHp <= 0.4f && !hpCheck[2])
        {
            hpCheck[2] = true;
            audioSource.PlayOneShot(sound_bossHit);
        }
        else if (ratioHp <= 0.2f && !hpCheck[1])
        {
            hpCheck[1] = true;
            audioSource.PlayOneShot(sound_bossHit);
        }
    }


    protected override void AttackCustom()
    {
        
    }

    public void StartRoutine()
    {
        audioSource.PlayOneShot( Resources.Load<AudioClip>("Sound/13_bosslaugh") );
        
        // active = true;
        ready = true;
        bullet.StartSkillRoutine();
        //
        bossUI.InitHpBar();
    }

    // 죽을 때 효과를 처리해야함. 
    // protected override void DieCustom()  // *****************************************************
    // {
    //     audioSource.PlayOneShot( Resources.Load<AudioClip>("Sound/13_bossdeath") );
        
    //     StartCoroutine(Die());
    //     // obj.SetActive(false);
    //     // GetComponent<Collider2D>().enabled = true;
    //     // base.hp = 10;                // *****************************************************
    //     // base.DropItem();
    // }

    public void Enter2Phase()
    {        
        bulletCheck = true;
        StartCoroutine(bullet.stopInvoke());
        StartCoroutine(BossStop());
    }


    IEnumerator BossStop()
    {
        canMove = false;
        ready = false;
        // deltaScale = 0;
        
        rb.simulated = false;
        StopCoroutine(PlayAnim_move());
        yield return new WaitForSeconds(3.5f);
        
        audioSource.PlayOneShot( Resources.Load<AudioClip>("Sound/18_Boss_half")  );
        bac.OnEnter2Phase();        // 연출

        yield return new WaitForSeconds(2f);

        canMove = true;
        ready = true;
        // deltaScale = 0.001f;
        
        rb.simulated = true;
        StartCoroutine(PlayAnim_move());
        StartCoroutine(bullet.HalfAround());
    }

    IEnumerator Die()   // 보스 체력 0 이하로 내려갈 시 3초 뒤 보스 오브젝트 파괴
    {
        movementSpeed = 0f;
        

        Player.player.GetInvincible(30f);
        StartCoroutine(bullet.stopInvoke2());

        bac.onDeath();

        yield return new WaitForSeconds(5f);
        audioSource.PlayOneShot( Resources.Load<AudioClip>("Sound/13_bats") );

        deathAnimationEnd = true;
        bossUI.ActiveHpBar(false);

        StageManager.sm.FinishStage_clear();
    }

    void Update()
    {
        if( hp_curr < (hp_max * 0.6) && !bulletCheck)       // 반피 때 변신 예정. 변신 시 행동 중단 및 반피 패턴.
        {
            Enter2Phase();
        } 
    }


    public void FocusCameraBack()
    {
        DirectingManager.dm.FocusCamera_Player();
    }

}
