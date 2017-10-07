using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelConfigurator : MonoBehaviour {

    public GameObject iconBarObject;

    public Sprite penisIconBar;
    public Sprite vaginaIconBar;
    public Sprite assIconBar;

    public Sprite penisUI;
    public Sprite vaginaUI;
    public Sprite assUI;

    public Mesh penisBullet;
    public Mesh vaginaBullet;
    public Mesh assBullet;


    public GameObject playerPenis;
    public GameObject playerVagina;
    public GameObject playerAss;

    GameDataTransfer refGDT;


    // Use this for initialization
    void Start () {
        refGDT = FindObjectOfType<GameDataTransfer>();
        if (refGDT != null)
        {
            SetupChar();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetupChar()
    {
        int charIndex = refGDT.SelectedPlayer;

        switch (charIndex)
        {
            case 0:
                //Il personaggio scelto è il pene quindi attiviamo il corretto player, settiamo l'interfaccia corrispondente e i proiettili corretti in scena
                iconBarObject.GetComponent<Image>().sprite = penisIconBar;
                playerPenis.SetActive(true);
                playerVagina.SetActive(false);
                playerAss.SetActive(false);

                break;
            case 1:
                //Il personaggio scelto è la vagina quindi attiviamo il corretto player, settiamo l'interfaccia corrispondente e i proiettili corretti in scena
                iconBarObject.GetComponent<Image>().sprite = vaginaIconBar;
                playerVagina.SetActive(true);
                playerAss.SetActive(false);
                playerPenis.SetActive(false);

                break;
            case -1:
                //Il personaggio scelto è il culo quindi attiviamo il corretto player, settiamo l'interfaccia corrispondente e i proiettili corretti in scena
                iconBarObject.GetComponent<Image>().sprite = assIconBar;
                playerAss.SetActive(true);
                playerVagina.SetActive(false);
                playerPenis.SetActive(false);

                break;
        }
    }

}
