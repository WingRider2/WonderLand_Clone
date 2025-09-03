using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private float rotateDuration = 0.5f;
    private float duration = 5.0f;
    public void RotateCube(Vector3 rotateAngle)
    {
        StartCoroutine(RotateOverTime(rotateAngle));
    }
    private IEnumerator RotateOverTime(Vector3 byAngles)
    {
        yield return new WaitForSeconds(rotateDuration);
        Quaternion from = transform.rotation;
        Quaternion to = Quaternion.Euler(transform.eulerAngles + byAngles);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = to;
       
    }
}
