using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] prefabs; // �������(����)�� ������ �迭 ���� ����, �پ��� ����� ���� ���尡��
    
    [SerializeField]
    List<GameObject>[] pools; // ��������� ��� �迭 ����

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int i=0; i<pools.Length; i++)
        {
            // pools ����Ʈ �ʱ�ȭ
            pools[i] = new List<GameObject>();
        }
    }
    public GameObject Get(int i)
    {
        GameObject select = null;
        foreach(GameObject item in pools[i])
        { // �迭 ��ȸ
            if (!item.activeSelf)
            { // ���� �Ⱦ��� �ִ� ������Ʈ(����)�� �ֳ�?
                select = item;
                select.SetActive(true); // �Ⱦ��� �ִ� �� Ȱ��ȭ
                break;
            }
        }

        if (!select)
        { // �Ⱦ��� ������Ʈ�� ã�� ���ϸ� ����
            select = Instantiate(prefabs[i], transform);
            pools[i].Add(select);
        }


        return select;
    }
}
