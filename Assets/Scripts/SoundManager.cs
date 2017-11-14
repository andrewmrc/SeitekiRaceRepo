using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private MovePlayer refMP;
    private GameManager refGM;
    public AudioTrackClass clipList;
    public List<AudioClip> currentAudioClipList = new List<AudioClip>();
    public Transform[] laneArray = new Transform[5];
    public AudioSource audioSoundManager;

    public int currIndexAudioClipArray;

    private void Awake()
    {
        refMP = FindObjectOfType<MovePlayer>();
        refGM = FindObjectOfType<GameManager>();
        refGM.delCurrentLane = PlaySingleTrack;
        audioSoundManager = GetComponent<AudioSource>();

        laneArray[0] = refGM.lane_0;
        laneArray[1] = refGM.lane_1;
        laneArray[2] = refGM.lane_2;
        laneArray[3] = refGM.lane_less_1;
        laneArray[4] = refGM.lane_less_2;

        //spegni le lane secondarie
        laneArray[1].transform.GetChild(0).gameObject.SetActive(false);
        laneArray[2].transform.GetChild(0).gameObject.SetActive(false);
        laneArray[3].transform.GetChild(0).gameObject.SetActive(false);
        laneArray[4].transform.GetChild(0).gameObject.SetActive(false);

        //Inserisci una traccia audi random come principale del livello
        SelectRandomAudioTrack();

        //Applica una clip audio per ogni tracciato
        SetAudioClip();
    }

    private void PlaySingleTrack(int _numLane, bool _on)
    {
        switch (_numLane)
        {
            //case 0:
            //    StartCoroutine(IncreaseVolumeCO(refGM.lane_0));

            //    StartCoroutine(DecreaseVolumeCO(lane_1));
            //    StartCoroutine(DecreaseVolumeCO(lane_2));
            //    StartCoroutine(DecreaseVolumeCO(lane_less_1));
            //    StartCoroutine(DecreaseVolumeCO(lane_less_2));
            //    break;
            case 1:
                if (_on)
                {
                    StartCoroutine(IncreaseVolumeCO(refGM.lane_1));
                    laneArray[1].GetComponent<MeshRenderer>().material.color = Color.blue;
                    laneArray[1].transform.GetChild(0).gameObject.SetActive(true);
                    laneArray[1].transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.blue;
                }
                else
                {
                    StartCoroutine(DecreaseVolumeCO(refGM.lane_1));
                    laneArray[1].GetComponent<MeshRenderer>().material.color = Color.white;
                    laneArray[1].transform.GetChild(0).gameObject.SetActive(false);
                    laneArray[1].transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
                }


                //StartCoroutine(DecreaseVolumeCO(lane_0));
                //StartCoroutine(DecreaseVolumeCO(lane_2));
                //StartCoroutine(DecreaseVolumeCO(lane_less_1));
                //StartCoroutine(DecreaseVolumeCO(lane_less_2));
                break;
            case 2:
                if (_on)
                {
                    StartCoroutine(IncreaseVolumeCO(refGM.lane_2));
                    laneArray[2].GetComponent<MeshRenderer>().material.color = Color.green;
                    laneArray[2].transform.GetChild(0).gameObject.SetActive(true);
                    laneArray[2].transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.green;
                }
                else
                {
                    StartCoroutine(DecreaseVolumeCO(refGM.lane_2));
                    laneArray[2].GetComponent<MeshRenderer>().material.color = Color.white;
                    laneArray[2].transform.GetChild(0).gameObject.SetActive(false);
                    laneArray[2].transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
                }

                //StartCoroutine(DecreaseVolumeCO(lane_1));
                //StartCoroutine(DecreaseVolumeCO(lane_0));
                //StartCoroutine(DecreaseVolumeCO(lane_less_1));
                //StartCoroutine(DecreaseVolumeCO(lane_less_2));
                break;
            case -1:
                if (_on)
                {
                    StartCoroutine(IncreaseVolumeCO(refGM.lane_less_1));
                    laneArray[3].GetComponent<MeshRenderer>().material.color = Color.yellow;
                    laneArray[3].transform.GetChild(0).gameObject.SetActive(true);
                    laneArray[3].transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.yellow;
                }
                else
                {
                    StartCoroutine(DecreaseVolumeCO(refGM.lane_less_1));
                    laneArray[3].GetComponent<MeshRenderer>().material.color = Color.white;
                    laneArray[3].transform.GetChild(0).gameObject.SetActive(false);
                    laneArray[3].transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
                }

                //StartCoroutine(DecreaseVolumeCO(lane_1));
                //StartCoroutine(DecreaseVolumeCO(lane_2));
                //StartCoroutine(DecreaseVolumeCO(lane_0));
                //StartCoroutine(DecreaseVolumeCO(lane_less_2));

                break;
            case -2:
                if (_on)
                {
                    StartCoroutine(IncreaseVolumeCO(refGM.lane_less_2));
                    laneArray[4].GetComponent<MeshRenderer>().material.color = Color.red;
                    laneArray[4].transform.GetChild(0).gameObject.SetActive(true);
                    laneArray[4].transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.red;
                }
                else
                {
                    StartCoroutine(DecreaseVolumeCO(refGM.lane_less_2));
                    laneArray[4].GetComponent<MeshRenderer>().material.color = Color.white;
                    laneArray[4].transform.GetChild(0).gameObject.SetActive(false);
                    laneArray[4].transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.white;
                }

                //StartCoroutine(DecreaseVolumeCO(lane_1));
                //StartCoroutine(DecreaseVolumeCO(lane_2));
                //StartCoroutine(DecreaseVolumeCO(lane_less_1));
                //StartCoroutine(DecreaseVolumeCO(lane_0));
                break;
        }
    }

    private IEnumerator IncreaseVolumeCO(Transform _lane)
    {
        _lane.GetComponent<AudioSource>().volume = 0.7f;
        yield return null;
    }

    private IEnumerator DecreaseVolumeCO(Transform _lane)
    {
        while (_lane.GetComponent<AudioSource>().volume != 0)
        {
            yield return null;
            _lane.GetComponent<AudioSource>().volume -= .05f;

        }
    }


    public void SelectRandomAudioTrack()
    {
        //estrae random da 1 a 3
        int randomTrack = Random.Range(0, 3);
        Debug.Log(randomTrack);

        //in base al numero random prende le audioclip da una diversa tracklist
        switch (randomTrack)
        {
            case 0:
                //ciclo for che prende la traccia corrispondente al numero precedentemente uscito random e la inserisce come currentAudioClipList
                for (int i = 0; i < clipList.audioClip_Track1.Length; i++)
                {
                    currentAudioClipList.Add(clipList.audioClip_Track1[i]);

                }
                break;
            case 1:
                //ciclo for che prende la traccia corrispondente al numero precedentemente uscito random e la inserisce come currentAudioClipList
                for (int i = 0; i < clipList.audioClip_Track2.Length; i++)
                {
                    currentAudioClipList.Add(clipList.audioClip_Track2[i]);

                }
                break;
            case 2:
                //ciclo for che prende la traccia corrispondente al numero precedentemente uscito random e la inserisce come currentAudioClipList
                for (int i = 0; i < clipList.audioClip_Track3.Length; i++)
                {
                    currentAudioClipList.Add(clipList.audioClip_Track3[i]);

                }
                break;
        }
    }


    public void SetAudioClip()
    {
        foreach (var lane in laneArray)
        {
            for (int i = currIndexAudioClipArray; i < currIndexAudioClipArray + 1; i++)
            {
                lane.GetComponent<AudioSource>().Stop();
                lane.GetComponent<AudioSource>().clip = currentAudioClipList[i];
                lane.GetComponent<AudioSource>().Play();
            }
            currIndexAudioClipArray++;
        }
    }
}
