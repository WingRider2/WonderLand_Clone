using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ActivationZone : MonoBehaviour
{
    public GameObject targetObject;
    private IActivatable activatable;

    private void Awake()
    {
        if (targetObject == null)
        {
            Debug.LogError("ActivationZone: targetObject가 할당되지 않았습니다.");
            return;
        }

        activatable = targetObject.GetComponent<IActivatable>();
        if (activatable == null)
            Debug.LogError($"[{targetObject.name}]에 IActivatable 구현체가 없습니다.");
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter called with: " + other.name);
        if (activatable != null && other.TryGetComponent<GrabableItem>(out var grabableItem))
        {
            activatable.Activate();
            grabableItem.RemoveRigidbodyNextFrame(); 
            grabableItem.transform.position = this.transform.position + Vector3.up;
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit called with: " + other.name);
        if (activatable != null && other.GetComponent<GrabableItem>() != null)
            activatable.Deactivate();
    }
}

