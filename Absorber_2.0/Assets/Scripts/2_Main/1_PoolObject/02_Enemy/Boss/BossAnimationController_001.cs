using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationController_001 : MonoBehaviour
{
    public Animator animator_boss;
    public Animator animator_weapon;

    public Transform transform_weapon;
    public Transform transform_eye;
    

    //============================================================================================
    // Start is called before the first frame update
    void Start()
    {
        animator_boss = GetComponent<Animator>();
        animator_weapon = transform.Find("Weapon").GetComponent<Animator>();

        transform_weapon = animator_weapon.transform.Find("Center");
        
        animator_weapon.gameObject.SetActive(false);

        transform_eye = transform.Find("Eye");
    }

    //======================================================
    public void  DirectingEnd()
    {
        DirectingManager.dm.DirectingEnd();

        GetComponent<Boss_001>().StartRoutine();
    }





    public void SetAnimation_weapon_apearance()
    {
        animator_weapon.gameObject.SetActive(true);
    }

    //================================================================

    public void OnEnter2Phase()
    {
        Effect effect =EffectPoolManager.instance.GetFromPool("211");    // 안광 
        effect.InitEffect( transform_eye.position );
        effect.ActionEffect();                                                

    }


    public void onDeath()
    {
        animator_boss.SetTrigger("die");

        animator_weapon.SetTrigger("die");
    }

    public void ShowEffect_bat()
    {
        Effect effect = EffectPoolManager.instance.GetFromPool("200");
        effect.InitEffect( transform.position );
        effect.ActionEffect();
    }

    //=================================== eye ====================================================

    public void ShowEffect_eye_useSkill()
    {
        Effect effect = EffectPoolManager.instance.GetFromPool("210");
        effect.InitEffect( transform_eye.position );
        effect.SetTarget(transform_eye);
        effect.ActionEffect();
    }

    //================================ use skill ========================================================
    public void ShowEffect_skill_normal()
    {
        ShowEffect_eye_useSkill();

        Effect effect = EffectPoolManager.instance.GetFromPool("201");
        effect.InitEffect(transform_weapon.position);
        effect.ActionEffect();
    }

    public void ShowEffect_skill_split()
    {
        ShowEffect_eye_useSkill();

        Effect effect = EffectPoolManager.instance.GetFromPool("202");
        effect.InitEffect(transform_weapon.position);
        effect.ActionEffect();
    }

    public void ShowEffect_skill_bomb()
    {
        ShowEffect_eye_useSkill();

        Effect effect = EffectPoolManager.instance.GetFromPool("203");
        effect.InitEffect(transform_weapon.position);
        effect.ActionEffect();
    }

    public void ShowEffect_skill_around()
    {
        ShowEffect_eye_useSkill();

        animator_weapon.SetTrigger("enter2phase");      // 무기 효과 
    }
}
