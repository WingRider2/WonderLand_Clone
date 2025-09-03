using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public Dictionary<string, GameObject> pool = new();
    private int selectedIndex = -1;
    private PlayerInteraction playerInteraction;

    private void Start()
    {
        ItemDatabase.Load();
        playerInteraction = FindAnyObjectByType<PlayerInteraction>();

        Player.Instance.addItem += AddItem;

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].index = i;
            slots[i].inventory = this;
            slots[i].Clear();
        }
    }

    public void AddItem(ItemData data)
    {
        ItemSlot emptySlot = GetItemStack();

        if (emptySlot != null)
        {
            emptySlot.itemData = data;
            emptySlot.quantity = 1;
            UpdateUI();
        }

        else
        {
        }
    }

    //  플레이어가 슬롯 선택 시 호출되는 메서드
    public void SelectSlot(int index)
    {
        if (index < 0 || index >= slots.Length || slots[index].itemData == null)
        {
            selectedIndex = -1;
            playerInteraction.activeItem = null;

            //  모든 슬롯 선택 해제
            DeselectAllSlots();
            return;
        }

        //  같은 슬롯을 다시 누르면 선택 해제로 전환
        if (selectedIndex == index)
        {
            selectedIndex = -1;
            playerInteraction.activeItem = null;

            //  같은 슬롯을 다시 눌렀을 때 선택 해제
            DeselectAllSlots();
            return;
        }

        selectedIndex = index;

        //  모든 슬롯 선택 해제 후 새 슬롯만 선택
        DeselectAllSlots();
        slots[selectedIndex].Select();

        UseSelectedItem();
    }

    public void UseSelectedItem()
    {
        if (selectedIndex == -1)
        {
            return;
        }

        var itemData = ItemDatabase.GetItemData(slots[selectedIndex].itemData.name);


        if (itemData == null)
        {
            return;
        }

        GameObject prefab = Resources.Load<GameObject>(itemData.prefab);

        if (prefab == null)
        {
            return;
        }

        //  아이템 프리팹을 씬에 생성해서 usable 추출 후 바로 비활성화 처리
        Transform playerTransform = Player.Instance.transform;
        GameObject itemInstance;

        if (!pool.ContainsKey(prefab.name))
        {
            itemInstance = Instantiate(prefab, playerTransform);
            itemInstance.transform.localScale = Vector3.one * 0.0001f;
            itemInstance.gameObject.layer = 0;
            itemInstance.GetComponent<Collider>().enabled = false;
            pool.Add(prefab.name, itemInstance);
        }
        else
        {
            itemInstance = pool[prefab.name];
        }


        IUsableItem usable = itemInstance.GetComponent<IUsableItem>();
        itemInstance.GetComponent<ILoadData>().LoadEffectData(JObject.FromObject(itemData.effectValues));

        playerInteraction.activeItem = usable;

        if (usable != null)
        {
            playerInteraction.activeItem = usable;
        }

        else
        {
            playerInteraction.activeItem = null;
        }

    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemData != null)
            {
                slots[i].Set();
            }

            else
            {
                slots[i].Clear();
            }
        }
    }

    private ItemSlot GetItemStack()
    {
        foreach (var slot in slots)
        {
            if (slot.itemData == null)
            {
                return slot;
            }
        }

        return null;
    }

    //  모든 슬롯의 선택 상태 해제 메서드
    private void DeselectAllSlots()
    {
        foreach (var slot in slots)
        {
            slot.Deselect();
        }
    }
}