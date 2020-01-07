using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider col) {
        if (col.tag == "PlayerSub") {
            Debug.Log("asdf");
        }
    }
}
