using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerSub")
        {
            AbyssalController.instance.HasEnteredMiniBossRoom = true;
        }
    }
}
