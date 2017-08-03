using System.Collections;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public byte speedBullet;
    public Action<int> delKillPig;
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
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Dick Pig")
        {
            delKillPig(100);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
