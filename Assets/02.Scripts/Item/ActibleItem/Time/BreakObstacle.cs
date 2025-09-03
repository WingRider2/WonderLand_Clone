using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakObstacle : MonoBehaviour
{

    public List<Transform> fragments;
    public Transform obstacle;
    public float restoreSpeed = 3f; 
    public float finishThreshold = 0.5f;
    public float rotationSpeed;

    private bool restoring = false;
    private Coroutine restoreRoutine;
    public  Action onRestoreComplete;
    private Rigidbody rigidbody;
    private void Awake()
    {
        foreach (Transform frag in this.transform)
        {
            fragments.Add(frag);
            frag.AddComponent<BreakObstacles>();
            frag.GetComponent<BreakObstacles>().obstacle = this.obstacle;
        }
    }

    public void Move(List<FragData> data)
    {
        if (!restoring)
        {
            // 시작
            
            restoring = true;
            restoreRoutine = StartCoroutine(MoveCoroutine(data));
        }
        else
        {
            // 중단
            restoring = false;
            StopCoroutine(restoreRoutine);
            restoreRoutine = null;
        }
    }
    private IEnumerator MoveCoroutine(List<FragData> Data)
    {
        // 필드 restoring 을 그대로 사용
        while (restoring)
        {
            bool anyLeft = false;

            for (int i = 0; i < fragments.Count; i++)
            {
                Transform frag = fragments[i];
                frag.GetComponent<Rigidbody>().isKinematic = true;
                FragData data = Data[i];

                
                // 보간 이동
                frag.localPosition = Vector3.MoveTowards(
                    frag.localPosition, data.pos, Time.deltaTime * restoreSpeed
                );
                frag.localRotation = Quaternion.RotateTowards(
                    frag.localRotation, data.rot, Time.deltaTime * restoreSpeed * rotationSpeed
                );
                frag.localScale = Vector3.MoveTowards(
                    frag.localScale, data.scale, Time.deltaTime * restoreSpeed
                );

                // 목표 근접 여부 체크
                float posErr = Vector3.Distance(frag.localPosition, data.pos);
                float rotErr = Quaternion.Angle(frag.localRotation, data.rot);
                float scaleErr = Vector3.Distance(frag.localScale, data.scale);

                if (posErr > finishThreshold ||
                    rotErr > finishThreshold * 100f ||
                    scaleErr > finishThreshold)
                {
                    anyLeft = true;
                }
                else
                {
                    // 오차 제거
                    frag.localPosition = data.pos;
                    frag.localRotation = data.rot;
                    frag.localScale = data.scale;
                }
            }

            // 더 이상 남은 조각이 없으면 자동으로 정지
            if (!anyLeft)
                restoring = false;

            yield return null;
        }

        restoreRoutine = null;

        onRestoreComplete?.Invoke();
        onRestoreComplete = null;
    }
}
