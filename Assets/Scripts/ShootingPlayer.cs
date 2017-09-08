using UnityEngine;
using UnityEngine.UI;

public class ShootingPlayer : MonoBehaviour
{
    public GameObject prefab;
    private Bullet refBullet;
    private GameManager refGM;

    private void Awake()
    {
        refGM = FindObjectOfType<GameManager>();
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

        
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                    Shoot();
            }

        }

    }

    public void Shoot()
    {
        GameObject bulletSpawned = Instantiate(prefab);
        bulletSpawned.transform.position = this.transform.position;
        refGM.nProjectiles--;
        refGM.fillPower.GetComponent<Image>().fillAmount -= .1f;
        refGM.nProjectilesText.text = refGM.nProjectiles.ToString();
        refBullet = FindObjectOfType<Bullet>();
        refBullet.delKillPig = KillPig;
    }

    private void KillPig(int _value)
    {
        //refGM.currentScore += _value;
        //StartCoroutine(refGM.FeedbackBonusCO(_value));
    }
}
