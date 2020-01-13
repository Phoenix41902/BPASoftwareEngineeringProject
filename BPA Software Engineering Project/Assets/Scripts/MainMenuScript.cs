using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Animator anim;

    IEnumerator fadeOutLoad()
    {
        anim.SetTrigger("End");
        yield return new WaitForSeconds(3f);
        if (GameControllerScript.instance.GetStage() == "one")
        {
            SceneManager.LoadScene("TutorialAndTopZone");
        }
        else if (GameControllerScript.instance.GetStage() == "two")
        {
            SceneManager.LoadScene("TwilightZone");
        }
        else if (GameControllerScript.instance.GetStage() == "three")
        {
            SceneManager.LoadScene("AbyssalZone");
        }
    }

    IEnumerator fadeOutNew()
    {
        anim.SetTrigger("End");
        yield return new WaitForSeconds(3f);
        GameControllerScript.instance.SetStage("one");
        SceneManager.LoadScene("TutorialAndTopZone");
    }

    IEnumerator fadeOutMenu()
    {
        anim.SetTrigger("End");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }

    public void NewGame()
    {
        StartCoroutine(fadeOutNew());
    }

    public void LoadGame()
    {
        StartCoroutine(fadeOutLoad());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        StartCoroutine(fadeOutMenu());
    }
}
