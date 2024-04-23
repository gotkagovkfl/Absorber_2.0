using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjEnemy_002_magicCircleE : Projectile_Enemy
{
    float tickDelay;
    Animator animator;

    protected override void InitEssentialInfo_enemyProj()
    {
        id_proj = "002";
    }


    public override void Action()
    {
        animator = GetComponent<Animator>();

        //int weight = splitNum + 1;

        animator.speed = 1f;

        float animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;


        tickDelay = animationLength;       // ź�ӿ� ������ ���� 

        int tickNum = (int) (lifeTime / tickDelay);                        //������ �� 

        StartCoroutine(Tick(tickNum));
    }

    // ===================================
    // �����Ÿ������� ���������� 
    // ===================================
    public IEnumerator Tick(int tickNum)
    {
        audioSource.PlayOneShot(audioSource.clip);
        for (int i = 0; i < tickNum; i++)
        {
            rb.simulated = true;
            yield return new WaitForFixedUpdate();

            rb.simulated = false;
            yield return new WaitForSeconds(tickDelay);
        }
    }
    public override void EnemyProjDestroy_custom()
    {

    }


    private void Update()
    {
        if (caster!=null)
        {
            myTransform.position = caster.position;
        }
        
    }
}
