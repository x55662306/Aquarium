using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFlock : MonoBehaviour {

    public GameObject fishPrefab;
    public GameObject sharkPrefab;
    public GameObject goalPrefab;

    static int fishNum = 50 ;
    public static List<GameObject> allFish = new List<GameObject>();
    public static GameObject shark;
    public static Vector3 goalPos = Vector3.zero;

    public static float edge=35f;


    // Use this for initialization
    void Start()
    {
        /*for (int i = 0; i < fishNum; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-edge, edge),
                                      Random.Range(-edge, edge),
                                      Random.Range(-edge, edge));
            allFish[i] = Instantiate(fishPrefab, pos, Quaternion.identity) as GameObject;
        }*/

        Vector3 sharkPos = new Vector3(Random.Range(-edge, edge),
                                      Random.Range(-edge, edge),
                                      Random.Range(-edge, edge));
        //shark = Instantiate(sharkPrefab, sharkPos, Quaternion.identity) as GameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Random.Range(0, 10000) < 25)
        {
            goalPos = new Vector3(Random.Range(-edge, edge),
                                  Random.Range(5, edge),
                                  Random.Range(-edge, edge));
            goalPrefab.transform.position = goalPos;
        }
        
    }
}
