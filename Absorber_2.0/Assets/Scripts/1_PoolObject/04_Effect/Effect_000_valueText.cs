using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;            //텍스트매쉬프로 

public class Effect_000_valueText : Effect
{
    TextMeshPro tmp;

    public int typeNum {get;set;}
    
    protected override void InitEssentialInfo_effect()
    {
        id_effect = "000";
    }

    // 개별 능력치 초기화 
    public override void InitEffect_custom(Vector3 targetPos)
    {
        tmp = GetComponent<TextMeshPro>();
        myTransform.localScale = originalScale;

        Color newColor = tmp.color;
        newColor.a =1f;
        tmp.color  = newColor;
        
        pos = targetPos;
        dir =  new Vector3(Random.Range(-0.2f,0.2f), 0.5f, 0).normalized;
        
        float newX = Random.Range(-0.5f,0.5f);
        float newY = Random.Range(0.5f, 1.5f);
        offset = new Vector3(newX, newY, 0);
 
        speed = 1f;
        lifeTime = 0.5f;
    }

    // 개별 초기화 
    public override void ActionEffect_custom()
    {
        switch (typeNum)
        {
            case 0:
                StartCoroutine( textAnimation_normal());
                break;
            case 1:
                StartCoroutine( textAnimation_dot() );
                break;
            case 2:
                StartCoroutine( textAnimation_highlight() );
                break;
            case 3:
                StartCoroutine( textAnimation_enhanced() );
                break;
        }
        
        // StartCoroutine(textAnimation_enhanced());
    }

    //====================================================================================
    
    //======================
    // 일반공격
    //======================
    public IEnumerator textAnimation_normal()
    {
        rb.velocity = dir * speed;  

        yield return new WaitForSeconds( 0.3f );
        for(int i=0;i<4;i++)
        {
            myTransform.localScale *= 0.9f;
            Color newColor = tmp.color;
            newColor.a -= 0.2f;
            tmp.color  = newColor;
            yield return new WaitForSeconds( 0.05f );
        }        
    }
    //======================
    // 도트데미지 
    //======================
    public IEnumerator textAnimation_dot()
    {
        myTransform.localScale *= 0.9f;
        yield return new WaitForSeconds( 0.3f );
        for(int i=0;i<4;i++)
        {
            myTransform.localScale *= 0.8f;
            Color newColor = tmp.color;
            newColor.a -= 0.2f;
            tmp.color  = newColor;
            yield return new WaitForSeconds( 0.05f );
        }        
    }

    //======================
    // 하이라이트 
    //======================
    public IEnumerator textAnimation_highlight()
    {
        for(int i=0;i<6;i++)
        {
            rb.MovePosition(rb.position + Vector2.up * 0.2f);
            myTransform.localScale *= 1.02f;
            yield return null;
        }  
        yield return new WaitForSeconds(0.2f);

        for(int i=0;i<4;i++)
        {
            Color newColor = tmp.color;
            newColor.a -= 0.1f;
            tmp.color  = newColor;
            yield return new WaitForSeconds( 0.04f );
        }       
    }

    //======================
    // 강화공격
    //======================
    public IEnumerator textAnimation_enhanced()
    {  
        for(int i=0;i<6;i++)
        {
            rb.MovePosition(rb.position + Vector2.up * 0.1f);
            myTransform.localScale *= 1.02f;
            yield return null;
        }  
        yield return new WaitForSeconds(0.2f);

        for(int i=0;i<4;i++)
        {
            Color newColor = tmp.color;
            newColor.a -= 0.1f;
            tmp.color  = newColor;
            yield return new WaitForSeconds( 0.04f );
        }              
    }

}
