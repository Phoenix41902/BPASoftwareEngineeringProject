using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerPatrol : MonoBehaviour
{
    // speed of fish
    public float Speed;

    // places where we can move
    private Transform[] MoveSpots = TwilightGameController.instance.FirstAnglerRoomPoints;
    private int randomSpot;

    // wait times
    private float waitTime;
    public float StartWaitTime;

    // start
    void Start()
    {
        // set wait time
        waitTime = StartWaitTime;
        // get a random spot to move towards
        randomSpot = Random.Range(0, MoveSpots.Length);    
    }

    private void Update()
    {
        // move towards the random postition
        transform.position = Vector2.MoveTowards(transform.position, MoveSpots[randomSpot].position, Speed * Time.deltaTime);

        // face the point
        // gets the new direction for where the player should face
        Vector2 direction = new Vector2(
            MoveSpots[randomSpot].position.x - transform.position.x,
            MoveSpots[randomSpot].position.y - transform.position.y
        );

        // changes the direction
        transform.right = -direction;

        // check if it is at the position
        if (Vector2.Distance(transform.position, MoveSpots[randomSpot].position) < 0.2f)
        {
            // if it is close enough wait there for a bit then find a new position
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, MoveSpots.Length);
                waitTime = StartWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
