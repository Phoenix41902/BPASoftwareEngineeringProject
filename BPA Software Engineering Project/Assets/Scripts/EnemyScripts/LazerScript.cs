using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody.velocity = -transform.right * 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
