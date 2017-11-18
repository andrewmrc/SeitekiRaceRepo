﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleScoreTextUI : MonoBehaviour {

    public float scoreValue;
    public bool malus = false;
	// Use this for initialization
	void OnEnable () {
        StartCoroutine(HandleMove());
    }

    public IEnumerator HandleMove ()
    {
        //fa muovere il text score verso l'alto
        while (this.transform.localPosition.y <= 250f)
        {
            //Debug.Log("CallBonus");
            this.transform.localPosition += new Vector3(0f, 4f, 0f);
            if (!malus)
            {
                this.GetComponent<Text>().text = "+ " + scoreValue.ToString();
            } else
            {
                this.GetComponent<Text>().text = "- " + scoreValue.ToString();
            }
            yield return null;
        }
        this.Recycle();
    }


}