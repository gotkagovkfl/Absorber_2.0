using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public Transform myTransform;
    public Transform target;
    public Enemy enemy_d;
    public string id_effect;

    public Rigidbody2D rb;   


    public bool readyDestroy;

    public Vector3 pos;     // 생성위치
    public Vector3 offset;  // 생성위치에서의 오프셋
    public Vector3 dir;     // 이동방향
    public float speed = 1;        // 이동속도
    public float lifeTime = 1f;    // 수명

    public Vector3 originalScale;

    protected AudioSource audioSource;

    //=================================================================================
    public abstract void InitEssentialEffectInfo();


    public void InitEffect(Vector3 targetPos)
    {
        myTransform = transform;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.loop =false;
        }


        InitEffect_custom(targetPos);
    }

    public abstract void InitEffect_custom(Vector3 targetPos);       // 기본 변수 초기화


    public void SetTarget(Transform t)
    {
        target = t;
    }

    public void SetDependency(Enemy e)
    {
        enemy_d = e; 
    }
    
    //
    public void ActionEffect()
    {
        myTransform.position = pos + offset;          // 설정된 값으로 위치 변경 
        StartCoroutine(EffectDestroy());            // Init하면서 설정된 수명이 지난후 파괴 
        ActionEffect_custom();
    }
    
    public abstract void ActionEffect_custom();                 // 각 이펙트의 행동 개시 

    // lifetime 초 후에 풀로 반납 
    public IEnumerator EffectDestroy()
    {
        yield return StartCoroutine( ReadyDestroy() );
        
        EffectPoolManager.epm.TakeToPool(GetComponent<Effect>());
        
        readyDestroy = false;

        // StopCoroutine(EffectDestroy());
    }


    public IEnumerator ReadyDestroy()
    {
        if (lifeTime != -1)
        {
            yield return new WaitForSeconds(lifeTime);
            readyDestroy = true;
        }     
        yield return new WaitUntil( ()=> readyDestroy ); 
    }
    

    void Awake()
    {
        originalScale = transform.localScale;
    }

}
