using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    //[Header("Animation")]
    //public Animator animator;

    [Header("Player Stats")]
    public int hp;

    [Header("Movement")]
    private Vector2 curMovementInput;
    public Vector2 CurMovementInput
    {
        get { return curMovementInput; }
        set { curMovementInput = value; }
    }

    public float speed;
    public float jumpForce;

    [Header("Camera Settings")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float CamCurXRot
    {
        get { return camCurXRot; }
        set { camCurXRot = value; }
    }

    public float lookSensitivity;

    public ItemData itemData;
    public event Action<ItemData> addItem;

    public Transform Hand;

    public void TriggerAddItem(ItemData data)
    {
        itemData = data;

        if(itemData == null)
        {
            Debug.LogError("[Player] itemData가 할당되지 않는 상테에서 TriggerAddItem 호출됨");
            return;
        }

        SoundManager.Instance.PlaySFX(SoundType.dropSound);

        addItem?.Invoke(itemData);
    }
}
