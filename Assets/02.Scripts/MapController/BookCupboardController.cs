using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class BookCupboardController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform frag in this.transform)
        {
            Destroy(frag.GetComponent<Rigidbody>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
