using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour {

    public float speed = 0.001f;
    public float rotateRange = 5.0f;
    public float rotateSpeed = 4.0f;
    private bool turning = false;

    // Use this for initialization
    void Start ()
    {
        speed = Random.Range(0.5f, 2.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(this.transform.position, Vector3.zero) >= GlobalFlock.edge)
            turning = true;
        else
            turning = false;

        if (turning)
        {
            Vector3 direction = Vector3.zero - this.transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(direction),
                                                      rotateSpeed * Time.deltaTime);
            speed = Random.Range(0.5f, 1);
        }
        else if (Random.Range(0, 5) < 1)
            ApplyRule();
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void ApplyRule()
    {
        List<GameObject> gos;
        gos = GlobalFlock.allFish;

        Vector3 goalPos = Vector3.zero;


        float dist, preyDist = float.MaxValue;
        foreach(GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if (dist <= preyDist)
                {
                    goalPos = go.transform.position;
                }
            }
        }

        Vector3 direction = goalPos - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(direction),
                                                      rotateSpeed * Time.deltaTime);
    }
}
