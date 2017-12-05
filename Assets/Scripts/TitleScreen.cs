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
        fadeImage.GetComponent<Image>().CrossFadeAlpha(0f, 1f, false);
        yield return new WaitForSeconds(3f);
        fadeImage.GetComponent<Image>().CrossFadeAlpha(1f, 1f, false);
        yield return new WaitForSeconds(1.5f);
        cmLOGO.gameObject.SetActive(false);
        biokipLOGO.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        fadeImage.GetComponent<Image>().CrossFadeAlpha(0f, 1f, false);
        yield return new WaitForSeconds(3f);
        fadeImage.GetComponent<Image>().CrossFadeAlpha(1f, 1f, false);
        yield return new WaitForSeconds(3f);
        StartLevel();
    }


    private void StartLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
