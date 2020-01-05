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
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // on start
    private void Awake()
    {
        MakeSingleTon();
    }

    // create the arrays of patrol points
    public Transform[] FirstAnglerRoomPoints;
}
