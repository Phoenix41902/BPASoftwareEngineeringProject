using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTSCRIPT : MonoBehaviour
{
    private float health = 50;
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "DefaultMissile") {
            health -= col.gameObject.GetComponent<DefaultMissilesScript>().damage;
        } else if (col.tag == "ChargeMissile") {
            health -= col.gameObject.GetComponent<ChargeMissileScript>().damage;
        } else if (col.tag == "BigMissile") {
            health -= col.gameObject.GetComponent<BigMissileScirpt>().damage;
        } else if (col.tag == "TripleMissile") {
            health -= col.gameObject.GetComponent<TripleMissileScript>().damage;
        }
        
        Debug.Log(health);
    }

    void Update() {
        if (health <= 0f) {
            Destroy(gameObject);
        }
    }
}
