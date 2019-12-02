using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCollectorScript : MonoBehaviour
{
    // destroy the missile when it enters
    void OnTriggerEnter2D(Collider2D collider) {
        Destroy(collider.gameObject);
    }
}
