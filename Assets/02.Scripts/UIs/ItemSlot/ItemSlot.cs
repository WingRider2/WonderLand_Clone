using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData itemData;

    public Inventory inventory;

    public Image icon;
    private Outline outline;

    public int index;
    public bool isEquipped;
    public int quantity;

    //  선택된 슬롯 여부 체크
    public bool isSelected = false;


    private void Awake()
    {
        outline = GetComponent<Outline>();

        if (outline != null)
        {
            outline.effectColor = Color.gray;       //  기본 테두리 색상
            outline.enabled = false;
        }
    }

    private void OnEnable()
    {
        //outline.enabled = isEquipped;

        UpdateOutline();
    }

    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = itemData.icon;
        UpdateOutline();
    }

    public void Clear()
    {
        itemData = null;
        icon.gameObject.SetActive(false);
        isSelected = false;
        UpdateOutline();
    }

    //  선택 시 호출
    public void Select()
    {
        isSelected = true;
        UpdateOutline();
    }

    //  선택 해제 시 호출
    public void Deselect()
    {
        isSelected = false;
        UpdateOutline();
    }

    private void UpdateOutline()
    {
        if(outline == null)
        {
            return;
        }

        outline.enabled = itemData != null;
        outline.effectColor = isSelected ? Color.green : Color.gray;
    }
}
