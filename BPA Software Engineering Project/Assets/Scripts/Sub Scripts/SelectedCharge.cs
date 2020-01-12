using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedCharge : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TutorialScript.instance.CanSelectedMissile)
        {
            GameControllerScript.instance.SetSelectedMissile("charge");
            GameControllerScript.instance.SetStage("two");
            SceneManager.LoadScene("TwilightZone");
        }
    }
}
