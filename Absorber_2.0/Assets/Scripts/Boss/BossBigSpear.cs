using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBigSpear : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("boss", 0.01f);
    }

    void boss()
    {
        animator.SetTrigger("boss");
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int dmg = 1;
            //dmg = GameObject.Find("Boss1(Clone)").GetComponent<Boss>().Atk;
            // Player.Instance.OnDamage(dmg);

        }
    }
}