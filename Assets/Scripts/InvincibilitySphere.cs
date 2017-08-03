using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilitySphere : MonoBehaviour
{
    private MeshRenderer mr;
    private SphereCollider sc;
    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        sc = GetComponent<SphereCollider>();
    }

    public IEnumerator IncreaseSizeCO()
    {
        DisableGraphicSphere(true);
        while (this.transform.localScale != new Vector3(14, 14, 14))
        {
            this.transform.localScale += Vector3.one;
            yield return null;
        }

        while (this.transform.localScale != Vector3.one)
        {
            this.transform.localScale -= Vector3.one;
            yield return new WaitForSeconds(.3f);
        }
        DisableGraphicSphere(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                Destroy(collision.gameObject);
                break;
            case "Condom":
                Destroy(collision.gameObject);
                break;
            case "Bat":
                Destroy(collision.gameObject);
                break;
            case "Dick Pig":
                Destroy(collision.gameObject);
                break;
            case "Handcuff":
                Destroy(collision.gameObject);
                break;
            case "Mouth":
                Destroy(collision.gameObject);
                break;
            case "Muzzle":
                Destroy(collision.gameObject);
                break;
            case "Underwear":
                Destroy(collision.gameObject);
                break;
            case "Pill":
                Destroy(collision.gameObject);
                break;
        }
    }

    public void DisableGraphicSphere(bool _on)
    {
        mr.enabled = _on;
        sc.enabled = _on;
    }
}