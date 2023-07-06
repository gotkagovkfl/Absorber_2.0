using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjEnemy_001_explosion : Projectile_Enemy
{
    public override void InitEssentialProjInfo()
    {
        id_proj = "001";
    }
    
    
    public override void Action()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }

    public override void EnemyProjDestroy_custom()
    {
        
    }
}
