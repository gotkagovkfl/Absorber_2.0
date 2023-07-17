using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//===================================
// Something  - 풀링오브젝트 : 구 이펙트와 오브젝트를 합침
//====================================
public abstract class Something : MonoBehaviour, IPoolObject
{  
    //
    protected string _id_something;
    
    public Vector3 originalScale;
    //    
    public Transform myTransform;
    public Rigidbody2D rb;  
    // 
    public Transform targetToFollow;
    public Enemy enemy_d;
    //
    
    protected bool _isDead;

    protected Vector3 pos;     // 생성위치
    protected Vector3 offset;  // 생성위치에서의 오프셋
    protected Vector3 dir;     // 이동방향

    protected float speed = 1;        // 이동속도
    protected float lifeTime = 1f;    // 수명

    

    // protected AudioSource audioSource;

    //=================================================================================
    
    
    //===========================
    // IPoolObject : resourceManager에 로드시에 id 초기화 
    //===========================
    public void InitEssentialInfo()
    {
        InitEssentialInfo_something();
    }
    //
    protected abstract void InitEssentialInfo_something();
    //================
    // GetID
    //==============
    public string GetId()
    {
        return _id_something;
    }
    //===========================
    // IPoolObject : 처음 생성될때 
    //===========================
    public void OnCreatedInPool()
    {
        myTransform = transform;
        originalScale = myTransform.localScale;

        rb = GetComponent<Rigidbody2D>();
        // audioSource = GetComponent<AudioSource>();
        // if (audioSource != null)
        // {
        //     audioSource.playOnAwake = false;
        //     audioSource.loop =false;
        // }

    }

    //===========================
    // IPoolObject : 다시 사용될때, 
    //===========================
    public void OnGettingFromPool()
    {

    }


    //==============================================================
    public void InitSomething(Vector3 targetPos)
    {
        InitSomething_custom(targetPos);
    }

    public abstract void InitSomething_custom(Vector3 targetPos);       // 기본 변수 초기화


    public void SetTargetToFollow(Transform t)
    {
        targetToFollow = t;
    }

    public void SetDependency(Enemy e)
    {
        enemy_d = e; 
    }
    
    //
    public void ActionSomething()
    {
        myTransform.position = pos + offset;          // 설정된 값으로 위치 변경 
        StartCoroutine(DestroySomething());            // Init하면서 설정된 수명이 지난후 파괴 
        ActionSomething_custom();
    }
    
    public abstract void ActionSomething_custom();                 // 각 이펙트의 행동 개시 

    //===================================
    // lifetime 초 후에 풀로 반납 
    //==================================
    public IEnumerator DestroySomething()
    {
        // 설정된 수명이 끝나면 파괴 
        if (lifeTime != -1)
        {
            yield return new WaitForSeconds(lifeTime);
            _isDead = true;
        }     
        yield return new WaitUntil( ()=> _isDead ); 

        //
        EffectPoolManager.epm.TakeToPool(GetComponent<Effect>());
        
        _isDead = false;
    }
}
