using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinishDoor : MonoBehaviour
{
    public GameObject clearPanel;

    private void OnTriggerEnter(Collider other)
    {
        SoundManager.Instance.StopBGM();
        clearPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
