using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3_Bullet : MonoBehaviour
{
    Rigidbody2D bulletRigid;
    public Transform target;
    public GameObject normal3Prefab;
    public GameObject breathePrefab;
    public float nor3Speed = 10f;

    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        Invoke("Firenormal3", 1f);
        Invoke("Breathe", 3f);
    }

    void Firenormal3()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject normal3Bullet = Instantiate(normal3Prefab, transform.position, Quaternion.identity);
            Rigidbody2D normal3Rigid = normal3Bullet.GetComponent<Rigidbody2D>();
            float angle = i * 5f - 5f;
            Vector2 direction = Quaternion.Euler(0f, 0f, angle) * (target.position - transform.position);
            normal3Rigid.velocity = direction.normalized * nor3Speed;
        }
        Invoke("Firenormal3", 1f);
    }

    void Breathe()
    {
        GameObject breatheBoss = Instantiate(breathePrefab, transform.position, Quaternion.identity);
        Invoke("Breathe", 10f);
    }

    void Update()
    {
        
    }
}
