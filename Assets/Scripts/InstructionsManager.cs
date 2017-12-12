using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsManager : MonoBehaviour {

    public int pageCounter;
    public GameObject page1, page2, page3, rightArrow, leftArrow;

	public void ChangePage(int _value)
    {
        pageCounter += _value;
        switch (pageCounter){
            case 0:
                leftArrow.SetActive(false);
                page1.SetActive(true);
                page2.SetActive(false);
                break;

            case 1:
                leftArrow.SetActive(true);
                rightArrow.SetActive(true);
                page1.SetActive(false);
                page2.SetActive(true);
                page3.SetActive(false);
                break;

            case 2:
                rightArrow.SetActive(false);
                page2.SetActive(false);
                page3.SetActive(true);
                break;
        }
    }

    public void ResetInstructionsStatus ()
    {
        pageCounter = 0;
        leftArrow.SetActive(false);
        rightArrow.SetActive(true);
        page1.SetActive(true);
        page2.SetActive(false);
        page3.SetActive(false);
    }

}
