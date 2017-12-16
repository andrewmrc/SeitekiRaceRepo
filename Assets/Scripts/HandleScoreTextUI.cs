using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleScoreTextUI : MonoBehaviour {

    public float scoreValue;
    public bool malus = false;
	// Use this for initialization
	void OnEnable () {
        StartCoroutine(HandleMove());
    }

    public IEnumerator HandleMove ()
    {
        //Imposta lo score nel campo testo a seconda se è un bonus o un malus
        if (!malus)
        {
            this.GetComponent<Text>().text = "+ " + scoreValue.ToString();
        }
        else
        {
            this.GetComponent<Text>().text = "- " + scoreValue.ToString();
        }

        //fa muovere il text score verso l'alto
        while (this.transform.parent.localPosition.y <= 250f)
        {
            //Debug.Log("CallBonus");
            this.transform.parent.localPosition += new Vector3(0f, 5f, 0f);
            
            yield return null;
        }

        //Ricicla l'oggetto
        this.transform.parent.Recycle();
    }


}
