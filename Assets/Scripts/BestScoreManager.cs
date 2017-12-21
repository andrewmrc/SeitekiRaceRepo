using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreManager : MonoBehaviour {

    public Text scoreLevel1, scoreLevel2, scoreLevel3, scoreLevel4, scoreLevel5, scoreLevel6;

    public void Start()
    {
        //Scommentare il metodo qui sotto e avviare il gioco una volta se si vuole cancellare i best score dai player prefs!!!
        //DeleteBestScoreKeyFromPlayerPrefs();

        //All'avvio crea o carica i valori di best score dai player prefs
        LoadScoreFromPlayerPrefs();
    }

	public void LoadScoreFromPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("BestScore_Lv1"))
        {
            scoreLevel1.text = PlayerPrefs.GetInt("BestScore_Lv1").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("BestScore_Lv1", 0);
            scoreLevel1.text = PlayerPrefs.GetInt("BestScore_Lv1").ToString();
        }

        if (PlayerPrefs.HasKey("BestScore_Lv2"))
        {
            scoreLevel2.text = PlayerPrefs.GetInt("BestScore_Lv2").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("BestScore_Lv2", 0);
            scoreLevel2.text = PlayerPrefs.GetInt("BestScore_Lv2").ToString();
        }

        if (PlayerPrefs.HasKey("BestScore_Lv3"))
        {
            scoreLevel3.text = PlayerPrefs.GetInt("BestScore_Lv3").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("BestScore_Lv3", 0);
            scoreLevel3.text = PlayerPrefs.GetInt("BestScore_Lv3").ToString();
        }

        if (PlayerPrefs.HasKey("BestScore_Lv4"))
        {
            scoreLevel4.text = PlayerPrefs.GetInt("BestScore_Lv4").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("BestScore_Lv4", 0);
            scoreLevel4.text = PlayerPrefs.GetInt("BestScore_Lv4").ToString();
        }

        if (PlayerPrefs.HasKey("BestScore_Lv5"))
        {
            scoreLevel5.text = PlayerPrefs.GetInt("BestScore_Lv5").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("BestScore_Lv5", 0);
            scoreLevel5.text = PlayerPrefs.GetInt("BestScore_Lv5").ToString();
        }

        if (PlayerPrefs.HasKey("BestScore_Lv6"))
        {
            scoreLevel6.text = PlayerPrefs.GetInt("BestScore_Lv6").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("BestScore_Lv6", 0);
            scoreLevel6.text = PlayerPrefs.GetInt("BestScore_Lv6").ToString();
        }

    }


    //Utility per gli sviluppatori: metodo che consente di cancellare i best score dai player prefs
    public void DeleteBestScoreKeyFromPlayerPrefs ()
    {
        PlayerPrefs.DeleteKey("BestScore_Lv1");
        PlayerPrefs.DeleteKey("BestScore_Lv2");
        PlayerPrefs.DeleteKey("BestScore_Lv3");
        PlayerPrefs.DeleteKey("BestScore_Lv4");
        PlayerPrefs.DeleteKey("BestScore_Lv5");
        PlayerPrefs.DeleteKey("BestScore_Lv6");
    }

}
