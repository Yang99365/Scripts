using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float speed;

    public int startIndex;
    public int endIndex;
    public Transform[] sprites;

    float viewHeight;

    // Update is called once per frame

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2; //view����
    }
    void Update()
    {
        //Move
        Vector3 curPos = transform.position;
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;
        transform.position = curPos + nextPos;


        //background scrolling
        if (sprites[endIndex].position.y < viewHeight*(-1))  //-10���Ϸ� ��������
        {
            //Sprite reUse
            Vector3 backSpritePos = sprites[startIndex].localPosition; //���� ��������Ʈ�� ��ġ ��������
            Vector3 frontSpritePos = sprites[endIndex].localPosition; //�ǾƷ� ��ġ ��������
            sprites[endIndex].transform.localPosition = backSpritePos + Vector3.up * viewHeight;
            //-10 ���Ϸ� ������ endindex��������Ʈ�� ������ġ�� ���� ��������Ʈ�� ������ġ�� +10�� ���ش�.

            //Cursor Indexs Change �ε���������
            int startIndexSave = startIndex; //���� ��������Ʈ�� �ε����� ����
            startIndex = endIndex; //��ġ �Ű����� �ǾƷ� ��������Ʈ�� ���� ��������Ʈ��
            endIndex = (startIndexSave - 1 == -1) ? sprites.Length - 1 : startIndexSave - 1;
            //�����ε����� 1�� ���� -1�̸� index�� 2 �ƴϸ� 1���ش�.

        }

    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Background")
        {
            if (collision.gameObject.transform.GetSiblingIndex() == startIndex)
            {
                sprites[endIndex].position = new Vector3(0, sprites[startIndex].position.y + 10, 0);
                sprites[startIndex].SetSiblingIndex(endIndex);
                int temp = startIndex;
                startIndex = endIndex;
                endIndex = temp;
            }
        }
    }

}
