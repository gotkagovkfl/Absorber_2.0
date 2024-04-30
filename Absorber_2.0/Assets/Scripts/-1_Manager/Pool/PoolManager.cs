using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;


[Serializable]
internal struct PoolData
{
    [SerializeField]
    public string _name;

    public string Name => _name;

    [SerializeField]
    public Component _component;

    public Component Component => _component;
    [SerializeField]
    [Min(0)]
    public int _count;

    public int Count => _count;

    [SerializeField]
    public Transform _container;

    public Transform Container => _container;

    [SerializeField]
    public bool _nonLazy;

    public bool NonLazy => _nonLazy;
}


/// <summary>
/// Pool manager. You can set options for it in editor and then use in game. <br/>
/// It creates specified pools in Awake method, which then you can find with <b>GetPool</b> methods and call its methods.
/// </summary>
public abstract class PoolManager<T> : MonoBehaviour where T : Component, IPoolObject
{
    // public static PoolManager<T> instance;
    
    [SerializeField]
    private List<PoolData> _pools;
    
    protected readonly List<IPool<Component>> _poolsObjects = new();
    //=======================================================================================
    protected PoolType id_category;

    // pooling objects 사전
    // public Dictionary<string, GameObject> dic_objs  = new Dictionary<string, GameObject>();
    
    //============================================================================================================
    
    // 프리팹 주소 설정 후, 풀링 컨테이너 초기화 
    IEnumerator Start()
    {
        yield return new WaitUntil( ()=> PrefabManager.initialized);
        // instance = this;


        Init_custom();
        
        // SetDir();
        InitPoolData();
        InitPoolData_common();  // 공통 초기화 (원래 있던 코드)
    }

    //================================================================
    // 풀링 오브젝트를 참고할 사전의 키 설정
    protected abstract void Init_custom();


    // 해당 오브젝트의 아이디를 받아옴
    // public abstract string GetId(T obj);

    //==========================================================================
    // 풀데이터 초기화 : 풀링 오브젝트의 타입을 정의하고, 해당 타입에 맞게 오브젝트와 사전, 그리고 풀 데이터를 초기화한다.
    //========================================================================
    public void InitPoolData() 
    {   
        Dictionary<string, GameObject> dic_prefabs = PrefabManager.dic_prefabs[ id_category ];

        foreach(var i in dic_prefabs)
        {
            T obj = dic_prefabs[i.Key].GetComponent<T>();
           
            if (obj != null )
            {
                string id_obj = obj.GetId();     // obj의 id 겟

                // poolData 초기화 
                PoolData pd = new PoolData();
                pd._name = id_obj;
                pd._component = obj;
                pd._count = 0;
                pd._container = transform;      // 해당 풀 매니저에서 관리
                pd._nonLazy = false;

                _pools.Add(pd);
            }
        }
    }


    // 원래있던 코드 - 공통 초기화 =============================================================
    public void InitPoolData_common()
    {
        var namesGroups = _pools.Select(p => p.Name).GroupBy(n => n).Where(g => g.Count() > 1);

        if (namesGroups.Count() > 0)
            throw new Exception($"Pool Manager already contains pool with name \"{namesGroups.First().Select(g => g).First()}\"");

        var poolsType = typeof(List<IPool<Component>>);
        var poolsAddMethod = poolsType.GetMethod("Add");
        var genericPoolType = typeof(Pool<>);

        foreach (var poolData in _pools)
        {
            var poolType = genericPoolType.MakeGenericType(poolData.Component.GetType());
            var createMethod = poolType.GetMethod("Create", BindingFlags.Static | BindingFlags.NonPublic);

            var pool = createMethod.Invoke(null, new object[] { poolData.Component, poolData.Count, poolData.Container });

            if (poolData.NonLazy)
            {
                var nonLazyMethod = poolType.GetMethod("NonLazy");
                nonLazyMethod.Invoke(pool, null);
            }

            poolsAddMethod.Invoke(_poolsObjects, new object[] { pool });
        }
    }

    //====================================================================================================================================

    #region Get pool
    /// <summary>
    /// Find pool by <paramref name="index"/>.
    /// </summary>
    /// <typeparam name="T">Pool's objects type.</typeparam>
    /// <param name="index">Pool index.</param>
    /// <returns>Finded pool.</returns>
    public IPool<T> GetPool(int index) => (IPool<T>)_poolsObjects[index];

    /// <summary>
    /// Find pool by type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Pool's objects type.</typeparam>
    /// <returns>Finded pool.</returns>
    public IPool<T> GetPool() => (IPool<T>)_poolsObjects.Find(p => p.Source is T);

    /// <summary>
    /// Find pool by <paramref name="name"/>
    /// </summary>
    /// <typeparam name="T">Pool's objects type.</typeparam>
    /// <param name="name">Pool name.</param>
    /// <returns>Finded pool.</returns>
    public IPool<T> GetPool(string name) => (IPool<T>)_poolsObjects[_pools.FindIndex(p => p.Name == name)];
    #endregion

    #region Get from pool
    /// <summary>
    /// Find pool by <paramref name="index"/> and gets object from it.
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="GetPool{T}"/></typeparam>
    /// <param name="index"><inheritdoc cref="GetPool{T}"/></param>
    /// <returns>Pool's object (or <see langword="null"/> if free object was not finded).</returns>
    public T GetFromPool(int index) => GetPool(index).Get();

    /// <summary>
    /// Find pool by type <typeparamref name="T"/> and gets object from it.
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="GetPool{T}"/></typeparam>
    /// <returns>Pool's object (or <see langword="null"/> if free object was not finded).</returns>
    public T GetFromPool() => GetPool().Get();

    /// <summary>
    /// Find pool by name <paramref name="name"/> and gets object from it.
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="GetPool{T}"/></typeparam>
    /// <param name="name"><inheritdoc cref="GetPool{T}(string)"/></param>
    /// <returns>Pool's object (or <see langword="null"/> if free object was not finded).</returns>
    public T GetFromPool(string name)
    {
        T obj = GetPool(name).Get();
        
        GetFromPool_custom(obj);
        

        return obj;
    }
    // obj 별 설정 
    public abstract void GetFromPool_custom(T obj);  

    #endregion
    //======================================================================================================================================

    #region Take to pool
    /// <summary>
    /// Returns object back to pool and marks it as free.
    /// </summary>
    /// <param name="index"><inheritdoc cref="GetPool{T}"/></param>
    /// <param name="component">Object (its component) which returns back.</param>
    public void TakeToPool(int index, Component component) => _poolsObjects[index].Take(component);

    /// <summary>
    /// Returns object back to pool and marks it as free.
    /// </summary>
    /// <typeparam name="T">Pool type.</typeparam>
    /// <param name="component">Object (its component) which returns back.</param>
    public void TakeToPool(Component component) => GetPool().Take(component);

    /// <summary>
    /// Returns object back to pool and marks it as free.
    /// </summary>
    /// <typeparam name="T">Pool type.</typeparam>
    /// <param name="name"><inheritdoc cref="GetPool{T}(string)"/></param>
    /// <param name="component">Object (its component) which returns back.</param>
    public void TakeToPool(string name, Component component) => GetPool(name).Take(component);

    public void TakeToPool(T obj)
    {
        TakeToPool_custom(obj);
        TakeToPool(obj.GetId(), obj);
    }

    public abstract void TakeToPool_custom(T obj);  
    
    #endregion



}

