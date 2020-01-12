using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTrigger : MonoBehaviour
{
    private int Health = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DefaultMissile")
        {
            Health--;
        }
    }
    private void Update()
    {
        if (Health <= 0)
        {
            TutorialScript.instance.HasEnteredThridRoom = true;
            Destroy(gameObject);
        }
    }
}
