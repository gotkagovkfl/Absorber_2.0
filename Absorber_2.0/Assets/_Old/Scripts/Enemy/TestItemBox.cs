using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemBox : MonoBehaviour
{
    public float hp = 10;

    public GameObject[] prefabs_item = new GameObject[3];

    // 데미지입기 
    public void OnDamage(float dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Die();    
        }
    }

    // 사망  - 아이템 뿌림
    public void Die()
    {
       GameObject exp = Instantiate(prefabs_item[0],transform.position - Vector3.left*0.5f, transform.rotation);
       GameObject dropItem = null;
       
       int num = Random.Range(1,100);
       int itemNum = 0;
       if (num <= 70)
       {   
            itemNum = 1;
       }
       else
       {
            itemNum = 2;
       }
       
       dropItem = Instantiate(prefabs_item[itemNum],transform.position,transform.rotation);

       Destroy(gameObject);
    }

}
