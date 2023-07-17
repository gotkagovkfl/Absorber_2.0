using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_003_DarkLayer : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3( -Player.Instance.inputVector.x,0);

        rb.velocity = dir * Player.Instance.Speed * 0.6f;
    }
}
