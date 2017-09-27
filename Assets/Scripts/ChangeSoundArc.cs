using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeSoundArc : MonoBehaviour
{
    SoundManager refSM;
    public Action<int> delRecharge;
    public Action<int> delBonus;

    private void Awake()
    {
        refSM = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            refSM.SetAudioClip();

            //Controlla se tutti gli audio sono attivi e in caso chiama il metodo che attiva un bonus
            if(refSM.laneArray[1].GetComponent<AudioSource>().volume == 1){
                if (refSM.laneArray[2].GetComponent<AudioSource>().volume == 1)
                {
                    if (refSM.laneArray[3].GetComponent<AudioSource>().volume == 1)
                    {
                        if (refSM.laneArray[4].GetComponent<AudioSource>().volume == 1)
                        {
                            BonusRecharge();
                        }
                    }
                }
            }
        }
    }


    //Se tutte le clip audio sono attive (volume a 1) quando si passa sotto l'arco si riceve un bonus in punti e ricarica i colpi completamente
    public void BonusRecharge()
    {
        delRecharge(10);
        delBonus(1000);
    }

}
