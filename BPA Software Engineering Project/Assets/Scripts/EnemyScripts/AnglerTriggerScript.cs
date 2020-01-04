using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerTriggerScript : MonoBehaviour
{
    // fireing point
    [SerializeField]
    private GameObject lazerPrefab;
    [SerializeField]
    private GameObject firePoint;

    // animator
    [SerializeField]
    private Animator anglerAnim;

    // fire the lazer
    public void fireLazer()
    {
        Instantiate(lazerPrefab, firePoint.transform.position, firePoint.transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerSub")
        {
            fireLazer();
            anglerAnim.SetTrigger("fire");
        }
    }
}
