using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public float health = 1f;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            health--;
            if (health == 0)
            {
                Destroy(this.gameObject);
            }
            StartCoroutine(DecreaseHealth());
        }
    }


    private IEnumerator DecreaseHealth()
    {
        yield return new WaitForSeconds(0.1f);
        this.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
    }


}
