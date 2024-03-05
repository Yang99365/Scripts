using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // .. ������ ������ ����
    public GameObject[] prefabs;

    // .. Ǯ ����� �ϴ� ����Ʈ��
    List<GameObject>[] pools;

    // .. Ǯ�� �ʱ�ȭ�� ����ϴ� �Լ�
    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length]; //�迭 �ʱ�ȭ
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>(); //�迭 �� ����Ʈ �ʱ�ȭ
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;
        
        // ... ������ Ǯ�� ��� (��Ȱ��ȭ ��) �ִ� ���ӿ�����Ʈ ���� 
        foreach (GameObject item in pools[index]) // �迭, ����Ʈ���� �����͸� ���������� �����ϴ� �ݺ���
        {
            if(!item.activeSelf)
            {
                // ... �߰��ϸ� select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }   
        // ... ��ã������?
        if(!select)
        {
            // ... ���Ӱ� �����ϰ� select ������ �Ҵ�
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
}
