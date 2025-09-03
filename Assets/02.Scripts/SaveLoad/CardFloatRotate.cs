using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFloatRotate : MonoBehaviour
{
    public float rotateAngle = 10f;         //  회전 최대 각도
    public float rotateSpeed = 2f;          //  회전 속도

    public float floatHeight = 0.2f;        //  위아래 흔들림 높이
    public float speed = 1f;                //  위아래 흔들림 속도

    private Vector3 initialPosition;

    private float initialXRotation;
    private float initialZRotation;
    private float initialYRotation;

    private void Start()
    {
        initialPosition = transform.localPosition;

        //  회전 초기값 저장
        Vector3 initialRot = transform.rotation.eulerAngles;
        initialXRotation = initialRot.x;
        initialYRotation = initialRot.y;
        initialZRotation = initialRot.z;
    }

    private void Update()
    {
        //   부드러운 x/z 축 회전 
        float xRotation = Mathf.Sin(Time.time * rotateSpeed) * rotateAngle;
        float zRotation = Mathf.Cos(Time.time * rotateSpeed) * rotateAngle;
        transform.localRotation = Quaternion.Euler(initialXRotation + xRotation, initialYRotation, initialZRotation + zRotation);

        //  위아래 흔들림 위치 변화
        float yOffset = Mathf.Sin(Time.time * speed) * floatHeight;
        transform.localPosition = new Vector3(initialPosition.x, initialPosition.y + yOffset, initialPosition.z);
    }
}
