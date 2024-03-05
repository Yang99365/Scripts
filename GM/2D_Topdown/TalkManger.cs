using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class TalkManger : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateDate();
    }

    // Update is called once per frame
    void GenerateDate()
    {
        talkData.Add(2000, new string[] {"�ȳ�?:0", "���� ó���Դ�?:1"});
        talkData.Add(1000, new string[] { "�ȳ�2?:0", "���� ó���Դ�2?:1"});
        talkData.Add(100, new string[] { "����� �������ڴ�." });

        //Quest Talk
        talkData.Add(10+ 1000, new string[] { "���.:0",
                                                "�� ������ ���� ������ �ִ�:1",
                                                 "������ ȣ�� �ʿ� �絵�� �˷��ٲ���.:0"});
        talkData.Add(11 + 2000, new string[] { "����.:1",
                                                "ȣ�� ������ ������ �Ծ�?:0",
                                                 "��Ź �ϳ��� ����� �� ����ó���� ������ �ֿ���.:1"});


        talkData.Add(20 + 1000, new string[] { "�絵�� ����?:1",
        "���� �긮�� �ٴϸ� ������!:3",
        "���߿� �絵���� �Ѹ��� �ؾ߁پ�.:3",});
        talkData.Add(20 + 2000, new string[] { "ã���� �� �� ������ ��.:1",});
        talkData.Add(20 + 5000, new string[] {"��ó���� ������ ã�Ҵ�.", });

        talkData.Add(21 + 2000, new string[] { "��, ã���༭ ����.:2", });

        portraitData.Add(2000 + 0, portraitArr[0]);
        portraitData.Add(2000 + 1, portraitArr[1]);
        portraitData.Add(2000 + 2, portraitArr[2]);
        portraitData.Add(2000 + 3, portraitArr[3]);
        portraitData.Add(1000 + 0, portraitArr[4]);
        portraitData.Add(1000 + 1, portraitArr[5]);
        portraitData.Add(1000 + 2, portraitArr[6]);
        portraitData.Add(1000 + 3, portraitArr[7]);
    }

    public string GetTalk(int id, int talkIndex) //��� ��ȯ
    {
        //����ó��
        if(!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                //����Ʈ �� ó�� ��縶�� ���� ��
                //�⺻ ��縦 ������ �´�.

                return GetTalk(id-id%100, talkIndex); // get first talk
            }
            else
            {
                //�ش� ����Ʈ ���� ���� �� ��簡 ������
                //����Ʈ �� ó����縦 ������ �´�

                return GetTalk(id- id % 10, talkIndex); // get first quest talk

            }


        }

        if(talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }
            

    }
    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
