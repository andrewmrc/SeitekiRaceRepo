using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionPlayer : MonoBehaviour
{
    //public Action<int, string> delCondom;
    //public Action<int, string> delBat;
    //public Action<int, string> delHandcuff;
    //public Action<int, string> delMouth;
    //public Action<int, string> delMuzzle;
    //public Action<int, string> delUnderwear;
    //public Action<int, string> delPill;
    public Action<bool> delGameOver;
    public Action<bool> delFinishLevel;
    public Action<int> delRecharge;

    public Action<int> delBonus;
    public Action<int> delMalus;

    public Action<int> delSpecialItem;

    private AudioSource audioSource;
    private GameManager refGM;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        refGM = FindObjectOfType<GameManager>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            delRecharge(10);
            Destroy(collision.gameObject);
            audioSource.Play();
        }

        if (collision.gameObject.tag == "Obstacle")
        {
            this.gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = false;
            this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
            delGameOver(true);
        }

        if (collision.gameObject.tag == "EndTrack")
        {
            this.gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = false;
            this.gameObject.transform.GetChild(3).gameObject.SetActive(true);
            delFinishLevel(true);
        }

        if (collision.gameObject.tag == "Condom")
        {
            //delCondom(250, this.gameObject.name);
            delBonus(2500);
            Destroy(collision.gameObject);
            //audioSource.Play();
        }

        if (collision.gameObject.tag == "Bat")
        {
            //delBat(500, this.gameObject.name);
            delMalus(5000);
            Destroy(collision.gameObject);
            //audioSource.Play();
        }

        if (collision.gameObject.tag == "Handcuff")
        {
            //delHandcuff(300, this.gameObject.name);
            delMalus(3000);
            Destroy(collision.gameObject);
            //audioSource.Play();
        }

        if (collision.gameObject.tag == "Mouth")
        {
            //delMouth(600, this.gameObject.name);
            delBonus(6000);
            Destroy(collision.gameObject);
            //audioSource.Play();
        }

        if (collision.gameObject.tag == "Muzzle")
        {
            //delMuzzle(400, this.gameObject.name);
            delMalus(4000);
            Destroy(collision.gameObject);
            //audioSource.Play();
        }

        if (collision.gameObject.tag == "Underwear")
        {
            //delUnderwear(500, this.gameObject.name);
            delBonus(5000);
            Destroy(collision.gameObject);
            //audioSource.Play();
        }

        if (collision.gameObject.tag == "Pill")
        {
            //delPill(900, this.gameObject.name);
            delBonus(9000);
            Destroy(collision.gameObject);
           //audioSource.Play();
        }

        if (collision.gameObject.tag == "Special")
        {
            delSpecialItem(1);
        }

        if (collision.gameObject.tag == "Dick Pig")
        {
            this.gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().enabled = false;
            this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
            delGameOver(true);
        }
    }
}
