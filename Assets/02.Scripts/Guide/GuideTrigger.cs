using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideTrigger : MonoBehaviour
{
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(hasTriggered)
        {
            return;
        }

        if(other.CompareTag("Player"))
        {
            hasTriggered = true;

            GuideManager.Instance.ShowNextGuide();
        }

    }
}
