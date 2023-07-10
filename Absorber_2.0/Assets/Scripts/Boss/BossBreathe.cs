using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBreathe : Projectile_Enemy 
{
    protected override void InitEssentialInfo_enemyProj()
    {
        id_proj =  "105";
    }

    
    public override void Action()
    {

    }
    
    
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("Breathe", 5f);
    }

    void Breathe()
    {
        animator.SetTrigger("Breathe");
        Destroy(gameObject, 10f);
    }

    public override void EnemyProjDestroy_custom()
    {
        
    }
}
