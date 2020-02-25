using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // activeate fight
        if (collision.tag == "PlayerSub")
        {
            GameControllerScript.instance.SetToBossMusic();
            AbyssalController.instance.FinalBossStarted = true;
        }
    }
}
