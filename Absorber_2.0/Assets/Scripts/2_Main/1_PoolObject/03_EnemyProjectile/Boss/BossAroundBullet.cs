using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAroundBullet : Projectile_Enemy
{
    public Transform bulletTransform;
    public float rotationSpeed = 50f;



    protected override void InitEssentialInfo_enemyProj()
    {
        id_proj =  "104";
    }

    public override void Action()
    {
        
    }

    void Start()
    {
        bulletTransform = transform;
    }
    
    void FixedUpdate()
    {
        bulletTransform.RotateAround(Vector3.zero, Vector3.back, rotationSpeed * Time.deltaTime);
    }

    public override void OnHit()
    {
        if(caster)
        {
            Enemy boss = caster.GetComponent<Enemy>();
            boss.Healing(boss.damage*3);
        }
    }


    public override void EnemyProjDestroy_custom()
    {
        
    }
}
