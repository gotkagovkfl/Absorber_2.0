using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using DG.Tweening;
using System.Xml.Linq;

//=======================================
// 데이지, 상태이상 등 텍스트 오브젝트 - 나중에는 그냥 종륨별로 스크립트를 분리하자
//======================================
public class Effect_7000_Text : Effect
{
    TextMeshPro text;

    public int typeNum {get;set;}
    
    protected override void InitEssentialInfo_effect()
    {
        id_effect = "7000";
    }

    public override void InitEffect_custom(Vector3 targetPos)
    {
        text = GetComponent<TextMeshPro>();
        
        
        // Color newColor = text.color; //어차피 색은 다시 지정 될 거임. 
        // newColor.a =1f;
        // text.color  = newColor;
        
        dir =  new Vector3(Random.Range(-0.2f,0.2f), 0.5f, 0).normalized;
        
        float newX = Random.Range(-0.5f,0.5f);
        float newY = Random.Range(0.5f, 1.5f);
        offset = new Vector3(newX, newY, 0);
 
        speed = 1f;
        lifeTime = 0.5f;
    }

    public override void ActionEffect_custom()
    {
        switch (typeNum)
        {
            case 0:
                PlayAnim_normal();
                break;
            case 1:
                PlayAnim_dot();
                break;
            case 2:
                PlayAnim_highlight();
                break;
            case 3:
                PlayAnim_enhanced();
                break;
        }
    }




    //====================================================================================
    public void SetText(int type, string value, Color color)
    {
        typeNum = type;
        text.text= value;
        text.color = color;
        
        // 텍스트 그라데이션 설정. 
        VertexGradient textGradient = text.colorGradient;
        textGradient.bottomLeft = Color.white;
        textGradient.bottomRight = Color.white;
        
        switch(typeNum)
        {
            case 0:             // 일반 텍스트(일반공격 포함)
            case 1:             // 도트 공격 
            case 2:  
                textGradient.bottomLeft = color;
                textGradient.bottomRight = color;
                break;      
            case 3:
                break;
        }
        textGradient.topLeft = color;
        textGradient.topRight= color;    
        text.colorGradient = textGradient;

    }



    //========================


    //======================
    // 일반공격
    //======================
    void PlayAnim_normal()
    {
        rb.velocity = dir * speed;  

        Sequence seq = DOTween.Sequence()
        .AppendInterval(0.3f)
        .Append(t_effect.DOScale( 0.6f, 0.2f))
        .Join(text.DOFade(0.2f,0.2f) )
        .Play();
    }

    void PlayAnim_dot()
    {
        t_effect.localScale *= 0.9f;

        Sequence seq = DOTween.Sequence()
        .AppendInterval(0.3f)
        .Append(t_effect.DOScale( 0.2f, 0.2f))
        .Join(text.DOFade(0.2f,0.2f) )
        .Play();
    }

    void PlayAnim_highlight()
    {
        Sequence ses = DOTween.Sequence()
        .Append(t_effect.DOMove( t_effect.position+ Vector3.up ,1f))
        .Join(t_effect.DOScale(1.1f,1f))
        .AppendInterval(0.2f)
        .Append(text.DOFade( 0.6f, 0.2f))
        .Play();
    }

    void PlayAnim_enhanced()
    {
        Sequence ses = DOTween.Sequence()
        .Append(t_effect.DOMove( t_effect.position+ Vector3.up ,1f))
        .Join(t_effect.DOScale(1.1f,1f))
        .AppendInterval(0.2f)
        .Append(text.DOFade( 0.6f, 0.2f))
        .Play();  
    }


    // public IEnumerator textAnimation_normal()
    // {
    //     rb.velocity = dir * speed;  

    //     yield return new WaitForSeconds( 0.3f );

    //     for(int i=0;i<4;i++)
    //     {
    //         t_effect.localScale *= 0.9f;
    //         Color newColor = text.color;
    //         newColor.a -= 0.2f;
    //         text.color  = newColor;
    //         yield return new WaitForSeconds( 0.05f );
    //     }        
    // }
    //======================
    // 도트데미지 
    //======================
    // public IEnumerator textAnimation_dot()
    // {
    //     t_effect.localScale *= 0.9f;
    //     yield return new WaitForSeconds( 0.3f );
    //     for(int i=0;i<4;i++)
    //     {
    //         t_effect.localScale *= 0.8f;
    //         Color newColor = text.color;
    //         newColor.a -= 0.2f;
    //         text.color  = newColor;
    //         yield return new WaitForSeconds( 0.05f );
    //     }        
    // }

    //======================
    // 하이라이트 
    //======================
    // public IEnumerator textAnimation_highlight()
    // {
    //     for(int i=0;i<6;i++)
    //     {
    //         rb.MovePosition(rb.position + Vector2.up * 0.2f);
    //         t_effect.localScale *= 1.02f;
    //         yield return null;
    //     }  
    //     yield return new WaitForSeconds(0.2f);

    //     for(int i=0;i<4;i++)
    //     {
    //         Color newColor = text.color;
    //         newColor.a -= 0.1f;
    //         text.color  = newColor;
    //         yield return new WaitForSeconds( 0.04f );
    //     }       
    // }

    //======================
    // 강화공격
    //======================
    // public IEnumerator textAnimation_enhanced()
    // {  
    //     for(int i=0;i<6;i++)
    //     {
    //         rb.MovePosition(rb.position + Vector2.up * 0.1f);
    //         t_effect.localScale *= 1.02f;
    //         yield return null;
    //     }  
    //     yield return new WaitForSeconds(0.2f);

    //     for(int i=0;i<4;i++)
    //     {
    //         Color newColor = text.color;
    //         newColor.a -= 0.1f;
    //         text.color  = newColor;
    //         yield return new WaitForSeconds( 0.04f );
    //     }              
    // }


}