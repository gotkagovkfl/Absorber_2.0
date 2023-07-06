using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elite4_Attack : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("spears", 0.01f);
        
    }


    void spears()
    {
        animator.SetTrigger("forElite4");
        Destroy(gameObject, 5f);
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (transform.parent != null)
    //     {
    //         if (other.gameObject.CompareTag("Player"))
    //         {
    //             int dmg = 2;
    //             //dmg = GameObject.Find("Boss1(Clone)").GetComponent<Boss>().Atk;
    //             Player.Instance.OnDamage(dmg);

    //         }
    //     }
    // }
}
