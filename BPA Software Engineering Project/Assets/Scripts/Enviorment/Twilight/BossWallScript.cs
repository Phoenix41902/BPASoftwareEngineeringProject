using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWallScript : MonoBehaviour
{
    
 

    // function to be called when the sub hits the load zone
    private void Start()
    {
       
    }

    // on collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerSub")
        {
            GameControllerScript.instance.SetToBossMusic();
            TwilightGameController.instance.BossFightStarted = true;
        }
    }
}
