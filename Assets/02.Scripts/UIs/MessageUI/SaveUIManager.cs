using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveUIManager : Singleton<SaveUIManager>
{
    public TextMeshProUGUI saveText;
    public float displayDuration = 2f;

    private Coroutine currentCoroutine;

    private void Start()
    {
        if(saveText != null)
        {
            saveText.gameObject.SetActive(false);
        }
    }


    public void ShowSaveMessage(string message)
    {
        if(saveText == null)
        {
            return;
        }

        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(DisplayMessageRoutine(message));
    }

    private IEnumerator DisplayMessageRoutine(string message)
    {
        saveText.text = message;
        saveText.gameObject.SetActive(true);

        yield return new WaitForSeconds(displayDuration);

        saveText.gameObject.SetActive(false);
    }

    public void ShowRespawnNotAvailableMessage()
    {
        ShowSaveMessage("세이브 포인트와 충돌할 때 사용 가능합니다");
    }
}
