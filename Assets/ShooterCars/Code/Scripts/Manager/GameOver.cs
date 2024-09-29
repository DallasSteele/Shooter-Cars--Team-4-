using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    public gameManager GameManager;

    public void Quit()
    {
        Time.timeScale = 1f;
        GameManager.QuitGame();
    }
}
