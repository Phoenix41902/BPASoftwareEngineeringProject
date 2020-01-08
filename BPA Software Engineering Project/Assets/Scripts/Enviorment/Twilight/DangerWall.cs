using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the sub comes in contact, kill it
        if (collision.tag == "PlayerSub")
        {
            SubScript.instance.subHealth--;
        }
    }
}
