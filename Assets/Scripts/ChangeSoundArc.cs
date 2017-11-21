using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeSoundArc : MonoBehaviour
{
    SoundManager refSM;
    public CollisionPlayer refCP;

    private void Start()
    {
        refSM = FindObjectOfType<SoundManager>();
        refCP = FindObjectOfType<CollisionPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            refSM.SetAudioClip();
            refSM.StartPlayAudioClip();

            //Controlla se tutti gli audio sono attivi e in caso chiama il metodo che attiva un bonus
            if(refSM.laneArray[1].GetComponent<AudioSource>().volume > 0){
                if (refSM.laneArray[2].GetComponent<AudioSource>().volume > 0)
                {
                    if (refSM.laneArray[3].GetComponent<AudioSource>().volume > 0)
                    {
                        if (refSM.laneArray[4].GetComponent<AudioSource>().volume > 0)
                        {
                            BonusRecharge();
                        }
                    }
                }
            }
        }
    }


    //Se tutte le clip audio sono attive (volume > 0) quando si passa sotto l'arco si riceve un bonus in punti e ricarica i colpi completamente
    public void BonusRecharge()
    {
        Debug.Log("BONUSRECHARGE!");
        refCP.delRecharge(10);
        refCP.delBonus(10000);
    }

}
