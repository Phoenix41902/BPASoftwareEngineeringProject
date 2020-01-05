﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAnglerSpawn : MonoBehaviour
{
    // vars
    // spawn point and angler prefab
    public GameObject AnglerPrefab;
    public GameObject SpawnPoint;

    // timer vars
    public float StartWaitTime;
    private float waitTime;

    // spawn the angler fish
    private void spawn()
    {
        Instantiate(AnglerPrefab, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
    }

    // every frame
    private void Update()
    {
        // spawn a fish every x seconds
        if (waitTime <= 0)
        {
            spawn();
            waitTime = StartWaitTime;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
