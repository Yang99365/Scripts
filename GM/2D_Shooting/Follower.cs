using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float maxShotDelay;
    public float curShotDelay;

    public ObjectManager objectManager;

    public Vector3 followPos;
    public int followDelay;
    public Transform parent;
    public Queue<Vector3> parentPos;


    void Awake()
    {
        parentPos = new Queue<Vector3>();
    }
    void Update()
    {
        Watch();
        Follow();
        Fire();       
        Reload();
    }

    void Watch()
    {
        //input pos
        if(!parentPos.Contains(parent.position)) //�θ���ġ�� ������������ �������
        {
            parentPos.Enqueue(parent.position);
        }
        

        //output pos
        if(parentPos.Count > followDelay)
        {
            followPos = parentPos.Dequeue();
        }
        else if (parentPos.Count < followDelay) //���۽� ä��������
        {
            followPos = parent.position;
        }
    }
    void Follow()
    {
        transform.position = followPos;
    }

    void Fire()
    {
        if (!Input.GetButton("Fire1")) // ��Ŭ�� �ƴ� ���¸� Ż��
            return;

        if (curShotDelay < maxShotDelay) //cur���� max������ �������¸� �Ƚ�� Ż��
            return;

        GameObject bullet = objectManager.MakeObj("BulletFollower");
        bullet.transform.position = transform.position;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        

        curShotDelay = 0;

    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
}

    
