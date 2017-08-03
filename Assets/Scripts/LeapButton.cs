using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapButton : MonoBehaviour
{
    MainMenu refMM;
    public Animator anim;

    private void Awake()
    {
        refMM = FindObjectOfType<MainMenu>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (refMM.blinkingObject == this.gameObject)
            anim.SetBool("On", true);
        else
            anim.SetBool("On", false);       
    }
}