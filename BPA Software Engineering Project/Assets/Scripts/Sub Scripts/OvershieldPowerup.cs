using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvershieldPowerup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (SubScript.instance.subHealth < 5)
        {
            SubScript.instance.subHealth = 4;
            Destroy(gameObject);
        }
    }
}
