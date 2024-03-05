using Packages.Rider.Editor.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float hp;
    public int enemyScore;
    public Sprite[] sprites;
    public string enemyName;
    public bool isDead;

    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletA;
    public GameObject bulletB;
    public GameObject itemPower;
    public GameObject itemBoom;
    public GameObject itemCoin;
    public GameObject player;
    public ObjectManager objectManager;
    public GameManager gameManager;

    // Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;
    

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(enemyName == "B")
        {
            anim = GetComponent<Animator>();
        }

        //rigid = GetComponent<Rigidbody2D>();
        //rigid.velocity = Vector2.down * speed; //�Ʒ��θ� ������
    }

    void OnEnable()
    {
        switch (enemyName)
        {
            case "B":
                hp = 3000;
                
                Invoke("Stop", 2);
                break;
            case "L":
                hp = 50;
                spriteRenderer.sprite = sprites[0];
                break;
            case "M":
                hp = 15;
                spriteRenderer.sprite = sprites[0];
                break;
            case "S":
                hp = 3;
                spriteRenderer.sprite = sprites[0];
                break;
        }
    }

    void Stop()
    {
        if (!gameObject.activeSelf)//��Ȱ��ȭ�� Ż��
            return;
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Invoke("Think", 2);
    }

    void Think()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1; //���� 3�ѱ�� 0���� �ʱ�ȭ
        curPatternCount = 0; //���� ����� ����Ƚ�� �ʱ�ȭ

        switch (patternIndex)
        {
            case 0:
                FireForward();
                break; 
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
        }
    }

    void FireForward()
    {
        if (hp <= 0) return;
        //Fire 4 Bullet Forward
        GameObject bulletR = objectManager.MakeObj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        GameObject bulletRR = objectManager.MakeObj("BulletBossA");
        bulletRR.transform.position = transform.position + Vector3.right * 0.45f;

        GameObject bulletL = objectManager.MakeObj("BulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.3f;
        GameObject bulletLL = objectManager.MakeObj("BulletBossA");
        bulletLL.transform.position = transform.position + Vector3.left * 0.45f;


        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

       
        rigidR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        
        //Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex]) //�� ���Ϻ� Ƚ�� ���� �� ������������
            Invoke("FireForward", 2);
        else
            Invoke("Think", 3);
    }

    void FireShot()
    {
        if (hp <= 0) return;
        //Fire Shot
        for (int index=0;index<5;index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyB");
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position; //��ǥ�� ����
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 4, ForceMode2D.Impulse);
        }
        

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex]) //�� ���Ϻ� Ƚ�� ���� �� ������������
            Invoke("FireShot", 3.5f);
        else
            Invoke("Think", 3);
    }

    void FireArc()
    {
        if (hp <= 0) return;
        for (int index = 0; index < 5; index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyA");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            //�Ѿ� Ƚ�� Ȧ���� �ؾ� �̵� ��������
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI * 10 * curPatternCount / maxPatternCount[patternIndex]), -1); ;
            rigid.AddForce(dirVec.normalized * 4, ForceMode2D.Impulse);
        }

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex]) //�� ���Ϻ� Ƚ�� ���� �� ������������
            Invoke("FireArc", 0.15f);
        else
            Invoke("Think", 3);
    }

    void FireAround()
    {
        if (hp <= 0) return;
        //Fire Around
        int roundNumA = 50;
        int roundNumB = 40;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;
        for (int i = 0; i < roundNum; i++)
        {
            GameObject bullet = objectManager.MakeObj("BulletBossB");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            //�Ѿ� Ƚ�� Ȧ���� �ؾ� �̵� ��������
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundNum), 
                                        Mathf.Sin(Mathf.PI * 2 * i / roundNum));//����

            rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * i / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
        }
        

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex]) //�� ���Ϻ� Ƚ�� ���� �� ������������
            Invoke("FireAround", 0.7f);
        else
            Invoke("Think", 3);
    }

    void Update()
    {
        if (enemyName == "B")
            return;

        Fire();
        Reload();
    }

    void Fire()
    {
        if (curShotDelay < maxShotDelay) //cur���� max������ �������¸� �Ƚ�� Ż��
            return;

        if(enemyName == "S")
        {
            GameObject bullet = objectManager.MakeObj("BulletEnemyA");
            bullet.transform.position = transform.position;
            
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position; //��ǥ�� ����
            rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);
        }
        else if(enemyName == "L")
        {
            GameObject bulletR = objectManager.MakeObj("BulletEnemyB");
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;
            
            GameObject bulletL = objectManager.MakeObj("BulletEnemyB");
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;
            
            
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f); //��ǥ�� ����
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);

            rigidR.AddForce(dirVecR.normalized * 3, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 3, ForceMode2D.Impulse);//normalized ������ ���� ũ���1
        }

        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;

    }


    public void OnHit(int dmg)
    {
        if (hp <= 0)
            return;

        hp -= dmg;
        if(enemyName == "B")
        {
            anim.SetTrigger("OnHit");
        }
        else
        {
            spriteRenderer.sprite = sprites[1]; //�ǰݽ� ��������Ʈ ��ü
            Invoke("ReturnSprite", 0.1f); //0.1���Ŀ� ReturnSprite()�Լ� ȣ��
        }
        

        if(hp <= 0 && !isDead)
        {
            isDead = true; //�׾����� �˸� + �ѹ��� �����ϰ� �ؼ� �����ߺ�x
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;

            //Random Ratio Item Drop
            int ran = enemyName == "B" ? 0 : Random.Range(0, 10); //������ ������x
            if (ran < 3) //30% 1,2,3
            {
                Debug.Log("������ ����");
            }
            else if (ran < 6) // 30% 4,5,6
            {
                GameObject itemCoin = objectManager.MakeObj("ItemCoin");
                itemCoin.transform.position = transform.position;

                
                
            }
            else if (ran < 8) //20% 7.8
            {
                GameObject itemPower = objectManager.MakeObj("ItemPower");
                itemPower.transform.position = transform.position;

                
            }
            else if (ran < 10) //20% 9,10
            {
                GameObject itemBoom = objectManager.MakeObj("ItemBoom");
                itemBoom.transform.position = transform.position;

                
            }
            isDead = false;
            gameObject.SetActive(false);
            CancelInvoke();
            transform.rotation = Quaternion.identity;
            gameManager.CallExplosion(transform.position, enemyName);
            
        }
    }
    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet" && enemyName != "B")
        {
            
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
            collision.gameObject.SetActive(false);
        
        }
    }



}
