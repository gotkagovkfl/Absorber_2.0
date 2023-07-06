using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : Projectile_Enemy
{

    public override void InitEssentialProjInfo()
    {
        id_proj =  "000";
    }
    public void SetUp()
    {
        damage = 5;
    }
    public override void Action()
    {
        base.rb.velocity = transform.up * base.speed;
    }


    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
     //       int dmg = damage;

      //      if (dmg != 0)
      //      {
       //         Player.Instance.OnDamage(dmg);
       //     }

            //rigid.isKinematic = true;
       // }
   // }

    public override void EnemyProjDestroy_custom()
    {
        
    }
}
