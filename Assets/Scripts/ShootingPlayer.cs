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
        //if (Input.GetMouseButtonDown(0))
        //{
        //    GameObject bulletSpawned = Instantiate(prefab);
        //    bulletSpawned.transform.position = this.transform.position;

        //    refBullet = FindObjectOfType<Bullet>();
        //    refBullet.delKillPig = KillPig;
        //}
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
