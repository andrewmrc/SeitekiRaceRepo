﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public float health = 1f;
    private GameManager refGM;

    private void Awake()
    {
        refGM = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            health--;
            if (health == 0)
            {
                //Destroy(this.gameObject);
                refGM.FeedbackBonusCO(3000, false);
                this.gameObject.SetActive(false);
            }
            if (this.gameObject.activeSelf == true)
            {
                StartCoroutine(DecreaseHealth());
            }
        }
    }


    private IEnumerator DecreaseHealth()
    {
        yield return new WaitForSeconds(0.05f);
        this.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
    }


}
