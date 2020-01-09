using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWallScript : MonoBehaviour
{
    public GameObject bossGate;
    private bool canGo = true;

    // function to be called when the sub hits the load zone
    private void Start()
    {
        bossGate.transform.position = bossGate.transform.position + new Vector3(0, 1000, 0);
    }

    // on collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerSub" && canGo)
        {
            bossGate.transform.position = bossGate.transform.position + new Vector3(0, -1000, 0);
            canGo = false;
        }
    }
}
