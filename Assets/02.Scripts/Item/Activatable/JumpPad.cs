using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class JumpPad : MonoBehaviour
{
    public float jumpMultiplier = 1.5f;
    public bool isAutomatic = false;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if(!isAutomatic)
            {
                var controller = collision.transform.GetComponent<Player>();
                if (controller != null)
                    controller.jumpForce *= jumpMultiplier;
            }
            else
            {
                var rb = collision.transform.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(Vector3.up * rb.mass * jumpMultiplier, ForceMode.Impulse);
                }
            }
        
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (!isAutomatic)
            {
                var controller = collision.transform.GetComponent<Player>();
                if (controller != null)
                    controller.jumpForce /= jumpMultiplier;
            }
        
        }
    }
}
