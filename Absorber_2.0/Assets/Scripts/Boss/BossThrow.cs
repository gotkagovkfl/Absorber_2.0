using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrow : Projectile_Enemy 
{
    public override void InitEssentialProjInfo()
    {
        id_proj =  "103";
    }
    
    
    public override void Action()
    {
        strongAttack = true;
        
        rb.velocity = direction * speed;

        audioSource.PlayOneShot(audioSource.clip);
    }

    public override void EnemyProjDestroy_custom()
    {
        
    }
}