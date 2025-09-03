using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutArea : MonoBehaviour
{
    public Transform WallResponPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            other.transform.position = WallResponPos.position;
            other.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
