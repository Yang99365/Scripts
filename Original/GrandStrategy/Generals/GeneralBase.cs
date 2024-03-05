using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "General", menuName = "Generals/GeneralBase")]
public class GeneralBase : ScriptableObject
{
    // general info
    [SerializeField]
    new string name;
    [SerializeField]
    string code;
    [SerializeField]
    Sprite portrait;
    [SerializeField]
    GeneralType type;
    [SerializeField]
    ReadUnitType unitType;

    // stats
    [Header("Stats")]
    [SerializeField]
    int level;
    [SerializeField]
    int maxHealth;
    [SerializeField]
    int maxMana;
    [SerializeField]
    int physicalAttack;
    [SerializeField]
    int physicalDefense;
    [SerializeField]
    int magicAttack;
    [SerializeField]
    int magicDefense;
    [SerializeField]
    int evade;
    [SerializeField]
    int resistance;
    [SerializeField]
    int speed;
    [SerializeField]
    int moveRange;
    [SerializeField]
    int jumpPower;
    [SerializeField]
    int count;
    [SerializeField]
    int actionPoint;
    [SerializeField]
    int cost;
    [SerializeField]
    bool status;

    // skills
    [Header("Skills")]
    [SerializeField]
    List<LearnableSkill> learnableSkills;

    [SerializeField]
    string Faction;

    // getter
    public string Name { get => name;}
    public string Code { get => code;}
    public Sprite Portrait { get => portrait;}
    public GeneralType Type { get => type;}
    public ReadUnitType UnitType { get => unitType;}
    public int Level { get => level;}
    public int MaxHealth { get => maxHealth;}
    public int MaxMana { get => maxMana;}
    public int PhysicalAttack { get => physicalAttack;}
    public int PhysicalDefense { get => physicalDefense;}
    public int MagicAttack { get => magicAttack;}
    public int MagicDefense { get => magicDefense;}
    public int Evade { get => evade;}
    public int Resistance { get => resistance;}
    public int Speed { get => speed; set => speed = value; }
    public int MoveRange { get => moveRange;}
    public int JumpPower { get => jumpPower;}
    public int Count { get => count;}
    public int ActionPoint { get => actionPoint;}
    public int Cost { get => cost;}
    public bool Status { get => status;}
    
    public List<LearnableSkill> LearnableSkills { get => learnableSkills;}
}

[Serializable]
public class LearnableSkill
{
    [SerializeField]
    public SkillBase SkillBase;
    [SerializeField]
    public int level;

    public SkillBase Skill { get => SkillBase; }

    public int Level { get => level; }
}

public enum GeneralType
{
    Warrior,
    MagicWarrior,
    Mage,
    Archer,
    Cavalry,
    Healer,
    Tank,
}
public enum ReadUnitType
{
    HeavyInfantry,
    LightInfantry,
    HeavyCavalry,
    LightCavalry,
    LongBow,
    CrossBow,
    Mage,
    Healer,
    Prist,
}
