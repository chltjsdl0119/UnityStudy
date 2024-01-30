using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 프로그램 시작 시, DB에 추가해둔 정보를 갱신할 것이다.
// 해당 스크립트는 json, scriptable Object와 같은 데이터 저장 기능을 쓰지 않고, 아이템 클래스에 대한 정보를 리스트로 저장하는 것으로 DB를 연출할 것이다.

public class ItemDB : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    
    // Start is called before the first frame update
    void Start()
    {
        // 리스트의 기본 함수 Add는 데이터를 리스트에 추가하는 기능이다.
        
        // 아이템에 대한 추가 진행
        // 생성자의 순서대로 인자 값을 넣어서 생성을 진행한다.
        
        // 맨 처음에 넣어주는 문장은 실제 리소스 폴더에 있는 이미지의 이름
        // 이름을 일일이 넣으면 불편하기 때문에, 리소스 폴더 내의 이미지는 숫자로 저장해 놓으면 편리하다.
        
        items.Add(new Item("1", "억까 방어구", 100, "억까 데미지 무효화 방어구.", 0, 9999, 0, Item.ItemType.Armor));
        items.Add(new Item("2", "억까단 처단용 도끼", 100, "억까단 머리를 쪼개버릴 수 있는 도끼.", 9999, 5, 9999, Item.ItemType.Weapon));
        items.Add(new Item("3", "독사과", 100, "억까단 암살용 사과.", 9999, 0, 0, Item.ItemType.Use));
        items.Add(new Item("4", "뭐냐", 100, "뭐냐", 9999, 9999, 9999, Item.ItemType.Use));
        items.Add(new Item("5", "고기", 100, "맛깔난 고기", 0, 0, 0, Item.ItemType.Use));
    }
}
