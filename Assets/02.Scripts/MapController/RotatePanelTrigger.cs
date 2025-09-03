// RotatePanelTrigger.cs
using System;
using UnityEngine;
using System.Collections;

public class RotatePanelTrigger : MonoBehaviour
{
    [Tooltip("월드 축을 지정하세요. 예) (1,0,0)=(X축), (0,1,0)=(Y축) 등")]
    public Vector3 rotateAxis = Vector3.right;
    [Tooltip("위에서 지정한 축을 기준으로 몇 도 회전할지")]
    public float rotateAngle = 90f;
    public RoomRotator roomRotator;
    [Tooltip("플레이어를 얼마나 띄울지 (Impulse 세기)")]
    public float liftImpulse = 2f;

    public bool isFixed;

    public bool camRotate=true;
    public Transform player;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        if (!camRotate) return;
        camRotate = false;
        other.GetComponent<Rigidbody>().AddForce(Vector3.up*20, ForceMode.Impulse);
        player = other.transform;
        //player.transform.SetParent(this.transform);
        if (!roomRotator.isRotating)
        {
            roomRotator.StartCoroutine(
                roomRotator.RotateBy(rotateAxis, rotateAngle)
            );
                        
        }
        if (isFixed)
        {
            other.transform.SetParent(this.transform);  
        }
        roomRotator.onRotateComplete = null;
        roomRotator.onRotateComplete += RotateComplete;
        if (other.transform.TryGetComponent<PlayerController>(out var playerController))
        {
            playerController.OffCanMoveLook();
            roomRotator.onRotateComplete += playerController.OnCanMoveLook;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        if (roomRotator.isRotating)        
        {
            roomRotator.isRotating = false;
        }

    }
    void RotateComplete()
    {
        player.transform.SetParent(null , true);
        //player.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(0, 0, 0));
        player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        //StartCoroutine(RotateBy(player));
        camRotate = true;
    }
    public IEnumerator RotateBy(Transform intransform)
    {
        if (roomRotator.isRotating) yield break;
        roomRotator.isRotating = true;

        Quaternion start = intransform.rotation;
        
        Quaternion end = new Quaternion(0, 0, 0, 0);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / 1f;
            transform.rotation = Quaternion.Slerp(start, end, t);
            yield return null;
        }

        transform.rotation = end;
    }

}

