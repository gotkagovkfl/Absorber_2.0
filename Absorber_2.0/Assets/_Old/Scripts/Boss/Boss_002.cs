using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_002 : Enemy
{
    //BossAnimationController_002 bac;
    Boss2_Bullet bullet;

    bool bulletCheck = false;

    int baseHp;
    int baseAtk;
    int baseDef;

    bool bossDied;

    public Vector2[] positions;

    protected override void InitEssentialInfo_enemy()
    {
        id_enemy = "b_002";
    }

    public override void InitEnemyStatusCustom()
    {
        //bac = GetComponent<BossAnimationController_002>();
        bullet = GetComponent<Boss2_Bullet>();

        baseHp = 3000;
        baseAtk = 20;
        baseDef = 5;
        //
        isBoss = true;          //**************************

        hpFull = baseHp + Random.Range(-600, 600);
        damage = baseAtk + Random.Range(-5, 5);
        def = baseDef + Random.Range(-1, 1);
        //speed = 2;

        attackSpeed = 0f;

        itemProb = 100;
        manaValue = 3;

        strongAttack = true;
        canKnockBack = false;

        bossDied = false;

        positions = new Vector2[]
        {
            new Vector2 (10,5),
            new Vector2 (10,-5),
            new Vector2 (-10,5),
            new Vector2 (-10,-5)
        };
    }

    protected override void AttackCustom()
    {

    }

    public void StartRoutine()
    {
        // active = true;
        canMove = true;
        ready = true;
        //bullet.StartSkillRoutine();
    }


    public override void MoveCustom()
    {
        canMove = false;
        int ranIndex = Random.Range(0, 4);
        Vector2 ranPosition = positions[ranIndex];
        transform.position = ranPosition;
        Invoke("BoolMoveCustom", 5f);
    }

    public void BoolMoveCustom()
    {
        canMove = true;
    }

    public override void DieCustom()  // *****************************************************
    {
        StartCoroutine(Die());
        // obj.SetActive(false);
        // GetComponent<Collider2D>().enabled = true;
        // base.hp = 10;                // *****************************************************
        // base.DropItem();
    }

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
        deltaScale = 0;

        rb.simulated = false;
        StopCoroutine(MoveAnimation());
        yield return new WaitForSeconds(3.5f);

        //bac.OnEnter2Phase();        // ����

        yield return new WaitForSeconds(2f);

        canMove = true;
        ready = true;
        deltaScale = 0.001f;

        rb.simulated = true;
        StartCoroutine(MoveAnimation());
    }

    IEnumerator Die()   // ���� ü�� 0 ���Ϸ� ������ �� 3�� �� ���� ������Ʈ �ı�
    {
        speed = 0f;

        StartCoroutine(bullet.stopInvoke2());

        //bac.onDeath();

        yield return new WaitForSeconds(5f);

        deathAnimationEnd = true;

        StageManager.sm.FinishStage(true);
    }

    void Update()
    {
        if (hp < (hpFull * 0.8) && !bulletCheck)       // ���� �� ���� ����. ���� �� �ൿ �ߴ� �� ���� ����.
        {
            Enter2Phase();
        }
    }
}
