using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    private int currentCoins;
    private int startCoins = 50000;
    //public GameObject CurrencyUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //okay,its working//DontDestroyOnLoad(gameObject); //just making sure its working
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        //load saved coins count / start with default
        currentCoins = PlayerPrefs.GetInt("Coins", startCoins);
        UpdateCoinUI();
    }

    //metode menambahkan koin
    public void AddCoins(int amount)
    {
        currentCoins += amount;
        UpdateCoinUI();
        SaveCoins(); //save the coins (updated)
    }

    //metode menggunakan koin
    public bool SpendCoins(int amount)
    {
        if (currentCoins >= amount)
        {
            currentCoins -= amount;
            UpdateCoinUI();
            SaveCoins(); //save coin (after updated)
            return true;
        }
        else
        {
            Debug.Log("Not enough coins!");
            return false;
        }
    }

    //metode mendapatkan koin
    public int GetCurrentCoins()
    {
        return currentCoins;
    }

    // Add coins to the player's total
    public void GetRichQuick(int amount)
    {
        currentCoins += amount;
        CurrencyUI.Instance.UpdateCoinDisplay(currentCoins);
        Debug.Log("Coins Added: " + amount + ". New Total: " + currentCoins);
    }

    //save coin count
    private void SaveCoins()
    {
        PlayerPrefs.SetInt("Coins", currentCoins);
        PlayerPrefs.Save();
    }

    //function called after any update
    private void UpdateCoinUI()
    {
        CurrencyUI.Instance.UpdateCoinDisplay(currentCoins); //update UI
    }

}
