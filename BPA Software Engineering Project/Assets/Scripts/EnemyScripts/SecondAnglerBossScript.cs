using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondAnglerBossScript : MonoBehaviour
{
    // move spots
    public Transform[] MoveSpots;
    private int randomSpot;

    // target
    public Transform Target;

    // fireing vars
    public GameObject LazerPrefab;
    public GameObject FirePoint;

    // speed
    public float Speed;

    // health
    public float Health;
    public Slider HealthBar;
    public GameObject BossCanvas;

    // wait times
    private float waitTime;
    public float StartWaitTime;

    // bool
    private bool fightHasStarted;
    private bool canFire = false;
    private bool hasChecked = false;

    // Key
    public GameObject Key;

    private void Start()
    {
        fightHasStarted = AbyssalController.instance.HasEnteredMiniBossRoom;
        waitTime = StartWaitTime;
        randomSpot = Random.Range(0, MoveSpots.Length);
        // hide health
        BossCanvas.SetActive(false);
    }

    private void Update()
    {
        fightHasStarted = AbyssalController.instance.HasEnteredMiniBossRoom;
        if (fightHasStarted)
        {
            facePlayerAndPatrol();
            checkForBossKill();
            if (!hasChecked)
            {
                canFire = true;
                hasChecked = true;
                BossCanvas.SetActive(true);
            }
        }

        if (canFire)
        {
            StartCoroutine(fireLazer());
        }
    }

    // function to fire
    IEnumerator fireLazer()
    {
        Instantiate(LazerPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
        canFire = false;
        yield return new WaitForSeconds(1f);
        canFire = true;
    }

    // function to move to a random position
    private void facePlayerAndPatrol()
    {
        // face the player
        // face the point
        // gets the new direction for where the player should face
        Vector2 directionPlayer = new Vector2(
            Target.position.x - transform.position.x,
            Target.position.y - transform.position.y
        );

        // changes the direction
        transform.right = -directionPlayer;

        // move towards the random postition
        transform.position = Vector2.MoveTowards(transform.position, MoveSpots[randomSpot].position, Speed * Time.deltaTime);

        // check if it is at the position
        if (Vector2.Distance(transform.position, MoveSpots[randomSpot].position) < 0.2f)
        {
            // if it is close enough wait there for a bit then find a new position
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, MoveSpots.Length);
                waitTime = StartWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    // function to check if boss is killed
    private void checkForBossKill()
    {
        HealthBar.value = Health;
        if (Health <= 0)
        {
            BossCanvas.SetActive(false);
            Instantiate(Key, FirePoint.transform.position, FirePoint.transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // take damage
        if (collision.tag == "ChargeMissile") Health -= SubScript.instance.chargeMissileDamage;
        if (collision.tag == "DefaultMissile") Health -= SubScript.instance.DefaultMissileDamage;
        if (collision.tag == "TripleMissile") Health -= SubScript.instance.TripleMissileDamage;
        if (collision.tag == "BigMissile") Health -= SubScript.instance.BigMissileDamage;
    }
}
