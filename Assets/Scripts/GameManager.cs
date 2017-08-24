using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private MovePlayer refMP;
    private CollisionPlayer refCP;
    private SoundManager refSM;
    private InvincibilitySphere refInv;
    public GameObject panelGameOver, panelFinishedLevel;
    public Text scoreGO, scoreFL, startCounter;
    public Transform lane_0, lane_1, lane_2, lane_less_1, lane_less_2;


    public GameObject scorePoint, fillPower;
    public TextMesh textScorePlayer;
    public int currentScore;
    public GameObject shootButton;

    // Assign delegates to their methods
    private void Awake()
    {
        Time.timeScale = 0;
        StartCoroutine(StartCounterCO());
        refMP = FindObjectOfType<MovePlayer>();
        refSM = FindObjectOfType<SoundManager>();
        refCP = FindObjectOfType<CollisionPlayer>();
        refInv = FindObjectOfType<InvincibilitySphere>();
        refCP.delGameOver = GameOver;
        refCP.delFinishLevel = FinishedLevel;
        refCP.delCondom = Condom;
        refCP.delBat = Bat;
        refCP.delHandcuff = Handcuff;
        refCP.delMouth = Mouth;
        refCP.delMuzzle = Muzzle;
        refCP.delPill = Pill;
        refCP.delUnderwear = Underwear;

        //StartCoroutine(DistanceScore());
    }

    private void Update()
    {
        scorePoint.GetComponentInChildren<Text>().text = currentScore.ToString();
    }

    private IEnumerator StartCounterCO()
    {
        while (Time.timeScale == 0)
        {
            startCounter.text = "3";
            startCounter.color = Color.green;
            yield return new WaitForSecondsRealtime(1f);
            startCounter.text = "2";
            startCounter.color = Color.red;
            yield return new WaitForSecondsRealtime(1f);
            startCounter.text = "1";
            startCounter.color = Color.magenta;
            yield return new WaitForSecondsRealtime(1f);
            startCounter.gameObject.SetActive(false);
            shootButton.SetActive(true);
            Time.timeScale = 1;
        }
        StartCoroutine(DistanceScore());
    }

    // Method called by button for restart scene
    public void Restart()
    {
        SceneManager.LoadScene("Circuit 1");
        //Time.timeScale = 1;
    }

    // Method called to return in main menu
    public void ReturnToMainMenu()
    {
        //SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        Application.Quit();

    }

    // Increment score based to distance of the player and 
    //set it to text in 2 panel of Game Over and Finished Level
    private IEnumerator DistanceScore()
    {
        while (Time.timeScale > 0)
        {
            currentScore += ((int)refMP.transform.position.z * -1) * ((int)refMP.speed / 10);
            scoreGO.text = "Your score: " + currentScore.ToString();
            scoreFL.text = "Your final score: " + currentScore.ToString();
            yield return new WaitForSecondsRealtime(.1f);
        }
    }

    // Show green plus score feedback
    public IEnumerator FeedbackBonusCO(float _value)
    {
        if (fillPower.GetComponent<Image>().fillAmount <= 0.9f)
        {
            fillPower.GetComponent<Image>().fillAmount += .1f;
        }
        else
        {
            // Set the superpower and reset the fillPower
            StartCoroutine(refInv.IncreaseSizeCO());
            fillPower.GetComponent<Image>().fillAmount = 0f;
        }

        textScorePlayer.gameObject.SetActive(true);
        textScorePlayer.color = Color.green;        
        float initialY = textScorePlayer.transform.position.y;
        while (textScorePlayer.transform.position.y <= 25f)
        {
            textScorePlayer.transform.position += new Vector3(0f, .5f, 0f);
            textScorePlayer.text = "+ " + _value.ToString();
            yield return null;
        }
        textScorePlayer.gameObject.SetActive(false);
        textScorePlayer.transform.position = new Vector3(textScorePlayer.transform.position.x, initialY, textScorePlayer.transform.position.z);
    }

    // Show red minus score feedback
    public IEnumerator FeedbackMalusCO(float _value)
    {
        textScorePlayer.gameObject.SetActive(true);
        textScorePlayer.color = Color.red;
        fillPower.GetComponent<Image>().fillAmount -= .1f;

        float initialY = textScorePlayer.transform.position.y;

        while (textScorePlayer.transform.position.y <= 25f)
        {
            textScorePlayer.transform.position += new Vector3(0f, .5f, 0f);
            textScorePlayer.text = "- " + _value.ToString();
            yield return null;
        }

        textScorePlayer.gameObject.SetActive(false);
        textScorePlayer.transform.position = new Vector3(textScorePlayer.transform.position.x, initialY, textScorePlayer.transform.position.z);
    }

#region DelegatesMethods

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
