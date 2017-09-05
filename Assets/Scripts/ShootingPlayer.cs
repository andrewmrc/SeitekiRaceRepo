using UnityEngine;

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

    public void Shoot()
    {
        GameObject bulletSpawned = Instantiate(prefab);
        bulletSpawned.transform.position = this.transform.position;

        refBullet = FindObjectOfType<Bullet>();
        refBullet.delKillPig = KillPig;
    }

    private void KillPig(int _value)
    {
        //refGM.currentScore += _value;
        //StartCoroutine(refGM.FeedbackBonusCO(_value));
    }
}
