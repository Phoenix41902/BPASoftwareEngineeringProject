using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbyssalController : MonoBehaviour
{
    // create instance
    public static AbyssalController instance;

    // Key alg
    public int KeyCounter = 4;
    public Text GateAlert;
    public GameObject[] FirstBossGate;

    IEnumerator KeyMessage()
    {
        GateAlert.text = "BOSS GATE OPEN";
        yield return new WaitForSeconds(10f);
        GateAlert.text = "";
    }

    private void Update()
    {
        if (KeyCounter == 0)
        {
            StartCoroutine(KeyMessage());
            for (int i = 0; i < FirstBossGate.Length; i++)
            {
                Destroy(FirstBossGate[i]);
            }
            KeyCounter--;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    // section for the mini boss
    public bool HasEnteredMiniBossRoom = false;

    // section for final boss
    public bool FinalBossStarted;

}
