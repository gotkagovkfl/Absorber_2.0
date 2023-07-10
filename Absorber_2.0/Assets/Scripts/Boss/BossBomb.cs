using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBomb : Projectile_Enemy 
{

    
    SpriteRenderer spriter;
    Animator animator;



    protected override void InitEssentialInfo_enemyProj()
    {
        id_proj =  "102";
    }


    public override void Action()
    {
        animator = GetComponent<Animator>(); 
        spriter = GetComponent<SpriteRenderer>();

 
        SetSortingLayerBack();
        
        rb.velocity = direction * speed;
        
        

        Invoke("bomb", 3f);

    }
    



    void bomb()
    {
        //
        audioSource.PlayOneShot(audioSource.clip);
        
        //
        rb.velocity = Vector3.zero;
        damage = 15;
        animator.SetTrigger("bomb");          // bomb �ִϸ��̼� �ߵ� Ʈ����
        // Destroy(gameObject, 2f);
    }

    public override void EnemyProjDestroy_custom()
    {
        animator.SetTrigger("default");
    }


    public void SetSortingLayer_plate()
    {
        spriter.sortingOrder=0;
    }
    public void SetSortingLayerBack()
    {
        spriter.sortingOrder=2;
    }


}
