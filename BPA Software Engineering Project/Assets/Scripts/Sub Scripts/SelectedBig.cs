using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedBig : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TutorialScript.instance.CanSelectedMissile)
        {
            GameControllerScript.instance.SetSelectedMissile("big");
            GameControllerScript.instance.SetStage("two");
            SceneManager.LoadScene("TwilightZone");
        }
    }
}
