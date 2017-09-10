using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject blinkingObject, playerPool, circuitPool;

    // Delegato chiamato quando si sceglie un determinato tracciato
    public Action<string> delChoosedTrack;
    public Action<string> delChoosedPlayer;

    [SerializeField]
    Image fadeImage;


    private void Update()
    {
        CheckMousePosition();

        if (Input.GetMouseButtonDown(0) && blinkingObject != null)
        {
            // Se é il player lo fa scomparire, non lo fa distruggere e fa apparire i circuiti
            if (blinkingObject.CompareTag("Player"))
            {
                delChoosedPlayer(blinkingObject.name);
                playerPool.SetActive(false);
                Debug.Log(blinkingObject.name);
                StartFadeOut();
                //circuitPool.SetActive(true);
            }
            // cliccando su un circuito lancio il delegato e cambio scena
            //else if (blinkingObject.CompareTag("Circuit"))
            //{
            //    Debug.Log(blinkingObject.name);
            //    delChoosedTrack(blinkingObject.name);
            //    SceneManager.LoadScene(blinkingObject.name);
            //}
        }
    }

    // Serve per far fare l'animazione all'oggetto su cui é sopra il mouse
    private void CheckMousePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.blue);
            blinkingObject = hit.transform.gameObject;
        }
        else
        {
            blinkingObject = null;
        }
    }


    public void CharSelected ()
    {
        StartFadeOut();

    }

    private void StartFadeOut()
    {
        //devo rimuovere i tasti "CIRCUITO" e iniziare il gioco
        fadeImage.raycastTarget = true;
        StartCoroutine(fadeOut());
    }


    IEnumerator fadeOut()
    {
        while (fadeImage.color.a < 0.99f)
        {
            fadeImage.color = Color.Lerp(fadeImage.color, new Color(0, 0, 0, 1f), Time.deltaTime * 2);
            print(fadeImage.color);
            yield return new WaitForEndOfFrame();
        }
        fadeImage.color = new Color(0, 0, 0, 1f);
        StartLevel();
    }


    private void StartLevel()
    {
        //data.SelectedPlayer = int.Parse(SelectedPlayer.Split('r')[1]) - 1;
        //data.SelectedCircuit = int.Parse(SelectedCircuit.Split('t')[1]) - 1;
        SceneManager.LoadScene("Circuit1");
    }
}
