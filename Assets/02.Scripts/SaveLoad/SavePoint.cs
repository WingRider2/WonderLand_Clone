using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private bool hasSaved = false;

    private void OnTriggerEnter(Collider other)
    {
        if(hasSaved)
        {
            return;
        }

        if(other.CompareTag("Player"))
        {
            hasSaved = true;

            SaveManager.Instance.EnableSave();

            SaveManager.Instance.SavePlayerPosition(transform.position);

            RoomRotator roomRotator = FindObjectOfType<RoomRotator>();

            if(roomRotator != null)
            {
                SaveManager.Instance.SaveRoomRotation(roomRotator.transform.rotation.eulerAngles);
            }

            SaveUIManager.Instance.ShowSaveMessage("위치가 저장되었습니다");
        }
    }
}
