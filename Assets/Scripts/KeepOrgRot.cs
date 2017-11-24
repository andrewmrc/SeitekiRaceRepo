using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOrgRot : MonoBehaviour {

    private Quaternion OrgRotation;
    private Vector3 OrgPosition;

	// Use this for initialization
	void Start () {
        OrgRotation = transform.rotation;
        OrgPosition = transform.parent.transform.position - transform.position;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        transform.rotation = OrgRotation;
        transform.position = transform.parent.position - OrgPosition;
    }

}
