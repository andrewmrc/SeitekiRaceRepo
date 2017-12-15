using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    private MovePlayer refMP;
    private CollisionPlayer refCP;
    private SoundManager refSM;
    private InvincibilitySphere refInv;
    private GameDataTransfer refGDT;
    private AudioManager refAM;
    public GameObject panelGameOver, panelFinishedLevel;
    public Text scoreGO, scoreFL, nProjectilesText;
    public Image startCounter;
    public Transform lane_0, lane_1, lane_2, lane_less_1, lane_less_2;

    public List<Sprite> counterImage = new List<Sprite>();
    public AudioClip counterSound;

    public float initialY;

    public Action<int,bool> delCurrentLane;

    public GameObject scorePoint, fillPower;
    public GameObject textScorePlayer;
    public int currentScore;
    public GameObject shootButton;

    public GameObject fpsCounterText;
    float fpsCounter = 0;
    float frequency = 0.5f;

    public int FramesPerSec { get; protected set; }
    private bool endScore;

    private GameObject fadeOutPanel;

    private GameObject pauseButton;
    private GameObject pausePanel;

    public bool noShoot = false;

    //Oggetti Bonus
    public int specialItemCount = 0;

    //Variabili relative allo shooting
    public int nProjectiles = 10;

    [Space(10)]

    //Serve a identificare il livello corrente per salvare correttamente i player prefs
    public int levelKeyIdentifier;



    // Assign delegates to their methods
    private void Awake()
    {
        nProjectilesText.text = nProjectiles.ToString();
        Time.timeScale = 0;
        StartCoroutine(StartCounterCO());
        refMP = FindObjectOfType<MovePlayer>();
        refSM = FindObjectOfType<SoundManager>();
        refCP = FindObjectOfType<CollisionPlayer>();
        refInv = FindObjectOfType<InvincibilitySphere>();
        refGDT = FindObjectOfType<GameDataTransfer>();
        refAM = FindObjectOfType<AudioManager>();
        refCP.delGameOver = GameOver;
        refCP.delFinishLevel = FinishedLevel;
        //refCP.delCondom = Condom;
        //refCP.delBat = Bat;
        //refCP.delHandcuff = Handcuff;
        //refCP.delMouth = Mouth;
        //refCP.delMuzzle = Muzzle;
        //refCP.delPill = Pill;
        //refCP.delUnderwear = Underwear;
        refCP.delBonus = Bonus;
        refCP.delMalus = Malus;

        refCP.delSpecialItem = Special;

        refCP.delRecharge = RechargeProjectiles;
        //textScorePlayer = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMesh>();
        textScorePlayer = GameObject.FindGameObjectWithTag("ScoreText");

        //initialY = textScorePlayer.transform.localPosition.y;
        textScorePlayer.gameObject.SetActive(false);
        //StartCoroutine(DistanceScore());

        //StartCoroutine(FPS());
        pausePanel = GameObject.FindGameObjectWithTag("PausePanel");
        pausePanel.SetActive(false);
        pauseButton = GameObject.FindGameObjectWithTag("PauseButton");
        pauseButton.GetComponent<Button>().onClick.AddListener(PauseGame);
        fadeOutPanel = GameObject.FindGameObjectWithTag("FadeOutPanel");
        //fadeOutPanel.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
        fadeOutPanel.GetComponent<Image>().CrossFadeAlpha(0f, 1f, true);
    }


    

    private void Update()
    {
        scorePoint.GetComponentInChildren<Text>().text = currentScore.ToString();
        //fpsCounter += (Time.deltaTime - fpsCounter) * .1f;
        //if (!pickup)
        //{
        //    StopCoroutine("FeedbackBonusCO");
        //    StopCoroutine("FeedbackMalusCO");
        //    ResetTextScore();
        //}
    }


    //private IEnumerator FPS()
    //{
    //    for (;;)
    //    {
    //        // Capture frame-per-second
    //        int lastFrameCount = Time.frameCount;
    //        float lastTime = Time.realtimeSinceStartup;
    //        yield return new WaitForSeconds(frequency);
    //        float timeSpan = Time.realtimeSinceStartup - lastTime;
    //        int frameCount = Time.frameCount - lastFrameCount;

    //        // Display it
    //        FramesPerSec = Mathf.RoundToInt(frameCount / timeSpan);
    //        fpsCounterText.GetComponent<Text>().text = FramesPerSec.ToString() + " fps";
    //    }
    //}


    private IEnumerator StartCounterCO()
    {
        while (Time.timeScale == 0)
        {
            startCounter.overrideSprite = counterImage[2];
            startCounter.GetComponent<AudioSource>().PlayOneShot(counterSound);
            //startCounter.color = Color.green;
            yield return new WaitForSecondsRealtime(1f);
            startCounter.overrideSprite = counterImage[1];
            startCounter.GetComponent<AudioSource>().PlayOneShot(counterSound);
            //startCounter.color = Color.red;
            yield return new WaitForSecondsRealtime(1f);
            startCounter.overrideSprite = counterImage[0];
            startCounter.GetComponent<AudioSource>().PlayOneShot(counterSound);
            //startCounter.color = Color.magenta;
            yield return new WaitForSecondsRealtime(1f);
            startCounter.gameObject.SetActive(false);
            //shootButton.SetActive(true);
            Time.timeScale = 1;
        }
        pauseButton.GetComponent<Button>().interactable = true;
        refSM.StartPlayAudioClip();
        StartCoroutine(DistanceScore());
    }

    // Method called by button for restart scene
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Time.timeScale = 1;
    }

    // Method called to return in main menu
    public void ReturnToMainMenu()
    {
        
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        //Application.Quit();

    }

    // Increment score based to distance of the player and 
    //set it to text in 2 panel of Game Over and Finished Level
    private IEnumerator DistanceScore()
    {
        while (!endScore)
        {
            currentScore += (-(int)refMP.transform.position.z / 10);// * ((int)refMP.speed / 10);
            scoreGO.text = "YOUR SCORE: " + currentScore.ToString();
            scoreFL.text = "FINAL SCORE: " + currentScore.ToString();
            yield return new WaitForSecondsRealtime(.1f);
        }
    }

    // Show green plus score feedback
    public void FeedbackBonusCO(float _value, bool _audioON)
    {
        //Debug.Log("CallBonus");

        //Attiva la clip audio
        if (_audioON) {
            delCurrentLane(refMP.numLane, true);
        }

        GameObject textSpawnedBonus = textScorePlayer.gameObject.Spawn(new Vector3(textScorePlayer.transform.position.x, textScorePlayer.transform.position.y, textScorePlayer.transform.position.z), Quaternion.Euler(textScorePlayer.transform.rotation.x, 0, textScorePlayer.transform.rotation.z)) as GameObject;

        textSpawnedBonus.transform.SetParent(panelFinishedLevel.gameObject.transform.parent);
        textSpawnedBonus.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        textSpawnedBonus.transform.GetChild(0).GetComponent<Text>().color = Color.green;
        textSpawnedBonus.transform.GetChild(0).GetComponent<HandleScoreTextUI>().scoreValue = _value;
        textSpawnedBonus.gameObject.SetActive(true);

    }

    // Show red minus score feedback
    public void FeedbackMalusCO(float _value)
    {
        //Disattiva la clip audio
        delCurrentLane(refMP.numLane, false);

        GameObject textSpawnedMalus = textScorePlayer.gameObject.Spawn(new Vector3(textScorePlayer.transform.position.x, textScorePlayer.transform.position.y, textScorePlayer.transform.position.z), Quaternion.Euler(textScorePlayer.transform.rotation.x, 0, textScorePlayer.transform.rotation.z)) as GameObject;

        textSpawnedMalus.transform.SetParent(textScorePlayer.gameObject.transform.parent);
        textSpawnedMalus.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        textSpawnedMalus.transform.GetChild(0).GetComponent<Text>().color = Color.red;
        textSpawnedMalus.transform.GetChild(0).GetComponent<HandleScoreTextUI>().scoreValue = _value;
        textSpawnedMalus.transform.GetChild(0).GetComponent<HandleScoreTextUI>().malus = true;
        textSpawnedMalus.gameObject.SetActive(true);
        fillPower.GetComponent<Image>().fillAmount -= .1f;

    }


    //public void ResetTextScore()
    //{
    //    pickup = false;
    //    textScorePlayer.gameObject.SetActive(false);
    //    textScorePlayer.transform.localPosition = new Vector3(textScorePlayer.transform.localPosition.x, initialY, textScorePlayer.transform.localPosition.z);
    //}

    #region DelegatesMethods

    //Handle Bonus
    private void Bonus(int _value)
    {
        //Play bonus clip based on current character
        int charIndex = 0;
        if (refGDT != null)
            charIndex = refGDT.SelectedPlayer;
        switch (charIndex)
        {
            case 0:
                refAM.GetComponent<AudioSource>().PlayOneShot(refAM.penisBonus);
                break;

            case 1:
                refAM.GetComponent<AudioSource>().PlayOneShot(refAM.vaginaBonus);
                break;

            case -1:
                refAM.GetComponent<AudioSource>().PlayOneShot(refAM.assBonus);
                break;
        }
        currentScore += _value;
        FeedbackBonusCO(_value, true);
    }


    //Handle Malus
    private void Malus(int _value)
    {
        //Play malus clip based on current character
        int charIndex = 0;
        if (refGDT != null)
            charIndex = refGDT.SelectedPlayer;
        switch (charIndex)
        {
            case 0:
                refAM.GetComponent<AudioSource>().PlayOneShot(refAM.penisMalus);
                break;

            case 1:
                refAM.GetComponent<AudioSource>().PlayOneShot(refAM.vaginaMalus);
                break;

            case -1:
                refAM.GetComponent<AudioSource>().PlayOneShot(refAM.assMalus);
                break;
        }
        currentScore -= _value;
        FeedbackMalusCO(_value);
    }


    //Handle Special Item
    private void Special(int _value)
    {
        specialItemCount += _value;
    }


    // Recharge Projectiles
    private void RechargeProjectiles(int nP)
    {
        nProjectiles += nP;
        if(nProjectiles > 10)
        {
            nProjectiles = 10;
        }
        nProjectilesText.text = nProjectiles.ToString();
        fillPower.GetComponent<Image>().fillAmount = nProjectiles/10;
    }


    //Setta nei player prefs il valore del livello sbloccato a 1
    private void SetLevelCompleted()
    {
        switch (levelKeyIdentifier)
        {
            case 1:
                PlayerPrefs.SetInt("lvl_2", 1);
                break;

            case 2:
                PlayerPrefs.SetInt("lvl_3", 1);
                break;

            case 3:
                PlayerPrefs.SetInt("lvl_4", 1);
                break;

            case 4:
                PlayerPrefs.SetInt("lvl_5", 1);
                break;

            case 5:
                PlayerPrefs.SetInt("lvl_6", 1);
                break;

            default:

                break;
        }
    }


    // Stop all music, active panel Finish Level and play sound
    private void FinishedLevel(bool _on)
    {
        //Time.timeScale = 0;
        if (specialItemCount == 3)
        {
            currentScore += 50000;
        }
        pauseButton.SetActive(false);

        nProjectiles = 0;
        refMP.enabled = false;
        endScore = true;

        for (int i = 0; i < refSM.laneArray.Length; i++)
        {
            refSM.laneArray[i].GetComponent<AudioSource>().Stop();
        }

        //panelFinishedLevel.SetActive(_on);
        //panelFinishedLevel.GetComponent<AudioSource>().Play();

        //Salva il livello come completato nei player prefs
        SetLevelCompleted();

        StartCoroutine(FadeOutPanel(1));

    }


    // Stop all music, active panel Game Over and play sound
    private void GameOver(bool _on)
    {
        //Play death clip based on current character
        int charIndex = 0;
        if(refGDT != null)
            charIndex = refGDT.SelectedPlayer;
        switch (charIndex)
        {
            case 0:
                refAM.GetComponent<AudioSource>().PlayOneShot(refAM.penisDead);
                break;

            case 1:
                refAM.GetComponent<AudioSource>().PlayOneShot(refAM.vaginaDead);
                break;

            case -1:
                refAM.GetComponent<AudioSource>().PlayOneShot(refAM.assDead);
                break;
        }


        pauseButton.SetActive(false);
        //Time.timeScale = 0;
        nProjectiles = 0;
        refMP.enabled = false;
        endScore = true;

        for (int i = 0; i < refSM.laneArray.Length; i++)
        {
            refSM.laneArray[i].GetComponent<AudioSource>().Stop();
        }

        StartCoroutine(FadeOutPanel(0));
        //panelGameOver.SetActive(_on);
        //panelGameOver.GetComponent<AudioSource>().Play();
    }


    private IEnumerator FadeOutPanel(int value)
    {
        fadeOutPanel.GetComponent<Image>().CrossFadeAlpha(1f, 1f, false);
        yield return new WaitForSeconds(1f);
        if(value == 0)
        {
            panelGameOver.SetActive(true);
        } else
        {
            panelFinishedLevel.SetActive(true);
            panelFinishedLevel.GetComponent<AudioSource>().Play();
        }
        yield return new WaitForSeconds(1f);
        fadeOutPanel.GetComponent<Image>().CrossFadeAlpha(0f, 1f, false);
    }
    

    public void PauseGame ()
    {
        if(Time.timeScale == 1)
        {
            pauseButton.GetComponent<Image>().sprite = pauseButton.GetComponent<PauseSpriteContainer>().playIcon;
            endScore = true;
            noShoot = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            endScore = false;
            Time.timeScale = 1;
            pauseButton.GetComponent<Image>().sprite = pauseButton.GetComponent<PauseSpriteContainer>().pauseIcon;
            pausePanel.SetActive(false);
            noShoot = false;
        }
    }

    //// Called when take a Condom, change score of value passed by delegate
    //private void Condom(int _value, string _name)
    //{
    //    switch (_name)
    //    {
    //        case "Penis":
    //            currentScore += _value;
    //            StartCoroutine(FeedbackBonusCO(_value));
    //            break;
    //        case "Vagina":
    //            currentScore -= _value;
    //            StartCoroutine(FeedbackMalusCO(_value));
    //            break;
    //        case "Ass":
    //            currentScore -= _value;
    //            StartCoroutine(FeedbackMalusCO(_value));
    //            break;
    //    }
    //}

    //// Called when take a Bat, change score of value passed by delegate
    //private void Bat(int _value, string _name)
    //{
    //    switch (_name)
    //    {
    //        case "Penis":
    //            currentScore -= _value;
    //            StartCoroutine(FeedbackMalusCO(_value));
    //            break;
    //        case "Vagina":
    //            currentScore -= _value;
    //            StartCoroutine(FeedbackMalusCO(_value));
    //            break;
    //        case "Ass":
    //            currentScore -= _value;
    //            StartCoroutine(FeedbackMalusCO(_value));
    //            break;
    //    }
    //}

    //private void Handcuff(int _value, string _name)
    //{
    //    switch (_name)
    //    {
    //        case "Penis":
    //            currentScore += _value;
    //            StartCoroutine(FeedbackBonusCO(_value));
    //            break;
    //        case "Vagina":
    //            currentScore += _value;
    //            StartCoroutine(FeedbackBonusCO(_value));
    //            break;
    //        case "Ass":
    //            currentScore += _value;
    //            StartCoroutine(FeedbackBonusCO(_value));
    //            break;
    //    }
    //}

    //private void Mouth(int _value, string _name)
    //{
    //    switch (_name)
    //    {
    //        case "Penis":
    //            currentScore += _value;
    //            StartCoroutine(FeedbackBonusCO(_value));
    //            break;
    //        case "Vagina":
    //            currentScore += _value;
    //            StartCoroutine(FeedbackBonusCO(_value));
    //            break;
    //        case "Ass":
    //            currentScore -= _value;
    //            StartCoroutine(FeedbackMalusCO(_value));
    //            break;
    //    }
    //}

    //private void Muzzle(int _value, string _name)
    //{
    //    switch (_name)
    //    {
    //        case "Penis":
    //            currentScore -= _value;

    //            StartCoroutine(FeedbackMalusCO(_value));
    //            break;
    //        case "Vagina":
    //            currentScore -= _value;
    //            StartCoroutine(FeedbackMalusCO(_value));
    //            break;
    //        case "Ass":
    //            currentScore += _value;
    //            StartCoroutine(FeedbackBonusCO(_value));
    //            print("Dentro Muzzle");
    //            break;
    //    }
    //}

    //private void Underwear(int _value, string _name)
    //{
    //    switch (_name)
    //    {
    //        case "Penis":
    //            currentScore += _value;
    //            StartCoroutine(FeedbackBonusCO(_value));
    //            break;
    //        case "Vagina":
    //            currentScore += _value;
    //            StartCoroutine(FeedbackBonusCO(_value));
    //            break;
    //        case "Ass":
    //            currentScore += _value;
    //            StartCoroutine(FeedbackBonusCO(_value));
    //            break;
    //    }
    //}

    //private void Pill(int _value, string _name)
    //{
    //    switch (_name)
    //    {
    //        case "Penis":
    //            currentScore -= _value;
    //            StartCoroutine(FeedbackMalusCO(_value));
    //            break;
    //        case "Vagina":
    //            currentScore += _value;
    //            StartCoroutine(FeedbackBonusCO(_value));
    //            break;
    //        case "Ass":
    //            currentScore -= _value;
    //            StartCoroutine(FeedbackMalusCO(_value));
    //            break;
    //    }
    //}

    
    #endregion
}
