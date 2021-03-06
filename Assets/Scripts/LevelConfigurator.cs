﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelConfigurator : MonoBehaviour {

    public GameObject iconBarObject;

    public Sprite penisIconBar;
    public Sprite vaginaIconBar;
    public Sprite assIconBar;

    public GameObject charUIFull;
    public GameObject charUIOutline;

    public Sprite penisUI;
    public Sprite penisUIFull;

    public Sprite vaginaUI;
    public Sprite vaginaUIFull;

    public Sprite assUI;
    public Sprite assUIFull;


    public GameObject penisRecharge;
    public GameObject vaginaRecharge;
    public GameObject assRecharge;


    public GameObject playerPenis;
    public GameObject playerVagina;
    public GameObject playerAss;


    public List<GameObject> bulletInScene = new List<GameObject>();

    GameDataTransfer refGDT;


    // Use this for initialization
    void Awake () {
        refGDT = FindObjectOfType<GameDataTransfer>();
        if (refGDT != null)
        {
            //Trova tutti i bullet in scena precedentemente posizionati
            bulletInScene.AddRange(GameObject.FindGameObjectsWithTag("Bullet"));
            SetupChar();
        }
    }


    public void SetupChar()
    {
        Debug.Log("SETUP CHAR!");
        int charIndex = refGDT.SelectedPlayer;

        switch (charIndex)
        {
            case 0:
                //Il personaggio scelto è il pene quindi attiviamo il corretto player, settiamo l'interfaccia corrispondente e i proiettili corretti in scena
                iconBarObject.GetComponent<Image>().sprite = penisIconBar;
                playerPenis.SetActive(true);
                playerVagina.SetActive(false);
                playerAss.SetActive(false);

                charUIFull.GetComponent<Image>().sprite = penisUIFull;
                charUIOutline.GetComponent<Image>().sprite = penisUI;

                break;
            case 1:
                //Il personaggio scelto è la vagina quindi attiviamo il corretto player, settiamo l'interfaccia corrispondente e i proiettili corretti in scena
                iconBarObject.GetComponent<Image>().sprite = vaginaIconBar;
                playerVagina.SetActive(true);
                playerAss.SetActive(false);
                playerPenis.SetActive(false);

                for (int i = 0; i < bulletInScene.Count; i++)
                {
                    GameObject newRechargeItem = Instantiate(vaginaRecharge, new Vector3(bulletInScene[i].transform.position.x, vaginaRecharge.transform.position.y, bulletInScene[i].transform.position.z), Quaternion.Euler(-90, 180, 0));
                    bulletInScene[i].SetActive(false);
                    //Debug.Log("INSTANTIATE NEW RECHARGE ITEM: " + i);
                }

                charUIFull.GetComponent<Image>().sprite = vaginaUIFull;
                charUIOutline.GetComponent<Image>().sprite = vaginaUI;

                Debug.Log("VAGINA SETUP!");

                break;
            case -1:
                //Il personaggio scelto è il culo quindi attiviamo il corretto player, settiamo l'interfaccia corrispondente e i proiettili corretti in scena
                iconBarObject.GetComponent<Image>().sprite = assIconBar;
                iconBarObject.GetComponent<RectTransform>().sizeDelta = new Vector2(40f, 30f);
                playerAss.SetActive(true);
                playerVagina.SetActive(false);
                playerPenis.SetActive(false);

                for (int i = 0; i < bulletInScene.Count; i++)
                {
                    GameObject newRechargeItem = Instantiate(assRecharge, new Vector3(bulletInScene[i].transform.position.x, assRecharge.transform.position.y, bulletInScene[i].transform.position.z), Quaternion.Euler(0, 0, -90));
                    bulletInScene[i].SetActive(false);
                    //Debug.Log("INSTANTIATE NEW RECHARGE ITEM: " + i);
                }

                charUIFull.GetComponent<Image>().sprite = assUIFull;
                charUIOutline.GetComponent<Image>().sprite = assUI;

                Debug.Log("ASS SETUP!");

                break;
        }
    }

}
