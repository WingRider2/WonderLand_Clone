// DoorController.cs
using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour, IActivatable
{
    public float openAngle = 90f;
    public float openSpeed = 2f;

    private Quaternion closedLocalRot;
    private Quaternion openLocalRot;
    private Coroutine rotating;

    private void Awake()
    {
        // 부모(맵) 기준 로컬 회전 저장
        closedLocalRot = transform.localRotation;
        openLocalRot = closedLocalRot * Quaternion.Euler(0, 0, openAngle);
    }

    public void Activate()
    {
        if (rotating != null) StopCoroutine(rotating);
        rotating = StartCoroutine(RotateTo(openLocalRot));

        // 문 열기 사운드 추가
        SoundManager.Instance.PlaySFX(SoundType.openDoorSound);
    }

    public void Deactivate()
    {
        if (rotating != null) StopCoroutine(rotating);
        rotating = StartCoroutine(RotateTo(closedLocalRot));

        // 문 닫기 사운드 추가
        SoundManager.Instance.PlaySFX(SoundType.closeDoorSound);
    }

    private IEnumerator RotateTo(Quaternion targetLocal)
    {
        // 로컬 공간 회전으로 바꿨습니다!
        while (Quaternion.Angle(transform.localRotation, targetLocal) > 0.1f)
        {
            transform.localRotation = Quaternion.Slerp(
                transform.localRotation,
                targetLocal,
                Time.deltaTime * openSpeed
            );
            yield return null;
        }
        transform.localRotation = targetLocal;
    }
}

