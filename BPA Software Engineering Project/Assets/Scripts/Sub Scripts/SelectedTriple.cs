using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedTriple : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TutorialScript.instance.CanSelectedMissile)
        {
            GameControllerScript.instance.SetSelectedMissile("triple");
            GameControllerScript.instance.SetStage("two");
            SceneManager.LoadScene("TwilightZone");
        }
    }
}
