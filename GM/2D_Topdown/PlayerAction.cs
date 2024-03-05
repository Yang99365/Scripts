using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameManager GM;
    float h;
    float v;
    public float speed;
    bool isHorizonMove; //�밢������
    Vector3 dirVec;

    GameObject scanObject;

    Rigidbody2D rigid;
    Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        h = GM.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = GM.isAction ? 0 : Input.GetAxisRaw("Vertical");

        bool hDown = GM.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = GM.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = GM.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = GM.isAction ? false : Input.GetButtonUp("Vertical");
        //GM.isAction ? false : �׼��߿� ������ ����

        //�����̵� üũ

        if (hDown)  // ���������� ���� ���ΰ��� �����ʴ����ִ»��¿��� �Ȱ��� ����
            isHorizonMove = true;
        else if(vDown)
            isHorizonMove=false;
        else if(hUp || vUp)
            isHorizonMove= h != 0;

        //Anim
        if(anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("IsChange",true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("IsChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            anim.SetBool("IsChange", false);
        }

        //Direction
        if (vDown && v == 1)// ��Ű ��������
        {
            dirVec = Vector3.up;
        }
        else if (vDown && v == -1)
            dirVec = Vector3.down;
        else if (hDown && h == 1) // ������
            dirVec = Vector3.right;
        else if (hDown && h == -1) //����
            dirVec = Vector3.left;

        //Scan Obj
        if(Input.GetButtonDown("Jump")&&scanObject !=null)
        {
            GM.Action(scanObject);
        }

    }
    void FixedUpdate()
    {
        Vector2 moveVec = isHorizonMove ? new Vector2(h,0): new Vector2(0,v);
        rigid.velocity = moveVec *speed;

        //Ray
        Debug.DrawRay(rigid.position,dirVec * 0.7f, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if(rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject; //�� �� ������Ʈ ���
        }
        else { scanObject = null; }
    
    }
}
