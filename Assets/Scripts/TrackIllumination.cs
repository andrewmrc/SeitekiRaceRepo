using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackIllumination : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ChangeIllumination(0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ChangeIllumination(1);
        }
    }

    public void ChangeIllumination(int _value)
    {
        this.GetComponent<MeshRenderer>().material.SetFloat("_Metallic", _value);
    }
}
