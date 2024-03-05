using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Area")) //�浹�� �±װ� Area�϶��� ����(Ż�����)
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;
        
        //input�ý���
        //Vector3 playerDir = GameManager.instance.player.inputVec;

        
        switch (transform.tag)
        {
            case "Ground":
                float diffX = playerPos.x - myPos.x;
                float diffY = playerPos.y - myPos.y;
                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);
                //�� ������Ʈ ��ġ���̸� Ȱ���� �������� ���� (16������ȯ)

                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "Enemy":
                if(coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3,3),Random.Range(-3,3),0);
                    transform.Translate(ran + dist * 2); //�������͸� ���Ͽ� �����ִ� ���� ���ġ
                }

                break;
            
        }
    }
}
