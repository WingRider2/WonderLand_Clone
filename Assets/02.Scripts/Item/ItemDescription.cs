using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    public string Name;
    public string Description;

    public string getItemDescription()
    {
        return Name + "\n" + Description;
    }
}
