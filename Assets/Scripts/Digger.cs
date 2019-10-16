using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Digger : MonoBehaviour
{
    enum State
    {
        Detect,
        GoToTarget,
        Dig
    };

    DetectEggs detectScript;
    State state = State.Detect;
    GameObject egg;
    private NavMeshAgent navMeshAgent;
    Vector3 target;
    Vector3 Look_pos;//中途目標
    private float DigTime; 

    public GameObject test;

    // Use this for initialization
    void Start ()
    {
        detectScript = this.gameObject.GetComponentInChildren<DetectEggs>();
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        target = new Vector3(Random.Range(-80, 80), 0, Random.Range(-80, 80));
	}

    // Update is called once per frame
    void Update()
    {
        if (state == State.Detect)
        {
            if (detectScript.AnyEggs == true)
            {
                egg = detectScript.egg;
                state = State.GoToTarget;
                detectScript.AnyEggs = false;
            }
            if (Vector3.Distance(transform.position, target) < 2)
            {
                target = new Vector3(Random.Range(-80, 80), 0, Random.Range(-80, 80));
                test.transform.position = target;
            }
            navMeshAgent.destination = target;
            Look_pos = navMeshAgent.steeringTarget;
        }
        else if (state == State.GoToTarget)
        {
            navMeshAgent.destination = egg.transform.position;
            Look_pos = navMeshAgent.steeringTarget;
            if (Vector3.Distance(egg.transform.position, transform.position) < 1)
            {
                DigTime = 0;
                state = State.Dig;
                Debug.Log("find");
            }
        }
        else if (state == State.Dig)
        {
            DigTime += Time.deltaTime;
            if(DigTime > 3)
            {
                egg.GetComponent<Renderer>().enabled = true;
                Debug.Log(egg.GetComponent<Renderer>().enabled);
                state = State.Detect;
            }
        }
    }
}
