using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 데이터만 처리하는 클래스
public class General
{
    
    GeneralBase generalBase;
    int level;

    public int HP { get; set; }

    public List<Skill> Skills { get; set; }

    public General(GeneralBase generalBase, int level)
    {
        this.generalBase = generalBase;
        this.level = level;
        HP = generalBase.MaxHealth;

        foreach (LearnableSkill learnableSkill in generalBase.LearnableSkills)
        {
            if (level >= learnableSkill.Level)
            {
                Skills.Add(new Skill(learnableSkill.Skill));
            }

            if (Skills.Count >= 2) // 액티브 스킬은 2개까지만
            {
                break;
            }
        }
    }

    // 레벨에 따른 스텟 반환
    // 프로퍼티
    public int Attack
    {
        get { return Mathf.FloorToInt((generalBase.PhysicalAttack * level) / 100f); }
    }
    public int Defense
    {
        get { return Mathf.FloorToInt((generalBase.PhysicalDefense * level) / 100f); }
    }
    public int MagicAttack
    {
        get { return Mathf.FloorToInt((generalBase.MagicAttack * level) / 100f); }
    }
    public int Speed
    {
        get { return Mathf.FloorToInt((generalBase.Speed * level) / 100f); }
    }
    public int ActionPoint
    {
        // 100레벨 당 1씩 증가
        get { return Mathf.FloorToInt(generalBase.ActionPoint + (level/100f)); }
    }
    public int MaxHealth
    {
        get { return Mathf.FloorToInt((generalBase.MaxHealth * level) / 100f)+10; }
    }

}
