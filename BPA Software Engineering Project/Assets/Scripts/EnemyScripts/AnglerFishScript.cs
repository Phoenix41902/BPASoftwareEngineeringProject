using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerFishScript : MonoBehaviour
{
    // set up parts of the fish
    public float Health;

    // collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if it is a missile and which one then subtract the health
        if (collision.tag == "ChargeMissile") Health -= SubScript.instance.chargeMissileDamage;
        if (collision.tag == "DefaultMissile") Health -= SubScript.instance.DefaultMissileDamage;
        if (collision.tag == "TripleMissile") Health -= SubScript.instance.TripleMissileDamage;
        if (collision.tag == "BigMissile") Health -= SubScript.instance.BigMissileDamage;
    }

    // if it is out of health, die
    private void Update()
    {
        if (Health <= 0)
        {
            TwilightGameController.instance.AnglerLowerDoor();
            Destroy(gameObject);
        }
    }
}