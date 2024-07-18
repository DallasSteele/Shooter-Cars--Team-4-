using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nirwana1 : MonoBehaviour
{
    public static nirwana1 instance;

    private void Awake()
    {
        if (nirwana1.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    // Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References
  
    // public Weapon weapon;
    public FloatingTextManager floatingTextManager;

    //Logic
    public int pesos;
    public int experience;

    //Floating text 
    public void ShowText(string msg, int fontsize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontsize, color, position, motion, duration);
    }

    // Save state
    /*
     * INT preseredSkin
     * INT pesos
     * INT experience
     * INT weaponLevel 
     * 
     */

    public void SaveState()
    {
        Debug.Log("SaveState");
    }
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        Debug.Log("LoadState");
    }
}