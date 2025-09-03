using System.Collections.Generic;
using UnityEngine;

public static class ItemDatabase
{
    private static Dictionary<string, ItemData> itemDict;

    public static void Load()
    {
        if (itemDict != null) return; // 이미 불러온 경우 생략

        TextAsset jsonFile = Resources.Load<TextAsset>("Items");

        if (jsonFile == null)
        {
            Debug.LogError("Item.json 파일을 Resources 폴더에서 찾을 수 없습니다!");
            return;
        }

        // JsonUtility는 배열을 직접 파싱하지 못하므로 포장
        string wrappedJson = "{\"items\":" + jsonFile.text + "}";
        ItemList list = JsonUtility.FromJson<ItemList>(wrappedJson);

        itemDict = new Dictionary<string, ItemData>();
        foreach (var item in list.items)
        {
            itemDict[item.name] = item;
        }
    }

    public static ItemData GetItemData(string name)
    {
        if (itemDict == null)
            Load();

        return itemDict.TryGetValue(name, out var data) ? data : null;
    }

}
