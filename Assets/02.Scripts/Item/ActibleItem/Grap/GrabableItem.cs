using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class GrabableItem : MonoBehaviour
{
    private Rigidbody rb;
    private FixedJoint joint;
    private Rigidbody handRb;

    public Transform hand;
    void Awake()
    {
        // 시작할 때 Rigidbody가 붙어 있다면 제거
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Destroy(rb);
            rb = null;
        }
    }
    private void FixedUpdate()
    {
        if(this.transform.position.y< -50f)
        {
            rb.velocity = Vector3.zero;
            this.transform.position = Vector3.zero;
        }
    }
    private void Start()
    {
        handRb = hand.transform.GetComponent<Rigidbody>();
    }

    public void UsePrimary()
    {
        // ① Rigidbody가 없으면 붙여 주고 세팅
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            if(rb == null)
            {
                rb = transform.GetComponent<Rigidbody>();
            }
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.useGravity = false;
            rb.isKinematic = false;  // Joint를 위해 Dynamic 모드
        }

        // ② 관성 초기화 & Joint 붙이기
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = handRb;
        joint.breakForce = Mathf.Infinity;
        joint.breakTorque = Mathf.Infinity;
    }

    public void UseSecondary()
    {
        // ① Joint 제거
        if (joint != null)
        {
            Destroy(joint);
            joint = null;
        }
        if (rb == null)
        {
            rb = transform.GetComponent<Rigidbody>();
        }
        if (rb != null)
        {
            // ② 중력 켜고 살짝 Impulse
            rb.useGravity = true;
            rb.velocity = hand.forward * 2f;

            // ③ 다음 프레임에 Rigidbody 자체를 제거
            //StartCoroutine(RemoveRigidbodyNextFrame());
        }
    }

    public void RemoveRigidbodyNextFrame()
    {
        
        if (rb != null)
        {
            Destroy(rb);
            rb = null;
        }
    }
}


