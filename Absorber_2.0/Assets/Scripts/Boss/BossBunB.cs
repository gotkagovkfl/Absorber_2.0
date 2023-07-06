using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBunB : Projectile_Enemy 
{
    Animator animator;
    
    
    public GameObject splitBulletPrefab;
    public float baseSpeed = 2.0f;
    public float baseTime = 2.0f;
    public int baseNum = 10;

    public override void InitEssentialProjInfo()
    {
        id_proj =  "101";
    }


    public override void Action()
    {
        animator = GetComponent<Animator>();
        
        Invoke("SplitBullet", baseTime);

        rb.velocity = direction * speed;     
    }


    // void Start()
    // {
    //     splitBulletPrefab = Resources.Load<GameObject>("Prefabs/Boss/Boss_Bullet");
    //     Invoke("SplitBullet", baseTime);     
    //     // Destroy(gameObject, 10f);        
    // }

    void SplitBullet()
    {
        animator.SetTrigger("split");
    }

    // 분열 : 애니메이터에서 재생
    void Split()
    {
        //
        audioSource.PlayOneShot(audioSource.clip);
        
        //
        int ranNum = Random.Range(baseNum - 5, baseNum - 2);
        float ranTime = Random.Range(baseTime, baseTime + 3.0f);
        for (int i = 0; i < ranNum; i++)
        {
            Projectile_Enemy splitBullet = EnemyProjPoolManager.eppm.GetFromPool("100");
            
            float ranSpeed = Random.Range(baseSpeed +2 , baseSpeed +2 + 3.0f);
            splitBullet.SetUp(6, ranSpeed, 1,0,0, 6f );  
            
            splitBullet.transform.position = transform.position;
            splitBullet.SetDirection(Quaternion.AngleAxis(360f / ranNum * i, Vector3.forward) * transform.right);
            splitBullet.RotateProj();
            splitBullet.Action();
                                         
            // splitRigid.velocity = Quaternion.AngleAxis(360f / ranNum * i, Vector3.forward) * transform.right * ranSpeed;     // 20���� ������ �и��ϰ� �߻�
        }
        Invoke("SplitBullet", ranTime);  
    }

    public override void EnemyProjDestroy_custom()
    {
        
    }
}
