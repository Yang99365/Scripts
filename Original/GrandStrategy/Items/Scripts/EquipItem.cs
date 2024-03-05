using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equip Item", menuName = "Inventory/Items/Equip Item")]
public class EquipItem : GItemSO
{
    public enum EquipType
    {
        Weapon,
        Armor,
        Accessory
    }
    public enum EquipSlot
    {
        Head,
        Body,
        Hand,
        Foot,
        Accessory,
        Weapon
    }
    public EquipType equipType;
    public EquipSlot equipSlot;
    public float atkBonus;
    public float defBonus;
    public float intBonus;
    public float spdBonus;
    public float actionBonus;
    
    public void Awake()
    {
        Type = ItemType.Equipment;
        isStackable = false;
    }
}
