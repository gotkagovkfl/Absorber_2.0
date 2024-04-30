using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Dash : PlayerSkill
{
    private float dashingPower = 4f;

    public PlayerSkill_Dash()
    {
        id = "000";
        skillName = "대시";
        coolTime = 3f;
        duration = 0.2f;
    }


    protected override bool IsAvailable_custom()
    {
        return Player.player.canMove && Player.player.rb.velocity != Vector2.zero;
    }


    protected override void UseSkill_custom()
    {
        // 플레이어 무적처리 
        Player.player.GetInivincible(duration);
        
        // 충돌무시 
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true); 

        // 일정거리 대시
        Rigidbody2D rb = Player.player.rb;
        // rb.gravityScale = 0f;
        rb.velocity = rb.velocity * dashingPower;

        //
        Player.player.canMove = false;

        GameEvent.ge.onDash.Invoke();
    }



    protected override void ShowEffect()
    {
        for (int i=0; i< 5; i++)
        {
            // Effect effect = EffectPoolManager.instance.GetFromPool("012");
            // effect.InitEffect(t_player.position);
            // effect.ActionEffect();
            // effect.GetComponent<SpriteRenderer>().flipX = spriter.flipX;

            //             yield return null;
            // yield return null;
            // yield return null;
        }
    }

    protected override void OnFinishDuration_custom()
    {
        // 충돌무시 off
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);


        Player.player.canMove = true;
    }


}
