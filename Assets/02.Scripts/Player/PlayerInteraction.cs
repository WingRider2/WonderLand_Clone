using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    public Transform curInteracttransfrom;

    public Transform Obstacle;

    private IInteractable curInteractable;

    public Camera camera;

    public IUsableItem activeItem;

    public TextMeshProUGUI ItemDescriptionText;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            Debug.DrawRay(ray.origin, ray.direction * maxCheckDistance, Color.green);

            if (Physics.Raycast(ray, out var hit, maxCheckDistance, layerMask))
            {

                if (hit.collider.gameObject.layer == 10 && hit.collider.gameObject.TryGetComponent<BreakObstacles>(out var bos))
                {
                    Obstacle = bos.obstacle;
                }
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    Obstacle = null;
                    curInteractGameObject = hit.collider.gameObject;                    
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                }
            }
            else
            {
                curInteractGameObject = null;
            }
            if (Physics.Raycast(ray, out var obj, maxCheckDistance))
            {
                if(obj.transform.TryGetComponent<ItemDescription>(out var desc))
                {
                    ItemDescriptionText.text = desc.getItemDescription();
                }
                else
                {
                    ItemDescriptionText.text = "";

                }
            }
            else
            {
                ItemDescriptionText.text = "";
            }

        }
    }
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {           
            curInteractable.OnInteract(Player.Instance);
            curInteractGameObject = null;
            curInteractable = null;
        }
    }

    public void OnMouseLeftClick(InputAction.CallbackContext context)
    {
        
        if (context.phase == InputActionPhase.Performed)
        {
            if (activeItem is SizeToggleItem1)
            {
                activeItem.UsePrimary(this.transform);
            }
            else if (activeItem is SandGlass)
            {
                activeItem.UsePrimary(Obstacle);
            }
            else
            {
                if(curInteractGameObject != null)
                {
                    curInteracttransfrom = curInteractGameObject.transform;

                    activeItem.UsePrimary(curInteracttransfrom);
                }
            }
        }
    }

    public void OnMouseRightClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (activeItem is SizeToggleItem1)
            {
                activeItem.UseSecondary(this.transform);
            }
            else if (activeItem is SandGlass)
            {
                activeItem.UseSecondary(Obstacle);
            }
            else
            {
                if (curInteracttransfrom != null)
                {
                    Debug.Log(curInteracttransfrom.name);
                    activeItem.UseSecondary(curInteracttransfrom);
                }

            }
        }
    }

    public void OnRespawnInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        //  세이브 포인트에 도달하기 전까지 해당 기능을 막음.
        if(!SaveManager.Instance.isSaveAvailable)
        {
            SaveUIManager.Instance.ShowRespawnNotAvailableMessage();
            return;
        }

        Vector3 respawnPoint = SaveManager.Instance.GetSavedPosition();
        transform.position = respawnPoint;

        RoomRotator roomRotator = FindObjectOfType<RoomRotator>();

        if(roomRotator != null)
        {
            Vector3 savedRotation = SaveManager.Instance.GetSavedRoomRotationEuler();
            roomRotator.ApplyRotationInstantly(savedRotation);
        }
    }
}
