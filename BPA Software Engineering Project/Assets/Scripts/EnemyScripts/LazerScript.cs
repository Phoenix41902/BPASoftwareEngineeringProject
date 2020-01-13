using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float damage;
    private double time = 40;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody.velocity = -transform.right * 10;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "PlayerSub" || col.tag == "Terrain") {
            if (col.tag == "PlayerSub") {
                SubScript.instance.subHealth--;
            }
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }
}
