using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseMenu : MonoBehaviour
{
    public static bool PausingGame = false;

    public GameObject PausePanel;
    public gameManager GameManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PausingGame)
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
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        PausingGame = false;
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        PausingGame = true;
    }

    public void Quit()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        GameManager.QuitGame();

    }
}
