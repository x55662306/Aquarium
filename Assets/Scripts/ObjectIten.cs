using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectIten : MonoBehaviour {

    public int id;
    public string name;
    public int count;

    public bool isChecked;

    // Use this for initialization
    void Start()
    {
        isChecked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isChecked)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
        isChecked = false;
    }
}
