using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    // Development for Pause menu is based on tutorial video made by Brackeys in YouTube

    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject image;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        image.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        image.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitMenu()
    {
        pauseMenuUI.SetActive(false);
        image.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("Main Menu");
    }
}
