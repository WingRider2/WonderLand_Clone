using System.Collections;
using UnityEngine;

public class ElevatorController : MonoBehaviour, IActivatable
{
    public Transform platform;
    public Vector3 upOffset = new Vector3(0, 5, 0);
    public float moveSpeed = 2f;

    private Vector3 downPos;
    private Vector3 upPos;
    private Coroutine moving;

    private void Awake()
    {
        downPos = platform.localPosition;
        upPos = downPos + upOffset;
    }

    public void Activate()
    {
        if (moving != null) StopCoroutine(moving);
        moving = StartCoroutine(MoveTo(upPos));
    }

    public void Deactivate()
    {
        if (moving != null) StopCoroutine(moving);
        moving = StartCoroutine(MoveTo(downPos));
    }

    private IEnumerator MoveTo(Vector3 target)
    {
        while ((platform.localPosition - target).sqrMagnitude > 0.01f)
        {
            platform.localPosition = Vector3.MoveTowards(
                platform.localPosition,
                target,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }
    }
}
