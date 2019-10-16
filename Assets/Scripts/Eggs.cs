using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eggs : MonoBehaviour
{
    public Renderer rend;

    public bool isHide = false;


    // Use this for initialization
    void Start ()
    {
        rend = GetComponent<Renderer>();
	}

    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Ground" && isHide == false)
        {
            isHide = true;
            rend.enabled = false;
        }
    }
}
