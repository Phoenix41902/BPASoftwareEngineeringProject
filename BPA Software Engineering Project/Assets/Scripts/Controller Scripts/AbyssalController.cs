using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssalController : MonoBehaviour
{
    // create instance
    public static AbyssalController instance;

    private void Awake()
    {
        instance = this;
    }

    // section for the mini boss
    public bool HasEnteredMiniBossRoom = false;

    // section for the patroling anglers

}
