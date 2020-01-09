using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    // open
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerSub")
        {
            TwilightGameController.instance.OpenDoors();
            Destroy(gameObject);
        }
    }
}
