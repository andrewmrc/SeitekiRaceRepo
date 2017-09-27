﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionBar : MonoBehaviour {

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        float newZPos = (-player.transform.position.z / 5);
        this.gameObject.transform.GetChild(1).GetComponent<RectTransform>().localPosition = new Vector2(newZPos,0);
	}

}
