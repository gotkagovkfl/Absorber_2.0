using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField]
    private GameObject poolingObjectPrefab;

    private Queue<BossNormal> poolingObjectQueue = new Queue<BossNormal>();

    private void Awake()
    {
        Instance = this;
    }

    private BossNormal CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab, transform).GetComponent<BossNormal>();
        newObj.gameObject.SetActive(false);
        return newObj;
    }

    private void Initialize(int count)
    {
        for (int i = 0; i < count; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    public static BossNormal GetObject()
    {
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }else
        {
            var newObj = Instance.CreateNewObject();
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }

    public static void ReturnObject(BossNormal bossnormal)
    {
        bossnormal.gameObject.SetActive(false);
        bossnormal.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(bossnormal);
    }
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
