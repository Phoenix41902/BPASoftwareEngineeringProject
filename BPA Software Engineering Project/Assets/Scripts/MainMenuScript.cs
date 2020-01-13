using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void NewGame()
    {
        GameControllerScript.instance.SetStage("one");
        SceneManager.LoadScene("TutorialAndTopZone");
    }

    public void LoadGame()
    {
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

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
