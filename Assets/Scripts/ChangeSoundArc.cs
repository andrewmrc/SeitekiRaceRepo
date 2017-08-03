using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSoundArc : MonoBehaviour
{
    SoundManager refSM;

    private void Awake()
    {
        refSM = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            refSM.SetAudioClip();
        }
    }
}
