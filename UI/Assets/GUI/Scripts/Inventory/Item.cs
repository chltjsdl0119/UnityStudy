using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 유니티 에디터에서 직접 클래스, 구조체에 대한 접근을 진핼할 수 있게 처리해주는 속성이다.

// 현재 Item 스크립트는 데이터에 대한 설계만을 목적으로 사용하고 있고,
// ItemDB를 통해 추가할 것으로, 직접 오브젝트에 붙힐 수 있는 Monobehavior 상속을 받지 않는다.

[System.Serializable]

public class Item
{
    // 아이템에 관련된 기본 정보
    [Header("===== 아이템 기본 정보 =====")] 
    public string item_Name;
    public int id;
    public string description;
    public Texture2D icon;

    // 아이템 능력치
    // 공격력, 방어력, 공격 속도, 아이템 유형
    [Header("===== 능력치 =====")] 
    public int atk;
    public int def;
    public int speed;
    public ItemType type;

    /// <summary>
    /// 0 : 무기
    /// 1 : 방어구
    /// 2 : 소모품
    /// </summary>
    public enum ItemType
    {
        Weapon,
        Armor,
        Use
    }

    public Item(string icon_text, string itemName, int id, string description, int atk, int def, int speed, ItemType type)
    {
        this.item_Name = itemName;
        this.id = id;
        this.description = description;
        this.atk = atk;
        this.def = def;
        this.speed = speed;
        this.type = type;

        // 리소스 폴더/ 아이템 폴더/ 파일 이름
        icon = Resources.Load<Texture2D>($"ItemIcon/" + icon_text);
    }

    // 기본 생성자 (default Constructor)
    // 해당 생성자가 있을 경우, 클래스를 선언할 수 있다.
    // 따로 생성자를 만들지 않았을 경우, 자동으로 설정되는 형태이기도 하다.
    public Item()
    {
    }
}
