using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossAnimationController : MonoBehaviour
{
    public Animator animator_boss;
    public Animator animator_weapon;
    
    // Start is called before the first frame update
    void Start()
    {
        animator_boss = GetComponent<Animator>();
        animator_weapon = transform.Find("Weapon").GetComponent<Animator>();

        animator_weapon.gameObject.SetActive(false);
    }

    public void SetAnimation_weapon_apearance()
    {
        animator_weapon.gameObject.SetActive(true);
    }
}
