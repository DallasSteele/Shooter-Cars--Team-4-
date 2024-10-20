using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyUI : MonoBehaviour
{
    public static CurrencyUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI coinText;

    //this covers the awake to open the private state of CurrencyUI
    private void Awake ()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Initialize UI with current coins
        UpdateCoinDisplay(CoinManager.Instance.GetCurrentCoins());
    }

    //metode update display coin
    public void UpdateCoinDisplay(int coins)
    {
        Debug.Log("Updating Coin Display: " + coins); // Add this to check
        coinText.text = $"Coins: {coins}";
    }


}
