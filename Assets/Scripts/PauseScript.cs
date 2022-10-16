using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public static bool gameIsPaused;
    public GameObject pauseMenu;

    void Start()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.isGameActive)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        ShowHidePauseMenu(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    void Resume()
    {
        ShowHidePauseMenu(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void ShowHidePauseMenu(bool onOff)
    {
        pauseMenu.SetActive(onOff);
        GameManager.instance.volumeSlider.transform.parent.gameObject.SetActive(onOff);
        GameManager.instance.restartButton.SetActive(onOff);
    }
}
