using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionPlayer : MonoBehaviour
{
    public Action<int, string> delCondom;
    public Action<int, string> delBat;
    public Action<int, string> delHandcuff;
    public Action<int, string> delMouth;
    public Action<int, string> delMuzzle;
    public Action<int, string> delUnderwear;
    public Action<int, string> delPill;
    public Action<bool> delGameOver;
    public Action<bool> delFinishLevel;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            delGameOver(true);
        }

        if (collision.gameObject.tag == "EndTrack")
        {
            delFinishLevel(true);
        }

        if (collision.gameObject.tag == "Condom")
        {
            delCondom(250, this.gameObject.name);
            Destroy(collision.gameObject);
            audioSource.Play();
        }

        if (collision.gameObject.tag == "Bat")
        {
            delBat(500, this.gameObject.name);
            Destroy(collision.gameObject);
            audioSource.Play();
        }

        if (collision.gameObject.tag == "Handcuff")
        {
            delHandcuff(300, this.gameObject.name);
            Destroy(collision.gameObject);
            audioSource.Play();
        }

        if (collision.gameObject.tag == "Mouth")
        {
            delMouth(600, this.gameObject.name);
            Destroy(collision.gameObject);
            audioSource.Play();
        }

        if (collision.gameObject.tag == "Muzzle")
        {
            delMuzzle(400, this.gameObject.name);
            Destroy(collision.gameObject);
            audioSource.Play();
        }

        if (collision.gameObject.tag == "Underwear")
        {
            delUnderwear(500, this.gameObject.name);
            Destroy(collision.gameObject);
            audioSource.Play();
        }

        if (collision.gameObject.tag == "Pill")
        {
            delPill(900, this.gameObject.name);
            Destroy(collision.gameObject);
            audioSource.Play();
        }

        if (collision.gameObject.tag == "Dick Pig")
        {
            delGameOver(true);
        }
    }
}
