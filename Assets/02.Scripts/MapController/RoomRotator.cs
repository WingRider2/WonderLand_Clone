// RoomRotator.cs
using System;
using System.Collections;
using UnityEngine;

public class RoomRotator : MonoBehaviour
{
    public float rotationDuration = 1f;
    public bool isRotating;
    public CameraSwitcher cameraSwitcher;
    
    public Action onRotateComplete;
    public IEnumerator RotateBy(Vector3 axis, float angle)
    {
        if (isRotating) yield break;
        isRotating = true;

        // [추가] 회전 시작 효과음 재생
        SoundManager.Instance.PlaySFX(SoundType.roomRotateSound); // 원하는 enum으로

        Quaternion start = transform.rotation;
        Quaternion delta = Quaternion.AngleAxis(angle, axis.normalized);
        Quaternion end = delta * start;

        cameraSwitcher.SwitchToSubCamera();
        
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / rotationDuration;
            transform.rotation = Quaternion.Slerp(start, end, t);
            yield return null;
        }

        transform.rotation = end;
        onRotateComplete?.Invoke();
        cameraSwitcher.SwitchToMainCamera();
    }

    public void ApplyRotationInstantly(Vector3 eulerRotation)
    {
        transform.rotation = Quaternion.Euler(eulerRotation);
    }
}


