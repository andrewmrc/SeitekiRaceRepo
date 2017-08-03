using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPathScript : MonoBehaviour
{
    public EditorPathScript pathToFollow;

    public int currentWayPointID = 0;
    public float speed;
    private float reachDistance = 1.0f;
    public float rotationSpeed = 5.0f;
    public string pathName;

    Vector3 last_position, current_position;

    private void Start()
    {
        //pathToFollow = GameObject.Find(pathName).GetComponent<EditorPathScript>();
        last_position = transform.position;
    }

    private void Update()
    {
        float distance = Vector3.Distance(pathToFollow.pathList[currentWayPointID].position, this.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, pathToFollow.pathList[currentWayPointID].position, Time.deltaTime * speed);

        Quaternion rotation = Quaternion.LookRotation(pathToFollow.pathList[currentWayPointID].position - this.transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        if (distance <= reachDistance)
        {
            currentWayPointID++;
        }

        if (currentWayPointID >= pathToFollow.pathList.Count)
        {
            currentWayPointID = 0;
        }

    }

}
