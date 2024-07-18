using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class mainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public gameManager gameManager;

    //the play button
    public void PlayGame()
    {
        //forget this, it was a scene jumper
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        gameManager.PlayGame();
    }

    //the quit button
    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    //fullscreen
 //   public void SetFullscreen(bool isFullscreen)
 //   {
 //       Screen.fullScreen = isFullscreen;
 //  }

}
