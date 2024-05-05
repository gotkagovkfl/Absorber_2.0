using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour , IPoolObject
{
    public Transform myTransform;
    public Transform targetToFollow;
    public Enemy    enemy_d;
    public string id_effect;

    public Rigidbody2D rb;   

    public bool _isDead;

    public Vector3 pos;     // 생성위치
    public Vector3 offset = Vector3.zero;  // 생성위치에서의 오프셋
    public Vector3 dir;     // 이동방향
    public float speed = 1;        // 이동속도
    public float lifeTime = 1f;    // 수명

    public Vector3 originalScale;

    protected AudioSource audioSource;

    //=================================================================================
    
    
    //===========================
    // IPoolObject : resourceManager에 로드시에 id 초기화 
    //===========================
    public void InitEssentialInfo()
    {
        InitEssentialInfo_effect();
    }
    //
    protected abstract void InitEssentialInfo_effect();
    //================
    // GetID
    //==============
    public string GetId()
    {
        return id_effect;
    }
    //===========================
    // IPoolObject : 처음 생성될때 
    //===========================
    public void OnCreatedInPool()
    {
        myTransform = transform;
        originalScale = myTransform.localScale;

        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    //===========================
    // IPoolObject : 다시 사용될때, 
    //===========================
    public void OnGettingFromPool()
    {

    }




    //==============================================================
    public void InitEffect(Vector3 targetPos)
    {
        
        
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
        targetToFollow = t;
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
        // 설정된 수명이 끝나면 파괴 
        if (lifeTime != -1)
        {
            yield return new WaitForSeconds(lifeTime);
            _isDead = true;
        }     
        yield return new WaitUntil( ()=> _isDead ); 


        EffectPoolManager.instance.TakeToPool(this);
        
        _isDead = false;

        // StopCoroutine(EffectDestroy());
    }


    public IEnumerator ReadyDestroy()
    {
        if (lifeTime != -1)
        {
            yield return new WaitForSeconds(lifeTime);
            _isDead = true;
        }     
        yield return new WaitUntil( ()=> _isDead ); 
    }
    




    //==============================================================
}
