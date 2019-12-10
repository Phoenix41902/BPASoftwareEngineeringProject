using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerFishScript : MonoBehaviour
{
    // set up parts of the fish
    private float health = 30f;
    private float speed = 3f;

    // prefab for fireing
    [SerializeField]
    public GameObject lazerPrefab;
    public GameObject FirePoint;

    // animator
    [SerializeField]
    private Animator anglerAnim;

    void fireLazer() {
        Instantiate(lazerPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
    }

    void Update() {
        if (Input.GetButtonDown("Jump")) {
            fireLazer();
            anglerAnim.SetTrigger("fire");
        }
    }
}