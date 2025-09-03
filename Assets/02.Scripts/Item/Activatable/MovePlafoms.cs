using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlafoms : MonoBehaviour
{
    public List<Transform> fragments;

    private void Awake()
    {
        foreach (Transform frag in this.transform)
        {
            fragments.Add(frag);
        }

    }
    public void movePlafom()
    {
        foreach (var item in fragments)
        {
            StartCoroutine(item.GetComponent<MovePlafom>().Move());
        }
    }
}
