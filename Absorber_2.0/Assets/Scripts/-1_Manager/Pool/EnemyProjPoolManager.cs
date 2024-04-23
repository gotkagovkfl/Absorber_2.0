using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjPoolManager : PoolManager<Projectile_Enemy>
{
    public static EnemyProjPoolManager eppm;  // ���ʹ̰� ����ϴ� ����ü Ǯ �Ŵ��� ��ü

    protected override void Awake()
    {
        base.Awake();
        eppm = this;
    }
    
    void Start()
    {
        // 스테이지 종료 이벤트 발생시 모든 적 투사체 제거
        GameEvent.onStageClear.AddListener( CleanEveryObjects_enemyProj);
    }

    protected override void SetCategory()
    {
        id_category = "03";
    }
    // public override void SetDir()
    // {
    //     dir = "Prefabs/Enemies";
    // }
    public override void GetFromPool_custom(Projectile_Enemy obj)
    {
        obj.active = true;
        obj.isAlive = true;
    }

    // public override string GetId(Projectile_Enemy obj)
    // {
    //     return obj.id_proj;
    // }


    public override void TakeToPool_custom(Projectile_Enemy obj)
    {
        if (!obj.isAlive)
        {
            return;
        }
        //
        obj.active = false;
        obj.isAlive = false;
        //obj.CancelInvoke("ProjDestroy");
        // proj.myTransform.position = transform.position;     // �Ѿ� ������
    }

        //=======================================
    // 스테이지 종료시 남아있는 모든 적 제거 - 아이템을 생성하지않고 점수를 증가시키지 않음. 
    //=======================================
    public void CleanEveryObjects_enemyProj()
    {
        Projectile_Enemy[] projs = GetComponentsInChildren<Projectile_Enemy>();

        foreach(var proj in projs)
        {
            StartCoroutine( proj.DestroyProj( 0f ) );
        }
    }
}
