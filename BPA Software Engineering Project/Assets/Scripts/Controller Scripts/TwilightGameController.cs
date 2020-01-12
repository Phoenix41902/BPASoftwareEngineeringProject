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
        if (instance == null)
        {
            instance = this;
        }
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
            if (DoorHealth >= 0)
            {
                DoorOne.sprite = DoorSprites[DoorHealth];
            }         
        } else
        {
            Destroy(DoorOneObj);
        }
    }
 
    // section for getting to the key
    // jellys
    public GameObject guardObj;
    public SpriteRenderer guard;
    public Sprite[] guardPos;
    private int currentJelly = 0;

    public void JellyKill(int JellyIndex)
    {
        if (JellyIndex == currentJelly)
        {
            currentJelly += 1;
            if (currentJelly == 3)
            {
                Destroy(guardObj);
            }
            else
            {
                guard.sprite = guardPos[currentJelly];
            }        
        }
        else
        {
            SubScript.instance.subHealth = 0;
        }
    }

    // section for the boss key
    public GameObject[] KeyedDoors;

    // open the doors
    public void OpenDoors()
    {
        for(int i = 0; i < KeyedDoors.Length; i++)
        {
            Destroy(KeyedDoors[i]);
        }
    }

    private void Update()
    {
        // section for the first door
        
    }

    private void Start()
    {
        guard.sprite = guardPos[0];
    }
}
