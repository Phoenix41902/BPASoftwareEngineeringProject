using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTSCRIPT : MonoBehaviour
{
    private float health = 50;
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "DefaultMissile") {
            health -= SubScript.instance.defaultMissileDamage;
        } else if (col.tag == "ChargeMissile") {
            health -= SubScript.instance.lastChargeMissileDamage;
        } else if (col.tag == "BigMissile") {
            health -= SubScript.instance.bigMissileDamage;
        } else if (col.tag == "TripleMissile") {
            health -= SubScript.instance.tripleMissileDamage;
        }
        Debug.Log(health);
    }

    void Update() {
        if (health == 0f) {
            Destroy(gameObject);
        }
    }
}
