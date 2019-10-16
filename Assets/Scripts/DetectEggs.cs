using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEggs : MonoBehaviour
{
    public bool AnyEggs = false;
    public GameObject egg;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Egg" && AnyEggs == false)
        {
            egg = col.gameObject;
            AnyEggs = true;
            Debug.Log("find");
        }
    }
}
