using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public Transform[] laneArray = new Transform[4];
    public Transform[] objectArray;

    public int numObjectToSpawn;
    int initialThreshold;
    int distanceThreshold;

    // Use this for initialization
    void Start ()
    {
        //distanceThreshold = initialThreshold;
        //if(numObjectToSpawn == 0)
        //{
        //    if(initialThreshold < 0)
        //    {
        //        numObjectToSpawn = ((int)laneArray[0].localScale.z / initialThreshold) * -1;
        //    }
               
        //} else if (initialThreshold == 0)
        //{
        //    initialThreshold = ((int)laneArray[0].localScale.z / numObjectToSpawn);
        //}

        //numObjectToSpawn = ((int)laneArray[0].localScale.z / initialThreshold) * -1;
        initialThreshold = ((int)laneArray[0].localScale.z / numObjectToSpawn);


        for (int i = 0; i < laneArray.Length; i++)
        {
            distanceThreshold = initialThreshold;
            for (int j = 0; j < numObjectToSpawn; j++)
            {
                GameObject objectToSpawn = Instantiate(objectArray[Random.Range(0, objectArray.Length)].gameObject);
                objectToSpawn.transform.position = new Vector3(laneArray[i].transform.position.x, objectToSpawn.transform.position.y, Random.Range(distanceThreshold-100, distanceThreshold+100));
                distanceThreshold -= initialThreshold;
                Debug.Log(distanceThreshold);
            }
  
        }

       
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
