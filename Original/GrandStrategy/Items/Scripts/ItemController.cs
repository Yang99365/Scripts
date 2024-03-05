using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    //컨트롤러가 아니라 아이템 데이타로 이름을 바꾸는 것이 좋을 것 같다.
    public GItemSO []items;
    
    void Start()
    {
        /* 코파일럿이 아래 코드를 생성하도록 지시했습니다.
        items = new GItemSO[3];
        items[0] = ScriptableObject.CreateInstance<GItemSO>();
        items[0].itemName = "Battery";
        items[0].amount = 1;
        items[0].itemType = GItemSO.ItemType.Consumable;
        items[0].itemIcon = Resources.Load<Sprite>("Sprites/Items/Battery");
        items[0].itemPrefab = Resources.Load<GameObject>("Prefabs/Items/Battery");
        items[0].itemDescription = "Battery for flashlight";
        items[0].itemPrice = 100;
        
        items[1] = ScriptableObject.CreateInstance<GItemSO>();
        items[1].itemName = "Flashlight";
        items[1].amount = 1;
        items[1].itemType = GItemSO.ItemType.Equipment;
        items[1].itemIcon = Resources.Load<Sprite>("Sprites/Items/Flashlight");
        items[1].itemPrefab = Resources.Load<GameObject>("Prefabs/Items/Flashlight");
        items[1].itemDescription = "Flashlight for dark";
        items[1].itemPrice = 1000;
        
        items[2] = ScriptableObject.CreateInstance<GItemSO>();
        items[2].itemName = "Key";
        items[2].amount = 1;
        items[2].itemType = GItemSO.ItemType.Quest;
        items[2].itemIcon = Resources.Load<Sprite>("Sprites/Items/Key");
        items[2].itemPrefab = Resources.Load<GameObject>("Prefabs/Items/Key");
        items[2].itemDescription = "Key for locked door";
        items[2].itemPrice = 10000;
        */
    }

    // 아이템so를 담아둔 아이템 컨트롤러, 아이템 루팅 시스템은 아이템 컨트롤러에서 아이템을 정하고 루팅한다.
}
