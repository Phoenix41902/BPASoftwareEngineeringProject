using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    // keep track of pause
    public static bool GameIsPaused = false;

    // pause menu UI
    public GameObject PauseMenuUI;

    // fade to next level
    public Animator anim;

    IEnumerator fadeOut()
    {
        Time.timeScale = 1f;
        anim.SetTrigger("End");
        yield return new WaitForSeconds(3f);      
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // resume
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // pause
    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    // Load menu
    public void LoadMenu()
    {
        // ADD WHEN MENU
        StartCoroutine(fadeOut());
    }

    // Quit game
    public void QuitGame()
    {
        Application.Quit();
    }
}
