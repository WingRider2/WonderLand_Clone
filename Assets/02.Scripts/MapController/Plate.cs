using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField] private CubeController cubeController;

    [Header("Local 방향 기준 회전축 설정")] 
    [SerializeField] private Vector3 localRotateDirection;
    //z값 회전으로 오브젝트 배치, 방향은 x,y값에 +-180
    [SerializeField] private float angle = 90f;

    private void Start()
    {
        cubeController = transform.root.GetComponent<CubeController>();
        if (cubeController == null)
        {
            Debug.LogWarning("CubeController가 없습니다.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // plate 기준 로컬 방향을 월드 방향으로 변환
            Vector3 worldAxis = transform.TransformDirection(localRotateDirection);
            cubeController.RotateCube(worldAxis * angle);
        }
    }
}