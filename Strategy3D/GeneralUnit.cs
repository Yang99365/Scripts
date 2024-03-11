using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralUnit : MonoBehaviour
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
    
    void Start()
    {
        currentHP = maxHP;
    }
    
}
