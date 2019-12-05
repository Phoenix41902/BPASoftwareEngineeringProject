﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrippleMissileScript : MonoBehaviour
{
   // vars
    private float speed = 8f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        // missile needs to fly forward
        rb.velocity = transform.right * speed;
    }
}