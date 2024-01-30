using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 아이템을 리스트로 관리하는 인벤토리를 만들겠다.
// 인벤토리 내부에서는 데이터 베이스의 정보를 받아 그 값으로 OnGUI를 통해 UI를 렌더링한다.
public class Inventory : MonoBehaviour
{
    // 아이템 인벤토리
    public List<Item> inventory = new List<Item>();

    // 인벤토리에 값을 전달할 데이터 베이스
    private ItemDB itemDB = null;

    // 인벤토리 오픈여부 확인
    private bool isOpen = false;

    public int slot_x; // 인벤토리 가로
    public int slot_y; // 인벤토리 세로
    public List<Item> slots = new List<Item>(); // 아이템 슬롯
    public GUISkin skin;


    // 2023-10-19 목 (툴팁 기능 추가)
    private bool isOpenToolTip = false;
    private string toolTip;

    private bool isDrag = false; // 드래그 여부를 처리하기 위한 변수
    private Item draggedItem; // 드래그한 아이템 정보에 대한 저장
    private int pre_idx; // 선택했었던 아이템의 위치를 저장하기 위한 변수

    private void Start()
    {
        // 슬롯 생성
        for (int i = 0; i < slot_x * slot_y; i++)
        {
            // 빈 슬롯을 x * y만큼 추가
            // Item 생성자가 수정되어 있기 때문에, 기본 생성자 추가를 진행해줄 것
            slots.Add(new Item());
            
            //인벤토리 추가
            inventory.Add(new Item());
        }
        
        // 게임 내부에 있는 아이템 DB를 검색해서 찾아낸다.
        // 이 작업은 유니티 내에서 ItemDB에 대한 태그를 만들어야 작동한다.
        itemDB = GameObject.FindGameObjectWithTag("ItemDB").GetComponent<ItemDB>();
        
        // 데이터 베이스의 첫 번째 값을 가져온다.
        // 여러 개일 경우에는 인벤토리 전체의 값을 가져올 수 있도록 한다.
        // inventory.Add(itemDB.items[0]);

        for (int i = 0; i < slot_x; i++)
        {
            if (itemDB.items.Count > i)
            {
                // 데이터 베이스의 아이템이 존재하는 경우
                if (itemDB.items[i] != null)
                {
                    inventory[i] = itemDB.items[i];
                }
            }
        }
        
        AddItem(2);
        RemoveItem(1000);
    }

    private void Update()
    {
        // 유니티의 InputManager에서 설정한 Inventory 버튼을 눌렀을 때
        if (Input.GetButtonDown("Inventory"))
        {
            isOpen = !isOpen;
        }
    }

    private void OnGUI()
    {
        // GUI의 스킨을 인벤토리에서 연결해놓은 스킨으로 설정한다.
        GUI.skin = skin;
        
        // 툴팁에 대한 초기화
        toolTip = "";
        
        if (isOpen)
        {
            InventoryRender();

            // 인벤토리의 개수만큼 반복을 진행하면서, 화면에 대한 출력을 진행한다.
            // for (int i = 0; i < inventory.Count; i++)
            // {
            // label은 화면에 출력되는 텍스트를 의미하는 UI이다.
            //     GUI.Label(new Rect(100, i * 20, 200, 50), inventory[i].item_Name);   
            // }
        }
        
        // 툴팁 오픈 여부에 따라 툴팁에 대한 렌더링
        if (isOpenToolTip)
        {
            ToolTipRender();
        }

        if (isDrag)
        {
            OnDragRender();
        }
    }

    private void OnDragRender()
    {
        // 지정한 위치에 드래그한 아이템의 아이콘을 그려준다.
        Rect drag_Rect = new Rect(Event.current.mousePosition.x - 5,
            Event.current.mousePosition.y - 5, 50, 50);
        
        GUI.DrawTexture(drag_Rect,draggedItem.icon);
    }

    private void ToolTipRender()
    {
        // Event : GUI에 대한 이벤트, 해당 예제에서는 OnGUI()를 호출하는 주체를 말한다.
        Rect toolRect = new Rect(Event.current.mousePosition.x + 5, 
            Event.current.mousePosition.y + 2, 200, 200);
        
        // 1. Event.current : 현재 OnGUI를 호출하게 된 이벤트
        
        // 2. Layout Event : GUI의 요소의 포지션, 크기가 변경될 때 처리되는 Event, 실제로 그로잉되는 것이 아닌 관련 기능과 정보를 조사, 배치하는 단계에 대한 조사
        
        // toolRect 좌표 지점에, 툴팁에 적은 문자열을 작성하고, 이미지 스킨은 toolTip으로 설정해 박스를 렌더링한다.
        GUI.Box(toolRect, toolTip, skin.GetStyle("ToolTip"));
    }

    // 슬롯을 가로 세로 형태로 출력할 수 있게 처리한다.
    private void InventoryRender()
    {
        int idx = 0;
        
        for (int j = 0; j < slot_y; j++)
        {
            for (int i = 0; i < slot_x; i++)
            {
                Rect slot_Rect = new Rect(i * 52 + 100, j * 52 + 30, 50, 50);
                
                // 가로와 세로는 반복 수치에 따라 간격이 벌어지도록 설정
                // 박스에 slot 번호를 출력
                // 박스의 이미지는 Background 스타일로 적용
                GUI.Box(slot_Rect, "slot", skin.GetStyle("Background"));

                slots[idx] = inventory[idx];
                
                // 슬롯에 이름이 존재하는 경우(빈 슬롯이 아닌 경우)
                if (slots[idx].item_Name != null)
                {
                    // 텍스처 이미지를 그려주는 도구
                    GUI.DrawTexture(slot_Rect,slots[idx].icon);

                    
                    // 툴팁 기능 연결
                    // 해당 위치에 마우스가 올라와 있다면
                    if (slot_Rect.Contains(Event.current.mousePosition))
                    {
                        toolTip = SetToolTip(slots[idx]);
                        isOpenToolTip = true;
                        
                        // 마우스가 왼쪽 버튼이면서, 마우스가 드래그하려는 상황
                        if (Event.current.button == 0 && 
                            Event.current.type == EventType.MouseDrag && isDrag == false)
                        {
                            // 1. 드래그에 대한 플래그를 활성화
                            isDrag = true;
                        
                            // 2. 현재의 idx 값을 pre_idx에 전달(위치 저장)
                            pre_idx = idx;
                        
                            // 3. 현재 슬롯의 값을 드래그한 아이템으로 설정
                            draggedItem = slots[idx];
                        
                            // 4. 일벤토리를 빈 슬롯으로 전환
                            inventory[idx] = new Item();
                        }

                        // 마우스를 떼고, 드래그하고 있는 아이템이 존재하는 경우(드래그 처리)
                        if (Event.current.type == EventType.MouseUp && isDrag)
                        {
                            // 1. 아이템의 전 위치에 현재의 아이템을 배치한다.
                            inventory[pre_idx] = inventory[idx];
                        
                            // 2. 현재 위치의 인벤토리에 드래그한 아이템을 배치한다.
                            inventory[idx] = draggedItem;
                        
                            // 3. 드래그 처리에 대한 플래그 전환
                            isDrag = false;
                        
                            // 4. 드래그한 아이템에 대한 정보는 비워준다.
                            draggedItem = null;
                        }

                        if (Event.current.isMouse &&
                            Event.current.type == EventType.MouseDown && Event.current.button == 1)
                        {
                            if (inventory[idx].type == Item.ItemType.Use)
                            {
                                switch(inventory[idx].id)
                                {
                                    case 2:
                                        Debug.Log("¾Æ¹«·± È¿°ú°¡ ¾ø¾ú°í, ±×Àú ¸ÀÀÖ±â¸¸ Çß´Ù.");
                                        break;
                                    default:
                                        Debug.Log("ÀÌ°Ç ¹«¾ù¿¡ ¾²´Â ¹°°ÇÀÎ°¡..?");
                                        break;
                                }

                                inventory[idx] = new Item();
                            }
                        }
                    }
                }
                
                //빈 슬롯에 놓는 경우
                else
                {
                    if (slot_Rect.Contains(Event.current.mousePosition))
                    {
                        if (Event.current.type == EventType.MouseUp && isDrag)
                        {
                            // 1. 현재 위치의 인벤토리에 드래그한 아이템을 배치한다.
                            inventory[idx] = draggedItem;
                        
                            // 2. 드래그 처리에 대한 플래그 전환
                            isDrag = false;
                        
                            // 3. 드래그한 아이템에 대한 정보는 비워준다.
                            draggedItem = null;
                        }
                    }
                }
                
                if (toolTip == "")
                {
                    isOpenToolTip = false;
                }
                idx++;
            }
        }
    }

    /// <summary>
    /// 아이템의 유형에 따라, 출력 결과가 다르게 설계되는 방향성을 잡아주는 것이 좋다.
    /// 이름에 특징을 주고 싶을 경우에는 rich text를 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private string SetToolTip(Item item)
    {
        toolTip =
            $"\n    아이템 이름 : {item.item_Name}\n" +
            $"    아이템 공경력 : {item.atk}\n" +
            $"    아이템 방어력 : {item.def}\n" +
            $"    아이템 공격 속도 : {item.speed}\n\n" +
            $"    [아이템 설명]\n" +
            $"    {item.description}";

        return toolTip;
    }

    private void AddItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            // 인벤토리가 비었을 경우
            if (inventory[i] == null)
            {
                // 데이터베이스의 아이템 정보를 검색한다.
                for (int j = 0; j < itemDB.items.Count; j++)
                {
                    if (itemDB.items[j].id == id)
                    {
                        inventory[i] = itemDB.items[j];
                        return;
                    }
                }
            }
        }
    }

    private void RemoveItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].id == id)
            {
                inventory[i] = new Item();
                return;
            }
        }
    }
}
