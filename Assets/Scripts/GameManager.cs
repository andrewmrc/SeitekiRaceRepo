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
    public GameObject panelGameOver, panelFinishedLevel;
    public Text scoreGO, scoreFL, nProjectilesText;
    public Image startCounter;
    public Transform lane_0, lane_1, lane_2, lane_less_1, lane_less_2;

    public List<Sprite> counterImage = new List<Sprite>();

    public float initialY;

    public Action<int,bool> delCurrentLane;

    public GameObject scorePoint, fillPower;
    public TextMesh textScorePlayer;
    public int currentScore;
    public GameObject shootButton;

    public GameObject fpsCounterText;
    float fpsCounter = 0;
    float frequency = 0.5f;

    public int FramesPerSec { get; protected set; }
    public bool pickup;

    //Variabili relative allo shooting
    public int nProjectiles = 10;

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
       
        refCP.delRecharge = RechargeProjectiles;
        textScorePlayer = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMesh>();
        initialY = textScorePlayer.transform.localPosition.y;
        textScorePlayer.gameObject.SetActive(false);
        //StartCoroutine(DistanceScore());

        StartCoroutine(FPS());
    }


    

    private void Update()
    {
        scorePoint.GetComponentInChildren<Text>().text = currentScore.ToString();
        fpsCounter += (Time.deltaTime - fpsCounter) * .1f;
        if (!pickup)
        {
            StopCoroutine("FeedbackBonusCO");
            StopCoroutine("FeedbackMalusCO");
            ResetTextScore();
        }
    }


    private IEnumerator FPS()
    {
        for (;;)
        {
            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;

            // Display it
            FramesPerSec = Mathf.RoundToInt(frameCount / timeSpan);
            fpsCounterText.GetComponent<Text>().text = FramesPerSec.ToString() + " fps";
        }
    }


    private IEnumerator StartCounterCO()
    {
        while (Time.timeScale == 0)
        {
            startCounter.overrideSprite = counterImage[2];
            //startCounter.color = Color.green;
            yield return new WaitForSecondsRealtime(1f);
            startCounter.overrideSprite = counterImage[1];
            //startCounter.color = Color.red;
            yield return new WaitForSecondsRealtime(1f);
            startCounter.overrideSprite = counterImage[0];
            //startCounter.color = Color.magenta;
            yield return new WaitForSecondsRealtime(1f);
            startCounter.gameObject.SetActive(false);
            //shootButton.SetActive(true);
            Time.timeScale = 1;
        }
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
        while (Time.timeScale > 0)
        {
            currentScore += (-(int)refMP.transform.position.z / 10) * ((int)refMP.speed / 10);
            scoreGO.text = "Your score: " + currentScore.ToString();
            scoreFL.text = "Your final score: " + currentScore.ToString();
            yield return new WaitForSecondsRealtime(.1f);
        }
    }

    // Show green plus score feedback
    public IEnumerator FeedbackBonusCO(float _value)
    {
        //Debug.Log("CallBonus");
        pickup = false;
        pickup = true;

        //Attiva la clip audio
        delCurrentLane(refMP.numLane, true);

        ////Incrementa il potere per attivare la sfera di invincibilità
        //if (fillPower.GetComponent<Image>().fillAmount <= 0.9f)
        //{
        //    fillPower.GetComponent<Image>().fillAmount += .1f;
        //}
        //else
        //{
        //    // Set the superpower and reset the fillPower
        //    StartCoroutine(refInv.IncreaseSizeCO());
        //    fillPower.GetComponent<Image>().fillAmount = 0f;
        //}

        textScorePlayer.gameObject.SetActive(true);
        textScorePlayer.color = Color.green;        
        //initialY = textScorePlayer.transform.position.y;

        //fa muovere il text score verso l'alto
        while (textScorePlayer.transform.localPosition.y <= 25f || !pickup)
        {
            Debug.Log("CallBonus");
            textScorePlayer.transform.localPosition += new Vector3(0f, .5f, 0f);
            textScorePlayer.text = "+ " + _value.ToString();
            yield return null;
        }
        ResetTextScore();
    }

    // Show red minus score feedback
    public IEnumerator FeedbackMalusCO(float _value)
    {
        pickup = false;
        pickup = true;

        //Disattiva la clip audio
        delCurrentLane(refMP.numLane, false);

        textScorePlayer.gameObject.SetActive(true);
        textScorePlayer.color = Color.red;
        fillPower.GetComponent<Image>().fillAmount -= .1f;

        //initialY = textScorePlayer.transform.position.y;

        //fa muovere il text score verso l'alto
        while (textScorePlayer.transform.localPosition.y <= 25f || !pickup)
        {
            textScorePlayer.transform.localPosition += new Vector3(0f, .5f, 0f);
            textScorePlayer.text = "- " + _value.ToString();
            yield return null;
        }
        ResetTextScore();
    }


    public void ResetTextScore()
    {
        pickup = false;
        textScorePlayer.gameObject.SetActive(false);
        textScorePlayer.transform.localPosition = new Vector3(textScorePlayer.transform.localPosition.x, initialY, textScorePlayer.transform.localPosition.z);
    }

    #region DelegatesMethods

    //Handle Bonus
    private void Bonus(int _value)
    {
        currentScore += _value;
        StartCoroutine(FeedbackBonusCO(_value));
    }


    //Handle Malus
    private void Malus(int _value)
    {
        currentScore -= _value;
        StartCoroutine(FeedbackMalusCO(_value));
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


    // Stop all music, active panel Finish Level and play sound
    private void FinishedLevel(bool _on)
    {
        Time.timeScale = 0;

        for (int i = 0; i < refSM.laneArray.Length; i++)
        {
            refSM.laneArray[i].GetComponent<AudioSource>().Stop();
        }

        panelFinishedLevel.SetActive(_on);
        panelFinishedLevel.GetComponent<AudioSource>().Play();
    }

    // Stop all music, active panel Game Over and play sound
    private void GameOver(bool _on)
    {
        Time.timeScale = 0;

        for (int i = 0; i < refSM.laneArray.Length; i++)
        {
            refSM.laneArray[i].GetComponent<AudioSource>().Stop();
        }

        panelGameOver.SetActive(_on);
        panelGameOver.GetComponent<AudioSource>().Play();
    }

    // Called when take a Condom, change score of value passed by delegate
    private void Condom(int _value, string _name)
    {
        switch (_name)
        {
            case "Penis":
                currentScore += _value;
                StartCoroutine(FeedbackBonusCO(_value));
                break;
            case "Vagina":
                currentScore -= _value;
                StartCoroutine(FeedbackMalusCO(_value));
                break;
            case "Ass":
                currentScore -= _value;
                StartCoroutine(FeedbackMalusCO(_value));
                break;
        }
    }

    // Called when take a Bat, change score of value passed by delegate
    private void Bat(int _value, string _name)
    {
        switch (_name)
        {
            case "Penis":
                currentScore -= _value;
                StartCoroutine(FeedbackMalusCO(_value));
                break;
            case "Vagina":
                currentScore -= _value;
                StartCoroutine(FeedbackMalusCO(_value));
                break;
            case "Ass":
                currentScore -= _value;
                StartCoroutine(FeedbackMalusCO(_value));
                break;
        }
    }

    private void Handcuff(int _value, string _name)
    {
        switch (_name)
        {
            case "Penis":
                currentScore += _value;
                StartCoroutine(FeedbackBonusCO(_value));
                break;
            case "Vagina":
                currentScore += _value;
                StartCoroutine(FeedbackBonusCO(_value));
                break;
            case "Ass":
                currentScore += _value;
                StartCoroutine(FeedbackBonusCO(_value));
                break;
        }
    }

    private void Mouth(int _value, string _name)
    {
        switch (_name)
        {
            case "Penis":
                currentScore += _value;
                StartCoroutine(FeedbackBonusCO(_value));
                break;
            case "Vagina":
                currentScore += _value;
                StartCoroutine(FeedbackBonusCO(_value));
                break;
            case "Ass":
                currentScore -= _value;
                StartCoroutine(FeedbackMalusCO(_value));
                break;
        }
    }

    private void Muzzle(int _value, string _name)
    {
        switch (_name)
        {
            case "Penis":
                currentScore -= _value;

                StartCoroutine(FeedbackMalusCO(_value));
                break;
            case "Vagina":
                currentScore -= _value;
                StartCoroutine(FeedbackMalusCO(_value));
                break;
            case "Ass":
                currentScore += _value;
                StartCoroutine(FeedbackBonusCO(_value));
                print("Dentro Muzzle");
                break;
        }
    }

    private void Underwear(int _value, string _name)
    {
        switch (_name)
        {
            case "Penis":
                currentScore += _value;
                StartCoroutine(FeedbackBonusCO(_value));
                break;
            case "Vagina":
                currentScore += _value;
                StartCoroutine(FeedbackBonusCO(_value));
                break;
            case "Ass":
                currentScore += _value;
                StartCoroutine(FeedbackBonusCO(_value));
                break;
        }
    }

    private void Pill(int _value, string _name)
    {
        switch (_name)
        {
            case "Penis":
                currentScore -= _value;
                StartCoroutine(FeedbackMalusCO(_value));
                break;
            case "Vagina":
                currentScore += _value;
                StartCoroutine(FeedbackBonusCO(_value));
                break;
            case "Ass":
                currentScore -= _value;
                StartCoroutine(FeedbackMalusCO(_value));
                break;
        }
    }

    
    #endregion
}
