using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;


public class FuelManager : MonoBehaviour
{
    //datas
    [SerializeField] public int maxFuel = 6; //fuel capacity
    [SerializeField] public int currentFuel; //now fuel
    [SerializeField] private float fuelRestoreTime = 300f; //5 minutes in seconds
    [SerializeField] private TextMeshProUGUI fuelUIText; //ui text element to show fuel
    private DateTime lastFuelUseTime; //track when fuel was last used

    // Start is called before the first frame update
    private void Start()
    {
        LoadFuelData();
        UpdateFuelUI();

        //start checking fuel restore
        InvokeRepeating(nameof(RestoreFuel), 1f, 60f); //check every minute
    }

    public bool ConsumeFuel()
    {
        if (currentFuel > 0)
        {
            currentFuel--;
            lastFuelUseTime = DateTime.Now;
            SaveFuelData();
            UpdateFuelUI();
            return true; //fuel consumed once
        }
        else
        {
            Debug.Log("Not Enough Fuel!");
            return false;
        }
    }

    private void RestoreFuel()
    {
        if (currentFuel < maxFuel)
        {
            TimeSpan timeSinceLastUse = DateTime.Now - lastFuelUseTime;
            int fuelToRestore = Mathf.FloorToInt((float)timeSinceLastUse.TotalSeconds / fuelRestoreTime);

            if (fuelToRestore > 0)
            {
                currentFuel = Mathf.Min(currentFuel + fuelToRestore, maxFuel);
                lastFuelUseTime = DateTime.Now; //reset the last fuel usage
                SaveFuelData();
                UpdateFuelUI();
            }
        }

    }

    private void UpdateFuelUI()
    {
        //the alternative version | fuelUIText.text = "Fuel: " + currentFuel.ToString() + "/" + maxFuel.ToString();
        fuelUIText.text = $"Fuel: {currentFuel}/{maxFuel}";
    }

    private void LoadFuelData()
    {
        //load fuel from save
        currentFuel = PlayerPrefs.GetInt("Fuel", maxFuel);
        lastFuelUseTime = DateTime.Parse(PlayerPrefs.GetString("LastFuelUse", DateTime.Now.ToString()));
    }

    private void SaveFuelData()
    {
        //save to saves
        PlayerPrefs.SetInt("Fuel", currentFuel);
        PlayerPrefs.SetString("LastFuelUse", lastFuelUseTime.ToString());
        PlayerPrefs.Save();
    }
}