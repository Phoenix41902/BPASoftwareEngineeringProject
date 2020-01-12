using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThridTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerSub")
        {
            TutorialScript.instance.HasEnteredBossRoom = true;
        }
    }
}
