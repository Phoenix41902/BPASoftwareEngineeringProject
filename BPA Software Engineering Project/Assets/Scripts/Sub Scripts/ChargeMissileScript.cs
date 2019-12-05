using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeMissileScript : MonoBehaviour
{
    // vars
    private float speed;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        speed = SubScript.instance.chargeMissileSpeed;
        // missile needs to fly forward
        rb.velocity = transform.right * speed;
    }
    // if it hits something
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Enemy") {
            Destroy(gameObject);
        }
    }
}
