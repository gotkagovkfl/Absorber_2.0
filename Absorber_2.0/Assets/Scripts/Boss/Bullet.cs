using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Player.Instance.OnDamage(dmg);
            // Player.Instance.OnDamage(damage, hitPoint, strongAttack);   

            Destroy(gameObject);
        }
    }

    public virtual void Des()
    {
        Destroy(gameObject);
    }

}
