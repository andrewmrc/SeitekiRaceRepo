using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShootingPlayer : MonoBehaviour
{
    public GameObject prefab;
    private Bullet refBullet;
    private GameManager refGM;
    AudioSource newAS;
    public AudioClip shootingSound;

    private void Awake()
    {
        refGM = FindObjectOfType<GameManager>();
        newAS = gameObject.AddComponent<AudioSource>();
        newAS.spatialBlend = 0;
        newAS.clip = shootingSound;
    }

    private void Update()
    {
        if (refGM.nProjectiles > 0) {

            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }

            //if (Input.touchCount > 0 /*&& Input.GetTouch(0).phase == TouchPhase.Moved*/)
            //{
            //    Shoot();
            //}

        
            //for (int i = 0; i < Input.touchCount; ++i)
            //{
            //    if (Input.GetTouch(i).phase == TouchPhase.Began)
            //        Shoot();
            //}

        }

    }

    public void Shoot()
    {
        GameObject bulletSpawned = Instantiate(prefab);
        bulletSpawned.transform.position = this.transform.position;
        newAS.Play();
        refGM.nProjectiles--;
        refGM.fillPower.GetComponent<Image>().fillAmount -= .1f;
        refGM.nProjectilesText.text = refGM.nProjectiles.ToString();
        refBullet = FindObjectOfType<Bullet>();
        refBullet.delKillPig = KillPig;
    }

    private void KillPig(GameObject pig)
    {
        //refGM.currentScore += _value;
        //StartCoroutine(refGM.FeedbackBonusCO(_value));
        StartCoroutine(HandleKillPig(pig));
    }

    private IEnumerator HandleKillPig(GameObject pig)
    {
        pig.GetComponent<BoxCollider>().enabled = false;
        pig.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        pig.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        //pig.SetActive(false);
        Destroy(pig);
    }

}
