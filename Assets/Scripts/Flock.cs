using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Flock : MonoBehaviour
{
    enum State
    {
        swimming,
        back_step1,
        back_step2
    };

    State state = State.swimming;
    public float speed = 0.001f;
    public float rotateSpeed = 5.0f;
    public float neighborDistance = 3.0f;
    public Vector3 newGoal;
    public GameObject eggPrefab;
    Vector3 averageHeading;
    Vector3 averagePosition;
    DateTime enterTime;
    int life = 60;
    

    public bool turning = false;
    public bool turning2 = false;



    // Use this for initialization
    void Start ()
    {
        speed = Random.Range(0.8f, 2);
        enterTime = DateTime.Now;
    }

    // Update is called once per frame

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ground")
        {
            if (!turning)
                newGoal = this.transform.position - col.gameObject.transform.position;
            turning = true;
        }
        else if(col.gameObject.tag == "Hole" && state == State.back_step2)
        {
            List<GameObject> gos;
            gos = GlobalFlock.allFish;
            gos.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        turning = false;
    }


    void Update ()
    {
        if (this.transform.position.sqrMagnitude > 1600.0f)
        {
            if (!turning2)
                newGoal = this.transform.position;
            turning2 = true;
        }
        else
            turning2 = false;




        if (DateTime.Now.Subtract(enterTime).TotalSeconds > life && state != State.back_step2 && state != State.back_step1)
        {
            state = State.back_step1;
            if(Random.Range(0,10) < 1)
                Instantiate(eggPrefab, transform.position, Quaternion.identity);
        }
        if (turning || turning2)
        {
            Vector3 direction = newGoal - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                          Quaternion.LookRotation(direction),
                                                          rotateSpeed * Time.deltaTime);
            }
            speed = Random.Range(0.8f, 2);
        }
        else if(state == State.swimming)
        {
            if (Random.Range(0, 10) < 1)
                ApplyRule();
        }
        else if(state == State.back_step1 || state == State.back_step2)
        {
            if (Random.Range(0, 10) < 1)
                GoBack();
        }
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }

    public void ApplyRule()
    {
        List<GameObject> gos;
        gos = GlobalFlock.allFish;

        Vector3 vCentral = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;
        Vector3 vAvoidShark = Vector3.zero;
        float gSpeed = 0.1f;
        float accelerate = 0;

        Vector3 goalPos = GlobalFlock.goalPos;

        float dist;
        int groupSize = 0;

        foreach(GameObject go in gos)
        {
            if(go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if(dist <= neighborDistance)
                {
                    vCentral += go.transform.position;
                    groupSize++;

                    if(dist <= 2.0f)
                    {
                        vAvoid = vAvoid + (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        //avoid shark
        /*
        dist = Vector3.Distance(GlobalFlock.shark.transform.position, this.transform.position);
        if (dist <= 7.0f)
        {
            vAvoidShark = vAvoidShark + (this.transform.position - GlobalFlock.shark.transform.position)*2;
            accelerate = 5 / dist;
        }
        else
        {
            if (speed > 1)
                speed -= 1.0f;
        }
        */


        if (groupSize > 0)
        {
            vCentral = vCentral / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize + accelerate;

            Vector3 direction = (vCentral + vAvoid + vAvoidShark) - this.transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(direction),
                                                      rotateSpeed*Time.deltaTime);
            }

        }
    }

    public void GoBack()
    {
        Vector3 target = Vector3.zero + new Vector3(0 ,2.5f, 0);
        float height = 30.0f;

        if (state == State.back_step1)
        {
            target = (this.gameObject.transform.position + new Vector3(0, height, 0)) / 2;
            if(this.gameObject.transform.position.y > height)
                state = State.back_step2;
        }
            
        Vector3 direction = target - this.transform.position;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(direction),
                                                  rotateSpeed * Time.deltaTime);
        }
    }
}
