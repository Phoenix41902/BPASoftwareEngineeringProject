using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwilightGameController : MonoBehaviour
{
    // create the instance
    public static TwilightGameController instance;

    // function to make only one game controller per scene
    void MakeSingleTon()
    {
        instance = this;
    }

    // on start
    private void Awake()
    {
        MakeSingleTon();
        DoorOne.sprite = DoorSprites[DoorHealth];
    }

    // create the arrays of patrol points
    public Transform[] FirstAnglerRoomPoints;

    // create the states of the door
    public Sprite[] DoorSprites;
    public int DoorHealth = 4;

    // creates the section where the first door control is
    public SpriteRenderer DoorOne;
    public GameObject DoorOneObj;

    // function to lower the door
    public void AnglerLowerDoor()
    {
       if (DoorHealth >= 0)
        {
            DoorHealth -= 1;
            DoorOne.sprite = DoorSprites[DoorHealth];
        } else
        {
            Destroy(DoorOneObj);
        }
    }

    private void Update()
    {
        // section for the first door
        
    }
}
