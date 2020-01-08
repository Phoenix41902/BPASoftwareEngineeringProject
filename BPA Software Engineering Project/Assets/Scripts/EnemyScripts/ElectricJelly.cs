using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricJelly : MonoBehaviour
{
    public float Health;
    public int index;

    private void OnTriggerEnter2D(Collider2D collision)
    {      
        // deal damage to player
        if (collision.tag == "PlayerSub")
        {
            SubScript.instance.subHealth -= 2;
        }

        // take damage
        if (collision.tag == "ChargeMissile") Health -= SubScript.instance.chargeMissileDamage;
        if (collision.tag == "DefaultMissile") Health -= SubScript.instance.DefaultMissileDamage;
        if (collision.tag == "TripleMissile") Health -= SubScript.instance.TripleMissileDamage;
        if (collision.tag == "BigMissile") Health -= SubScript.instance.BigMissileDamage;
    }

    private void Update()
    {
        if (Health <= 0)
        {
            TwilightGameController.instance.JellyKill(index);
            Destroy(gameObject);
        }
    }
}
