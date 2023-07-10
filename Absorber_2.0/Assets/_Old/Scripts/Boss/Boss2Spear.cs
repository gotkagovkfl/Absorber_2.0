using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Spear : Projectile_Enemy
{
     
    Rigidbody2D spear;
    public Vector2 bulletPos;
    public bool stopCheck;
    public float stopDistance;
    public float spearSpeed;

    protected override void InitEssentialInfo_enemyProj()
    {
        id_proj = "201";
    }

    void Start()
    {
        target = Player.Instance.transform;
        stopDistance = 0.3f;
        bulletPos = target.position;
        stopCheck = false;
        spearSpeed = 10.0f;
        spear = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!stopCheck && (Vector2.Distance(transform.position, bulletPos) <= stopDistance))
        {
            stopCheck = true;
            StartCoroutine(stop());
        }
    }

    public override void Action()
    {

    }

    IEnumerator stop()
    {
        spear.velocity = Vector2.zero;
        yield return new WaitForSeconds(6f);
        target = Player.Instance.transform;
        bulletPos = target.position;
        Vector2 direction = target.position - transform.position;
        spear.velocity = direction.normalized * spearSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spear.rotation = angle;
        stopCheck = false;
    }

    public override void EnemyProjDestroy_custom()
    {
        //animator.SetTrigger("default");
    }
}
