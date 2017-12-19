using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingText, playerPool, circuitPool, charPanel, circuitPanel, charSelectText, circuitSelectText;
    int levelIndex;

    // Delegato chiamato quando si sceglie un determinato tracciato
    public Action<string> delChoosedTrack;
    public Action<string> delChoosedPlayer;

    [SerializeField]
    Image fadeImage;

    public GameObject level2Button;
    public GameObject level3Button;
    public GameObject level4Button;
    public GameObject level5Button;
    public GameObject level6Button;

    public AudioClip penisButtonAudioClip, assButtonAudioClip, vaginaButtonAudioClip, genericButtonAudioClip;

    public void Start()
    {
        StartCoroutine(fadeIn());
        SetupLevels();
        CheckLevels();
    }

    //private void Update()
    //{
    //    CheckMousePosition();

    //    if (Input.GetMouseButtonDown(0) && blinkingObject != null)
    //    {
    //        // Se é il player lo fa scomparire, non lo fa distruggere e fa apparire i circuiti
    //        if (blinkingObject.CompareTag("Player"))
    //        {
    //            delChoosedPlayer(blinkingObject.name);
    //            playerPool.SetActive(false);
    //            Debug.Log(blinkingObject.name);
    //            StartFadeOut();
    //            //circuitPool.SetActive(true);
    //        }
    //        // cliccando su un circuito lancio il delegato e cambio scena
    //        //else if (blinkingObject.CompareTag("Circuit"))
    //        //{
    //        //    Debug.Log(blinkingObject.name);
    //        //    delChoosedTrack(blinkingObject.name);
    //        //    SceneManager.LoadScene(blinkingObject.name);
    //        //}
    //    }
    //}

    //// Serve per far fare l'animazione all'oggetto su cui é sopra il mouse
    //private void CheckMousePosition()
    //{
    //    RaycastHit hit;
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    if (Physics.Raycast(ray, out hit, 100))
    //    {
    //        Debug.DrawLine(ray.origin, hit.point, Color.blue);
    //        blinkingObject = hit.transform.gameObject;
    //    }
    //    else
    //    {
    //        blinkingObject = null;
    //    }
    //}


    public void CharSelected (int charIndex)
    {
        //salvarsi quale personaggio è stato scelto
        GameDataTransfer dataObject = FindObjectOfType<GameDataTransfer>();
        dataObject.SelectedPlayer = charIndex;

        //Fa partire un audio diverso a seconda del personaggio scelto
        switch (charIndex)
        {
            case 0:
                this.gameObject.GetComponent<AudioSource>().PlayOneShot(penisButtonAudioClip);
                break;
            case 1:
                this.gameObject.GetComponent<AudioSource>().PlayOneShot(vaginaButtonAudioClip);
                break;
            case -1:
                this.gameObject.GetComponent<AudioSource>().PlayOneShot(assButtonAudioClip);
                break;

        }

        //Configura le schermate successive
        charPanel.SetActive(false);
        circuitPanel.SetActive(true);
        charSelectText.SetActive(false);
        circuitSelectText.SetActive(true);
    }


    public void CircuitSelected(int levelChosen)
    {
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(genericButtonAudioClip);
        levelIndex = levelChosen;

        StartFadeOut();

    }


    public void PlayGenericAudioButton()
    {
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(genericButtonAudioClip);
    }

    private void StartFadeOut()
    {
        //devo rimuovere i tasti "CIRCUITO" e iniziare il gioco
        fadeImage.raycastTarget = true;
        StartCoroutine(fadeOut());
    }


    IEnumerator fadeIn()
    {
        //while (fadeImage.color.a > 0.01f)
        //{
        //    fadeImage.color = Color.Lerp(fadeImage.color, new Color(0, 0, 0, 0f), Time.deltaTime * 2);
        //    print(fadeImage.color);
        //    yield return new WaitForEndOfFrame();
        //}
        //fadeImage.color = new Color(0, 0, 0, 0f);
        fadeImage.raycastTarget = false;
        fadeImage.GetComponent<Image>().CrossFadeAlpha(0f, 1f, false);
        yield return new WaitForSeconds(1f);
    }


    IEnumerator fadeOut()
    {
        //while (fadeImage.color.a < 0.99f)
        //{
        //    fadeImage.color = Color.Lerp(fadeImage.color, new Color(0, 0, 0, 1f), Time.deltaTime * 2);
        //    print(fadeImage.color);
        //    yield return new WaitForEndOfFrame();
        //}
        //fadeImage.color = new Color(0, 0, 0, 1f);
        fadeImage.GetComponent<Image>().CrossFadeAlpha(1f, 1f, false);
        yield return new WaitForSeconds(1.5f);
        //loadingText.SetActive(true);
        StartCoroutine(LoadAsynch());
        //StartLevel();
    }


    //private void StartLevel()
    //{
    //    //data.SelectedPlayer = int.Parse(SelectedPlayer.Split('r')[1]) - 1;
    //    //data.SelectedCircuit = int.Parse(SelectedCircuit.Split('t')[1]) - 1;
    //    SceneManager.LoadScene(levelIndex);
    //}


    IEnumerator LoadAsynch ()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);

        loadingText.SetActive(true);

        Vector3 rotation = new Vector3(0, 0, -180);
        while (!operation.isDone)
        {
            loadingText.transform.GetChild(0).Rotate(rotation * Time.deltaTime, Space.World);
            yield return null;
        }
    }


    //Metodo chiamato per controllare e inizializzare i valori delle variabili corrispondenti ai livelli -> 0 livello bloccato / 1 livello sbloccato
    public void SetupLevels ()
    {
        if (!PlayerPrefs.HasKey("lvl_2"))
        {
            PlayerPrefs.SetInt("lvl_2", 0);
        }

        if (!PlayerPrefs.HasKey("lvl_3"))
        {
            PlayerPrefs.SetInt("lvl_3", 0);
        }

        if (!PlayerPrefs.HasKey("lvl_4"))
        {
            PlayerPrefs.SetInt("lvl_4", 0);
        }

        if (!PlayerPrefs.HasKey("lvl_5"))
        {
            PlayerPrefs.SetInt("lvl_5", 0);
        }

        if (!PlayerPrefs.HasKey("lvl_6"))
        {
            PlayerPrefs.SetInt("lvl_6", 0);
        }
    }


    //Controlla e in caso sblocca la possibilità di selezionare i livelli in base al valore nei player prefs
    public void CheckLevels ()
    {
        if (PlayerPrefs.HasKey("lvl_2"))
        {
            if(PlayerPrefs.GetInt("lvl_2") == 1)
            {
                level2Button.GetComponent<Button>().interactable = true;
                level2Button.transform.GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                level2Button.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            } else
            {
                level2Button.GetComponent<Button>().interactable = false;
                level2Button.transform.GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, 128);
                level2Button.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 128);
            }
        }

        if (PlayerPrefs.HasKey("lvl_3"))
        {
            if (PlayerPrefs.GetInt("lvl_3") == 1)
            {
                level3Button.GetComponent<Button>().interactable = true;
                level3Button.transform.GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                level3Button.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            } else
            {
                level3Button.GetComponent<Button>().interactable = false;
                level3Button.transform.GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, 128);
                level3Button.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 128);
            }
        }

        if (PlayerPrefs.HasKey("lvl_4"))
        {
            if (PlayerPrefs.GetInt("lvl_4") == 1)
            {
                level4Button.GetComponent<Button>().interactable = true;
                level4Button.transform.GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                level4Button.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            } else
            {
                level4Button.GetComponent<Button>().interactable = false;
                level4Button.transform.GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, 128);
                level4Button.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 128);
            }
        }

        if (PlayerPrefs.HasKey("lvl_5"))
        {
            if (PlayerPrefs.GetInt("lvl_5") == 1)
            {
                level5Button.GetComponent<Button>().interactable = true;
                level5Button.transform.GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                level5Button.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            } else
            {
                level5Button.GetComponent<Button>().interactable = false;
                level5Button.transform.GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, 128);
                level5Button.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 128);
            }
        }

        if (PlayerPrefs.HasKey("lvl_6"))
        {
            if (PlayerPrefs.GetInt("lvl_6") == 1)
            {
                level6Button.GetComponent<Button>().interactable = true;
                level6Button.transform.GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                level6Button.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            } else
            {
                level6Button.GetComponent<Button>().interactable = false;
                level6Button.transform.GetChild(0).GetComponent<Text>().color = new Color32(255, 255, 255, 128);
                level6Button.transform.GetChild(1).GetComponent<Image>().color = new Color32(255, 255, 255, 128);
            }
        }

    }


    //Developer Method per testare i livelli senza doverli sbloccare prima
    public void UnlockLevels()
    {
        PlayerPrefs.SetInt("lvl_2", 1);
        PlayerPrefs.SetInt("lvl_3", 1);
        PlayerPrefs.SetInt("lvl_4", 1);
        PlayerPrefs.SetInt("lvl_5", 1);
        PlayerPrefs.SetInt("lvl_6", 1);
        CheckLevels();
    }

    public void LockLevels ()
    {
        PlayerPrefs.SetInt("lvl_2", 0);
        PlayerPrefs.SetInt("lvl_3", 0);
        PlayerPrefs.SetInt("lvl_4", 0);
        PlayerPrefs.SetInt("lvl_5", 0);
        PlayerPrefs.SetInt("lvl_6", 0);
        CheckLevels();
    }
}
