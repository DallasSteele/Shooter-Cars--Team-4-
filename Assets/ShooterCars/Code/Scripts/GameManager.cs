using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject startMenuPanel;
    public GameObject inGameUIPanel;
    public bool isGameActive = false;
    public Button playButton; //this for the start button
    public Button quitButton; //this for the quit button
    public carMovement carMovement;


    // Start is called before the first frame update
    private void Start()
    {
        //Disable the game
        DisableGameplay();
        playButton.onClick.AddListener(PlayGame); //listener to play
        quitButton.onClick.AddListener(QuitGame); //listener to quit
    }

    public void PlayGame()
    {
        //Enable the game, interactible
        EnableGameplay();
        startMenuPanel.SetActive(false);
        inGameUIPanel.SetActive(true);
        isGameActive = true;
        ShooterCar.Manager.GameController.Instance.OnGameStart();
    }

    private void DisableGameplay()
    {
        //Disable game objects and inputs
        //Disable the player controller, camera, and any other interact elements
        startMenuPanel.SetActive(true);
        inGameUIPanel.SetActive(false);
        
        isGameActive=false;
    }

    private void EnableGameplay()
    {
        //Enable game objects and components here
        //Enable the player controls and everything users use for the game
        startMenuPanel.SetActive(false);
        inGameUIPanel.SetActive(true);
    }

    public void QuitGame()
    {
        //Disable gameplay
        DisableGameplay();

        //Reset game variables and states
        //If any
        SceneManager.LoadScene("SampleScene 1");
    }

}
