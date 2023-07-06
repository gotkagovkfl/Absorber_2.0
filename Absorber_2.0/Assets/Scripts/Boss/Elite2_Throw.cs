using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elite2_Throw : Projectile_Enemy
{ 
    // Start is called before the first frame update
    public override void Action()
    {
        base.rb.velocity = transform.up * base.speed;
    }

    public override void EnemyProjDestroy_custom()
    {
        
    }

    public override void InitEssentialProjInfo()
    {
        id_proj = "011";
    }

    //void OnTriggerEnter2D(Collider2D other)               // �÷��̾�� �浹 �� ������ ���� �ı�
    //{
     //   if (other.gameObject.CompareTag("Player"))
     //   {
      //      int dmg = 1;
            //dmg = GameObject.Find("Boss1(Clone)").GetComponent<Boss>().Atk;
       //     Player.Instance.OnDamage(dmg);

       //     Destroy(gameObject);
       // }
  //  }
}
