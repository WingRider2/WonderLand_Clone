using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFricker : MonoBehaviour
{
    public Light targetLight;
    public float minIntensity = 0.8f;
    public float maxIntensity = 2.2f;
    public float smoothSpeed = 2f;              // 조명이 부드럽게 변하는 속도
    public float flickerInterval = 0.5f;        // 다음 목표 밝기를 바꾸는 시간 간격

    private float targetIntensity;
    private float timer = 0f;

    private void Start()
    {
        if (targetLight == null)
        {
            targetLight = GetComponent<Light>();
        }

        // 초기 목표 밝기 설정
        targetLight.intensity = Random.Range(minIntensity, maxIntensity);
        targetIntensity = targetLight.intensity;
    }

    private void Update()
    {
        // 현재 밝기에서 목표 밝기로 부드럽게 이동
        targetLight.intensity = Mathf.Lerp(targetLight.intensity, targetIntensity, Time.deltaTime * smoothSpeed);

        timer += Time.deltaTime;

        // 일정 확률로 새로운 목표 밝기를 지정
        if (timer >= flickerInterval)
        {
            targetIntensity = Random.Range(minIntensity, maxIntensity);
            timer = 0f;
        }
    }
}