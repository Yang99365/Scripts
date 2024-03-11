using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // 캐릭터 관리 객체
    // generalManager에서 캐릭터 정보를 가져올 수 있도록 해야함
    
    [Header ("캐릭터SO")]
    public GeneralBase generalBase;
    [Header ("적 플래그(ON으로 적 캐릭터로 취급)")]
    public bool isEnemy; // 적 플래그

    // 가져올 필드 (캐릭터 정보)
    [Header ("캐릭터 이미지")]
    public Sprite charaSprite; // 캐릭터 이미지
    [Header ("캐릭터명")]
    public string charaName; // 캐릭터명
    [Header ("캐릭터ID")]
    public int charaID; // 캐릭터ID
    [Header ("병종")]
    public Troops troopType; // 병종
    [Header ("병종 이미지")]
    public Sprite troopImage; // 병종 이미지
    [Header ("캐릭터 타입")]
    public GeneralType type; // 캐릭터 타입
    [Header ("캐릭터 타입 이미지")]
    public Sprite typeImage; // 캐릭터 타입 이미지

    // 가져올 필드 (스텟)
    [Header ("최대 HP")]
    public int maxHP; // 최대 HP
    [Header ("현재 HP")]
    public int currentHP; // 현재 HP
    [Header ("최대 MP")]
    public int maxMP; // 최대 MP
    [Header ("현재 MP")]
    public int currentMP; // 현재 MP
    [Header ("물리 공격력")]
    public int atk; // 물리 공격력
    [Header ("방어력")]
    public int def; // 방어력
    [Header ("마법 공격력")]
    public int matk; // 마법 공격력
    [Header ("마법 방어력")]
    public int mdef; // 마법 방어력
    [Header ("회피율")]
    public float evade; // 회피율
    [Header ("저항력")]
    public float resist; // 저항력
    [Header ("속도")]
    public int speed; // 속도
    [Header ("이동력")]
    public int move; // 이동력
    [Header ("치명타 확률")]
    public float critical; // 치명타 확률
    [Header ("반격 여부")]
    public bool counter; // 반격 여부

    // 가져올 필드 (스킬 과 장비)
    [Header ("장비")]
    public EquipItem[] equipments; // 장비
    [Header ("액티브스킬")]
    public List<ASkill> Askills; // 액티브스킬
    [Header ("패시브스킬")]
    public List<PSkill> Pskills; // 패시브스킬
    
    
    

    // 메인 카메라
    private Camera mainCamera;

    // 캐릭터 초기 설정 (인스펙터에서 입력)
    [Header ("초기 X 위치(-4～4)")]
    public int initPos_X; // 초기 X 위치
    [Header ("초기 Z 위치(-4～4)")]
    public int initPos_Z; // 초기 Z 위치

    // 게임 중 변화하는 캐릭터 데이터
    [HideInInspector]
    public int xPos; // 현재 x 좌표
    [HideInInspector]
    public int zPos; // 현재 z 좌표

    void Start()
    {

        // 메인 카메라의 참조를 가져옴
        mainCamera = Camera.main;

        // 초기 위치에 해당하는 좌표로 객체를 이동시킴
        Vector3 pos = new Vector3 ();
        pos.x = initPos_X; // x 좌표: 블록 크기가 1(1.0f)이므로 그대로 대입
        pos.y = 1.0f; // y 좌표 (고정)
        pos.z = initPos_Z; // z 좌표
        transform.position = pos; // 객체의 좌표를 변경

        // 객체를 좌우 반전 (빌보드 처리로 인해 한 번 반전되므로)
        Vector2 scale = transform.localScale;
        scale.x *= -1.0f; // X 방향의 크기를 양/음으로 바꿈
        transform.localScale = scale;

        // 기타 변수 초기화
        xPos = initPos_X;
        zPos = initPos_Z;

        currentHP = maxHP;
    }
    
    void Update()
    {
        // 빌보드 처리
        // (스프라이트 객체를 메인 카메라의 방향으로 향하게 함)
        Vector3 cameraPos = mainCamera.transform.position; // 현재 카메라 좌표를 가져옴
        cameraPos.y = transform.position.y; // 캐릭터가 바닥과 수직으로 서 있도록 함
        transform.LookAt (cameraPos);
    }

    /// <summary>
	/// 대상 좌표로 캐릭터 이동시키기
	/// </summary>
	/// <param name="targetXPos">x 좌표</param>
	/// <param name="targetZPos">z 좌표</param>
	public void MovePosition (int targetXPos, int targetZPos)
	{
		// 객체를 이동시킨다.
		// 이동 대상 좌표에 대한 상대 좌표를 구합니다.
		Vector3 movePos = Vector3.zero; // (0.0f, 0.0f, 0.0f)로 Vector3로 초기화
		movePos.x = targetXPos - xPos; // x 방향의 상대적 거리
		movePos.z = targetZPos - zPos; // z 방향의 상대적 거리
		// 이동 처리
		transform.position += movePos;
 
		// 캐릭터 데이터에 위치 저장
		xPos = targetXPos;
		zPos = targetZPos;
	}
}