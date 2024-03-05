using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public string[] enemyObjs;
    public Transform[] spawnPoints;

    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public Text scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;
    public GameObject gameOverSet;

    public ObjectManager objectManager;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    void Awake()
    {
        spawnList = new List<Spawn>();
        enemyObjs = new string[] {"EnemyS", "EnemyM", "EnemyL", "EnemyB"};
        ReadSpawnFile();
    }

    void ReadSpawnFile()
    {
        //���� �ʱ�ȭ
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        //������ ���� �б�
        TextAsset textFile = Resources.Load("Stage0") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while(stringReader != null)
        {
            string line = stringReader.ReadLine();
            Debug.Log(line);
            if (line == null)
                break;

            //������ ������ ����
            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }
        //�ؽ�Ʈ ���� �ݱ�
        stringReader.Close();

        //ù��° ���� ������ ����
        nextSpawnDelay = spawnList[0].delay;
    }

    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            //nextSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }

        // #.UI Score Update
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
        //"{0:n0}" ���ڸ����� ��ǥ�� �����ִ� ���ھ��
    }

    void SpawnEnemy()
    {
        int enemyIndex = 0;
        switch (spawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "L":
                enemyIndex = 2;
                break;
            case "B":
                enemyIndex = 3;
                break;
        }
        //int ranEnemy = Random.Range(0, 3); //0,1,2��ȯ
        //int ranPoint = Random.Range(0, 9);
        int enemyPoint = spawnList[spawnIndex].point;
        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;
                    
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();

        enemyLogic.player = player; //������ �����Ŀ� �÷��̾� ���� �Ѱ��ֱ�
        enemyLogic.gameManager = this; //���ӸŴ��� �� �ڽ��� �ٷ� �ѱ�
        enemyLogic.objectManager = objectManager;

        if(enemyPoint == 5 || enemyPoint == 6) //Left Spawn
        {
            enemy.transform.Rotate(Vector3.back * 90); //back = z��-1 , �� ���¹��� �ٲٱ�
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else if (enemyPoint == 7 || enemyPoint == 8) //Right Spawn
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else //Front Spawn
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }

        //������ �ε��� ����
        spawnIndex++;
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }
        //���� ������ ������ ����
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe",2f);       
    }
    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 4f;
        player.SetActive(true);

        Player playerLogic = player.GetComponent<Player>();
        playerLogic.isHit = false;
    }
    public void UpdateLifeIcon(int life)
    {
        // UI life init disable
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }

        // UI life init Active
        for (int index = 0; index < life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void UpdateBoomIcon(int boom)
    {
        // UI boom init disable
        for (int index = 0; index < 3; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 0);
        }

        // UI boom init Active
        for (int index = 0; index < boom; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }
    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    public void CallExplosion(Vector3 pos, string type)
    {
        GameObject explosion = objectManager.MakeObj("Explosion");
        Explosion explosionLogic = explosion.GetComponent<Explosion>();

        explosion.transform.position = pos;
        explosionLogic.StartExplosion(type);
    }
}