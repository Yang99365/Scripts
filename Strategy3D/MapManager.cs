using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.U2D.Animation;

public class MapManager : MonoBehaviour
{
    /*
    // 맵 불러오는 코드 생성이 이상해서 고쳐야함
    public GameObject mapPrefab; // 미리 생성된 맵 프리팹
    public GameObject blockPrefab_Grass; // 풀 블록 프리팹
    public GameObject blockPrefab_Water; // 물 블록 프리팹
    public Transform blockParent; // 블록의 부모 오브젝트
    public int MAP_WIDTH = 10; // 맵의 가로 크기
    public int MAP_HEIGHT = 10; // 맵의 세로 크기
    public int GENERATE_RATIO_GRASS = 90; // 풀 블록 생성 확률

    void Start()
    {
        // 미리 생성된 맵 프리팹을 불러옵니다.
        Instantiate(mapPrefab);
    }

    [ContextMenu("Generate Map Prefab")]
    public void GenerateMapPrefab()
    {
        
        // 새로운 게임 오브젝트를 생성하고, 그것을 부모로 설정합니다.
        GameObject newMap = new GameObject("Generated Map");
        blockParent = newMap.transform;

        // 맵을 생성합니다.
        for (int x = 0; x < MAP_WIDTH; x++)
        {
            for (int z = 0; z < MAP_HEIGHT; z++)
            {
                // 블록 생성 위치를 설정합니다.
                Vector3 blockPos = new Vector3(x, 0, z);

                // 블록 프리팹을 결정합니다.
                GameObject blockPrefab = Random.Range(0, 100) < GENERATE_RATIO_GRASS ? blockPrefab_Grass : blockPrefab_Water;

                // 블록을 생성하고, 부모를 설정합니다.
                GameObject block = Instantiate(blockPrefab, blockPos, Quaternion.identity, blockParent);
                block.name = "Block_" + x + "_" + z;
            }
        }

        #if UNITY_EDITOR
        // 생성된 맵을 프리팹으로 저장합니다.
        PrefabUtility.SaveAsPrefabAssetAndConnect(newMap, "Assets/GeneratedMap.prefab", InteractionMode.UserAction);
        #endif
    }
    */

      //맵 만드는 코드
    // 오브젝트와 프리팹 (Inspector에서 지정)
    public Transform blockParent; // 맵 블록의 부모 오브젝트의 Transform
    public GameObject blockPrefab_Grass; // 풀 블록
    public GameObject blockPrefab_Water; // 물 블록

    public CharactersManager charactersManager; // 캐릭터 관리 스크립트
    
    //지도 데이터
    public MapBlock[,] mapBlocks;

    // 상수 정의
    public const int MAP_WIDTH = 9; // 맵의 가로 너비
    public const int MAP_HEIGHT = 9; // 맵의 세로(깊이) 너비
    private const int GENERATE_RATIO_GRASS = 85; // 풀 블록이 생성될 확률

    

    void Start()
    {
        
        // 지도 데이터 초기화
		mapBlocks = new MapBlock[MAP_WIDTH, MAP_HEIGHT];

        // 블록 생성 위치의 기준이 될 좌표 설정
        Vector3 defaultPos = new Vector3 (0.0f, 0.0f, 0.0f); // x:0.0f y:0.0f z:0.0f 의 Vector3 변수 defaultPos 선언
        defaultPos.x = -(MAP_WIDTH / 2); // x 좌표의 기준
        defaultPos.z = -(MAP_HEIGHT / 2); // z 좌표의 기준

        // 블록 생성 처리
        for (int i = 0; i < MAP_WIDTH; i++)
        {// 맵의 가로 너비만큼 반복 처리
            for (int j = 0; j < MAP_HEIGHT; j++)
            {// 맵의 세로 너비만큼 반복 처리
                // 블록의 위치 결정
                Vector3 pos = defaultPos; // 기준 좌표를 기반으로 변수 pos 선언
                pos.x += i; // 첫 번째 for문의 반복 횟수만큼 x 좌표를 이동
                pos.z += j; // 두 번째 for문의 반복 횟수만큼 z 좌표를 이동

                // 블록의 종류 결정
                int rand = Random.Range (0, 100); // 0~99 중에서 하나의 랜덤한 숫자를 얻음
                bool isGrass = false; // 풀 블록 생성 플래그 (초기 상태는 false)
                // 난수 값이 풀 블록 확률 값보다 낮으면 풀 블록을 생성, 또는 이미 캐릭터가 있는 경우
                if (rand < GENERATE_RATIO_GRASS || charactersManager.GetCharacterDataByPos((int)pos.x,(int)pos.z) != null)
                    isGrass = true;

                // 오브젝트 생성
                GameObject obj; // 생성할 오브젝트의 참조
                if (isGrass)
                {// 풀 블록 생성 플래그: ON
                    obj = Instantiate (blockPrefab_Grass, blockParent); // blockParent의 자식으로 풀 블록 생성
                }
                else
                {// 풀 블록 생성 플래그: OFF
                    obj = Instantiate (blockPrefab_Water, blockParent); // blockParent의 자식으로 물 블록 생성
                }
                // 오브젝트의 좌표 적용
                obj.transform.position = pos;

                // 배열 mapBlocks에 블록 데이터 저장
				var mapBlock = obj.GetComponent<MapBlock> (); // オブジェクトのMapBlockを取得
				mapBlocks[i, j] = mapBlock;
                // 블록 데이터 설정
                mapBlock.xPos = (int)pos.x; // X 위치 기록
                mapBlock.zPos = (int)pos.z; // Z 위치 기록
            }
        }
    }
    /// <summary>
	/// 모든 블록 선택 해제하기
	/// </summary>
	public void AllSelectionModeClear ()
	{
		for (int i = 0; i < MAP_WIDTH; i++)
			for (int j = 0; j < MAP_HEIGHT; j++)
				mapBlocks[i, j].SetSelectionMode (MapBlock.Highlight.Off);
	}
    
    void Update()
    {
        
    }

    /// <summary>
    /// 주어진 위치에서 캐릭터가 도달할 수 있는 위치의 블록을 리스트로 반환
    /// </summary>
    /// <param name="xPos">기준 x 위치</param>
    /// <param name="zPos">기준 z 위치</param>
    /// <returns>조건을 만족하는 블록의 리스트</returns>
	public List<MapBlock> SearchReachableBlocks (int xPos, int zPos)
	{
		// 조건을 만족하는 블록 목록
		var results = new List<MapBlock> ();
 
		// 기준점이 되는 블록의 배열 내 번호(index)를 검색한다.
		int baseX = -1, baseZ = -1; // 配列内番号(검색 전에는 -1이 들어감)
		// 검색 처리
		for (int i = 0; i < MAP_WIDTH; i++)
		{
			for (int j = 0; j < MAP_HEIGHT; j++)
			{
				if ((mapBlocks[i, j].xPos == xPos) &&
					(mapBlocks[i, j].zPos == zPos))
				{// 지정된 좌표와 일치하는 맵 블록을 찾습니다.
				 // 배열 내 번호를 구해 루프를 종료합니다.
					baseX = i;
					baseZ = j;
					break; // 두 번째 루프를 빠져나온다.
				}
			}
			// 이미 발견되었다면 첫 번째 루프를 빠져나온다.
			if (baseX != -1)
				break;
		}
 
		// 각 방향의 막다른 골목까지 블록 데이터를 순서대로 가져와서 리스트에 추가한다.
		// X+ 방향
		for (int i = baseX + 1; i < MAP_WIDTH; i++)
			if (AddReachableList (results, mapBlocks[i, baseZ]))
				break;
		// X- 방향
		for (int i = baseX - 1; i >= 0; i--)
			if (AddReachableList (results, mapBlocks[i, baseZ]))
				break;
		//  Z+ 방향
		for (int j = baseZ + 1; j < MAP_HEIGHT; j++)
			if (AddReachableList (results, mapBlocks[baseX, j]))
				break;
		//  Z- 방향
		for (int j = baseZ - 1; j >= 0; j--)
			if (AddReachableList (results, mapBlocks[baseX, j]))
				break;
		// // 발밑의 블록
		results.Add (mapBlocks[baseX, baseZ]);
 
		return results;
	}
	/// <summary>
	///  (캐릭터 도달 블록 검색 처리용)
	/// 지정한 블록을 도달 가능 블록 리스트에 추가한다.
	/// </summary>
	/// <param name="reachableList">도달 가능한 블록 목록</param>
	/// <param name="targetBlock">대상 블록</param>
	/// <returns>막다른 골목 플래그(막다른 골목인 경우 true를 반환)</returns>
	private bool AddReachableList (List<MapBlock> reachableList, MapBlock targetBlock)
	{
		//  대상 블록이 통행이 불가능하면 그곳을 막다른 골목으로 끝낸다.
		if (!targetBlock.passable)
			return true;
 
		// 대상 위치에 다른 캐릭터가 이미 있다면 도달 불가로 종료(막다른 골목으로 만들지 않음)
		var charaData =
			GetComponent<CharactersManager> ().GetCharacterDataByPos (targetBlock.xPos, targetBlock.zPos);
		if (charaData != null)
			return false;
 
		// 도달 가능 블록 리스트에 추가하기
		reachableList.Add (targetBlock);
		return false;
	}

    /// <summary>
	/// 전달받은 위치에서 캐릭터가 공격할 수 있는 위치의 맵 블록을 목록으로 반환합니다.
	/// </summary>
	/// <param name="xPos">기점 x 위치</param>
	/// <param name="zPos">기점 z 위치</param>
	/// <returns>조건을 만족하는 맵 블록 목록</returns>
	public List<MapBlock> SearchAttackableBlocks (int xPos, int zPos)
	{
        
		// 조건을 만족하는 맵 블록의 목록
		var results = new List<MapBlock> ();
		
		// 기준점이 되는 블록의 배열 내 번호(index)를 검색한다.
		int baseX = -1, baseZ = -1; // 배열 내 번호(검색 전에는 -1이 들어감)
		//  검색 처리
		for (int i = 0; i < MAP_WIDTH; i++)
		{
			for (int j = 0; j < MAP_HEIGHT; j++)
			{
				if ((mapBlocks[i, j].xPos == xPos) &&
					(mapBlocks[i, j].zPos == zPos))
				{// 지정된 좌표와 일치하는 맵 블록을 찾습니다.
				 // 배열 내 번호를 구해 루프를 종료합니다.
					baseX = i;
					baseZ = j;
					break; // 두 번째 루프를 빠져나온다.
				}
			}
			// 이미 발견되었다면 첫 번째 루프를 빠져나온다.
			if (baseX != -1)
				break;
		}
 
		// 4방향으로 1칸씩 전진된 위치의 블록을 각각 세트합니다.
        // (가로, 세로 4칸)
        // X+방향
		AddAttackableList (results, baseX + 1, baseZ);
		// X-방향
		AddAttackableList (results, baseX - 1, baseZ);
		// Z+방향
		AddAttackableList (results, baseX, baseZ + 1);
		// Z-방향
		AddAttackableList (results, baseX, baseZ - 1);
		// (대각선 4칸)
        // X+Z+방향
		AddAttackableList (results, baseX + 1, baseZ + 1);
		// X-Z+방향
		AddAttackableList (results, baseX - 1, baseZ + 1);
		// X+Z-방향
		AddAttackableList (results, baseX + 1, baseZ - 1);
		// X-Z-방향
		AddAttackableList (results, baseX - 1, baseZ - 1);
 
		return results;
        
	}
	/// <summary>
	/// (캐릭터 공격 가능 블록 검색 처리용)
    /// 맵 데이터의 지정된 배열 내 번호에 해당하는 블록을 공격 가능 블록 리스트에 추가한다.
	/// </summary>
	/// <param name="attackableList">공격 가능 블록 목록</param>
	/// <param name="indexX">X 방향의 배열 내 번호</param>
	/// <param name="indexZ">Z 방향의 배열 내 번호</param>
	private void AddAttackableList (List<MapBlock> attackableList, int indexX, int indexZ)
	{
		// 지정한 번호가 배열의 외부에 있는 경우
		if (indexX < 0 || indexX >= MAP_WIDTH ||
			indexZ < 0 || indexZ >= MAP_HEIGHT)
			return;
 
		// 도달 가능 블록 리스트에 추가한다.
		attackableList.Add (mapBlocks[indexX, indexZ]);
	}
}