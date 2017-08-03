using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject blinkingObject, playerPool, circuitPool;

    // Delegato chiamato quando si sceglie un determinato tracciato
    public Action<string> delChoosedTrack;
    public Action<string> delChoosedPlayer;
 

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
                circuitPool.SetActive(true);
            }
            // cliccando su un circuito lancio il delegato e cambio scena
            else if (blinkingObject.CompareTag("Circuit"))
            {
                Debug.Log(blinkingObject.name);
                delChoosedTrack(blinkingObject.name);
                SceneManager.LoadScene(blinkingObject.name);
            }
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
}
