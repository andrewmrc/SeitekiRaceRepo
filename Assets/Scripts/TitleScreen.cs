using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {


    public Image fadeImage, cmLOGO, biokipLOGO;


    // Use this for initialization
    void Start () {
        StartCoroutine(HandleTitleScreen());
	}
	

    IEnumerator HandleTitleScreen()
    {
        StartCoroutine(fadeIn());
        yield return new WaitForSeconds(3f);
        StartCoroutine(fadeOut());
        yield return new WaitForSeconds(2f);
        cmLOGO.gameObject.SetActive(false);
        biokipLOGO.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        StartCoroutine(fadeIn());
        yield return new WaitForSeconds(3f);
        StartCoroutine(fadeOut());
        yield return new WaitForSeconds(3f);
        StartLevel();
    }


    IEnumerator fadeIn()
    {
        while (fadeImage.color.a > 0.01f)
        {
            fadeImage.color = Color.Lerp(fadeImage.color, new Color(0, 0, 0, 0f), Time.deltaTime * 2);
            print(fadeImage.color);
            yield return new WaitForEndOfFrame();
        }
        fadeImage.color = new Color(0, 0, 0, 0f);
        fadeImage.raycastTarget = false;
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
    }


    private void StartLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
