using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public GameObject fishPrefab;
    public GameObject sharkPrefab;

    private GameObject fishManager;
    private GlobalFlock globalFlock;
    private List<GameObject> allFish;
    private float edge = 0.5f;

    // Use this for initialization
    void Start ()
    {
        fishManager = GameObject.Find("FishManager");
        globalFlock = fishManager.GetComponent<GlobalFlock>();
        allFish = GlobalFlock.allFish;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Random.Range(0, 10000) < 100)
        //if(Input.GetKeyDown(KeyCode.X))
        {
            Vector3 pos = new Vector3(Random.Range(-edge, edge),
                                      1.0f,
                                      Random.Range(-edge, edge));
            Quaternion rotation = Quaternion.Euler(Random.Range(-120, -60), 0, 0);
            allFish.Add(Instantiate(fishPrefab, pos, rotation) as GameObject);
        }
    }
}
