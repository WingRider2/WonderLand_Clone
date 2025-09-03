using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;

public class SaveManager : Singleton<SaveManager>
{
    public SaveData saveData = new SaveData();
    private string path;

    private Vector3 lastSavedPosition;                      //  최근 저장 위치 추가
    public bool isSaveAvailable = false;                    //  세이브 포인트와 충돌 전까지 false
    private void Awake()
    {
        path = Path.Combine(Application.persistentDataPath, "save.json");
        LoadData();
        ResetSaveData();                       
    }

    public void SavePlayerPosition(Vector3 position)
    {
        //  이전과 동일한 위치면 저장하지 않는다.
        if (!isSaveAvailable || position == lastSavedPosition)
        {
            return;
        }

        saveData.savePoint = position;
        lastSavedPosition = position;
        SaveDataToFile();
    }

    public void SaveRoomRotation(Vector3 eulerAngles)
    {
        if(!isSaveAvailable)
        {
            return;
        }

        saveData.roomRotationEuler = eulerAngles;
        SaveDataToFile();
    }

    public Vector3 GetSavedPosition()
    {
        //return saveData.savePoint;
        return isSaveAvailable ? saveData.savePoint : Vector3.zero;
    }

    public Vector3 GetSavedRoomRotationEuler()
    {
        //return saveData.roomRotationEuler;
        return isSaveAvailable ? saveData.roomRotationEuler : Vector3.zero;
    }

    public void SaveDataToFile()
    {
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);
    }

    public void LoadData()
    {
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        else
        {
            saveData = new SaveData();
        }
    }

    //  이전 세이브 위치 제거
    private void ResetSaveData()
    {
        saveData.savePoint = Vector3.zero;
        saveData.roomRotationEuler = Vector3.zero;
        SaveDataToFile();
        isSaveAvailable = false;
    }

    //  세이브 포인트 트리거 충돌 시, 외부에서 호출
    public void EnableSave()
    {
        isSaveAvailable = true;
    }
}
