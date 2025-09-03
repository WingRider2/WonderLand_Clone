using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectValues
{
    public bool isThrowable;
    public float throwForce;
    public float[] smallScale;
    public float[] largeScale;
    public float scaleDuration;
}

[System.Serializable]
public class ItemData
{
    public int id;
    public string name;
    public string type;
    public string effect;
    public string prefab;
    public Sprite icon;
    public EffectValues effectValues;
}

