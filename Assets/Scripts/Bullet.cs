using System.Collections;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public byte speedBullet;
    public Action<GameObject> delKillPig;
    private void Awake()
    {
        StartCoroutine(DestroyByTimeCO());
    }

    private void Update ()
    {
        this.transform.Translate(this.transform.forward * (-speedBullet) * Time.deltaTime);
    }

    private IEnumerator DestroyByTimeCO()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.Recycle();
        //Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            //Destroy(gameObject);
            gameObject.Recycle();
        }

        if (collision.gameObject.tag == "Dick Pig")
        {
            delKillPig(collision.gameObject);

            //Destroy(collision.gameObject);
            gameObject.Recycle();
            //Destroy(gameObject);
        }
    }



}
