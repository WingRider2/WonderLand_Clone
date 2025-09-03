using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class MovePlafom : MonoBehaviour , IActivatable
{
    public float moveSpeed;
    public float moveHight;

    public bool isfirst = true;
    private void Awake()
    {
       if(this.transform.TryGetComponent<Rigidbody>(out var rigidbody))
        {
            rigidbody.isKinematic = true;
        }
    }
    public IEnumerator Move()
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + Vector3.up * moveHight;
        Quaternion targetRot = Quaternion.Euler(90f, 0f, 0f);

        while (true)
        {

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );


            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRot,
                moveSpeed * 100f * Time.deltaTime   
            );

            if (Vector3.Distance(transform.position, targetPos) < 0.01f
                && Quaternion.Angle(transform.rotation, targetRot) < 0.1f)
            {
                transform.position = targetPos;
                transform.rotation = targetRot;
                yield break;
            }

            yield return null;
        }
    }

    public void Activate()
    {
        if (isfirst)
        {
            StartCoroutine(Move());
            isfirst = false;
        }
        
    }

    public void Deactivate()
    {
        throw new System.NotImplementedException();
    }
}
