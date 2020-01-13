using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedTriple : MonoBehaviour
{
    // fade to next level
    public Animator anim;

    IEnumerator fadeOut()
    {
        anim.SetTrigger("End");
        yield return new WaitForSeconds(3f);
        GameControllerScript.instance.SetSelectedMissile("triple");
        GameControllerScript.instance.SetStage("two");
        SceneManager.LoadScene("TwilightZone");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TutorialScript.instance.CanSelectedMissile)
        {
            StartCoroutine(fadeOut());  
        }
    }
}
