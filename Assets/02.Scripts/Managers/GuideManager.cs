using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuideManager : Singleton<GuideManager>
{
    public TextMeshProUGUI guideTextUI;
    public float displayDuration = 4f;
    public List<string> guideTexts = new List<string>();

    private int currentIndex = 0;
    private Coroutine currentCoroutine;
    private bool isDisplaying = false;

    private void Start()
    {
        if (guideTextUI != null)
        {
            guideTextUI.gameObject.SetActive(false);
        }
    }

    public void StartGuideSequence()
    {
        if (guideTexts.Count == 0) return;
        currentIndex = 0;
        StartNextGuide();  // 코루틴 기반 자동 진행
    }

    public void ShowNextGuide()
    {
        // 외부에서 ShowNextGuide()를 중복 호출하는 걸 완전히 차단
        if (isDisplaying || currentIndex >= guideTexts.Count - 1)
        {
            return;
        }

        currentIndex++;
        StartNextGuide();
    }

    private void StartNextGuide()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(DelayedGuideRoutine(currentIndex));
    }

    private IEnumerator DelayedGuideRoutine(int index)
    {
        isDisplaying = true;

        guideTextUI.gameObject.SetActive(true);
        guideTextUI.text = guideTexts[index];

        yield return new WaitForSeconds(displayDuration);

        guideTextUI.gameObject.SetActive(false);
        isDisplaying = false;

        if(index == 2 && guideTexts.Count > 2)
        {
            yield return new WaitForSeconds(2f);
            currentIndex = 3;
            StartNextGuide();
        }
    }

    private void HideGuide()
    {
        guideTextUI.text = "";
        guideTextUI.gameObject.SetActive(false);
        isDisplaying = false;
    }
}
