using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elite1_Bullet : MonoBehaviour
{ 

    public float baseBunbSpeed;
    public float baseBunbTime;
    public int baseBunbNum;

    public float baseThrowSpeed;
    public float baseThrowTime;

    public int num;
    public Transform target;

    void Start()
    {
        target = Player.player.t_player;

        baseBunbSpeed = 2.0f;
        baseBunbTime = 5.0f;
        baseBunbNum = 2;

        baseThrowSpeed = 15.0f;
        baseThrowTime = 2.0f;

        num = Random.Range(0, 2);
        if (num == 0)
        {
            Invoke("Firebunb", 1f);
        }else
        {
            Invoke("FireThrow", 1f);
        }

        


    }

    void Firebunb()
    {
        if (GetComponent<Enemy>().hp_curr <=0 )
        {
            return;
        }
        
        //
        Effect effect = EffectPoolManager.instance.GetFromPool("666");
        effect.InitEffect(transform.position);
        effect.ActionEffect();
        //
        for (int i = 0; i < baseBunbNum; i++)
        {

            Projectile_Enemy bunbBullet = EnemyProjPoolManager.instance.GetFromPool("101");
            bunbBullet.SetUp(7, baseBunbSpeed, 1, 0, 0, 10f);
            bunbBullet.transform.position = transform.position;

            Vector3 direction;
            if (baseBunbNum == 1)
            {
                direction = target.position - transform.position;

            }
            else
            {
                direction = Quaternion.AngleAxis(360f / baseBunbNum * i, Vector3.forward) * transform.right;
            }

            bunbBullet.SetDirection(direction);
            bunbBullet.Action();

        }
        Invoke("Firebunb", baseBunbTime);
    }// Start is called before the first frame update

    void FireThrow()
    {
        if (GetComponent<Enemy>().hp_curr <=0 )
        {
            return;
        }
        
        //
        Effect effect = EffectPoolManager.instance.GetFromPool("666");
        effect.InitEffect(transform.position);
        effect.ActionEffect();
        
        //
        int ranNum = Random.Range(0, 2);
        float ranX;
        float ranY;
        float ranSpeed = Random.Range(baseThrowSpeed - 5.0f, baseThrowSpeed + 10.0f);
        float ranTime = Random.Range(baseThrowTime - 0.5f, baseThrowTime + 0.5f);
        if (ranNum == 0)
        {
            ranX = Random.Range(-1f, 1f) < 0f ? -40f : 40f;
            ranY = Random.Range(-20f, 20f);
        }
        else
        {
            ranX = Random.Range(-40f, 40f);
            ranY = Random.Range(-1f, 1f) < 0f ? -20f : 20f;
        }

        Projectile_Enemy throwBullet = EnemyProjPoolManager.instance.GetFromPool("103");
        throwBullet.SetUp(15, 15f, 1, 0, 0, 10f);
        throwBullet.transform.position = new Vector3(ranX, ranY, 0f);
        throwBullet.SetDirection(Player.player.t_player);
        throwBullet.RotateProj();
        throwBullet.Action();

        Invoke("FireThrow", 1.5f);
    }
}
