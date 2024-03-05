using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLootSystem : MonoBehaviour
{
    public ItemController itemController;
    public ShopData shopData;
    public GItemSO item;

    void Start()
    {
        item = itemController.items[0]; // 배터리
        
    }
    void Loot()
    {
        Inventory.instance.Add(item, 45);
    }
    void Loot2()
    {
        Inventory.instance.Add(itemController.items[1]); //검
    }
    
    // Z키를 누르면 아이템을 얻는다. 임시
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Loot();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Loot2();
        }
        
    }
}
