using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstaclePos : MonoBehaviour
{
    public List<FragData> originalData;

    private void Awake()
    {
        originalData = new List<FragData>();
        foreach (Transform frag in this.transform)
        {
            originalData.Add(new FragData
            {
                pos = frag.localPosition,
                rot = frag.localRotation,
                scale = frag.localScale
            });
        }
    }
}
