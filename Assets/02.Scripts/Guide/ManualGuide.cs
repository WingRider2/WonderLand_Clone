using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManualGuide : MonoBehaviour
{
    public TextMeshProUGUI controlTextUI;
    public float displayDuration = 4f;
    public List<string> controlMessage = new List<string>();

    private void Start()
    {
        if(controlTextUI != null && controlMessage.Count > 0)
        {
            StartCoroutine(GuideStart());
        }
    }

    private IEnumerator GuideStart()
    {
        yield return new WaitForSeconds(3f);

        if(controlTextUI != null && controlMessage.Count > 0)
        {
            StartCoroutine(ShowControlsRoutine());
        }
    }

    private IEnumerator ShowControlsRoutine()
    {
        controlTextUI.gameObject.SetActive(true);

        for(int i = 0; i < controlMessage.Count; i++)
        {
            controlTextUI.text = controlMessage[i];
            controlTextUI.gameObject.SetActive(true);

            yield return new WaitForSeconds(displayDuration);

            controlTextUI.gameObject.SetActive(false);
        }

        controlTextUI.gameObject.SetActive(false);

        GuideManager.Instance?.StartGuideSequence();
    }
}
