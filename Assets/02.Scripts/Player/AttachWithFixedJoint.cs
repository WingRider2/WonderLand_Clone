using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AttachWithFixedJoint : MonoBehaviour
{
    [Header("붙일 대상 Rigidbody")]
    public Rigidbody targetBody;

    [Header("파손 한계 (선택)")]
    public float breakForce = Mathf.Infinity;
    public float breakTorque = Mathf.Infinity;

    void Start()
    {
        // 1) FixedJoint 추가
        FixedJoint fj = gameObject.AddComponent<FixedJoint>();

        // 2) 연결할 Rigidbody 지정
        fj.connectedBody = targetBody;

        // 3) 파손 설정 (원하지 않으면 Infinity)
        fj.breakForce = breakForce;
        fj.breakTorque = breakTorque;

        // 4) 두 물체 간 충돌을 허용하려면 true
        fj.enableCollision = false;
    }
}
