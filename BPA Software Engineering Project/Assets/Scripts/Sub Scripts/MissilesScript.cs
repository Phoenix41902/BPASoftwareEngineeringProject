using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilesScript : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FireMissiles")) {
            FireMissile();
        }
    }

    // method to shoot the missiles
    void FireMissile() {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
