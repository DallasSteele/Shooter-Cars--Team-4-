using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class FuelUI : MonoBehaviour
{
    //datas
    [SerializeField] private FuelManager fuelManager;
    [SerializeField] private TextMeshProUGUI fuelText;
    [SerializeField] private Slider fuelSlider; //optional if r wanna represent fuel amount in a slider

    // Update is called once per frame
    private void Update()
    {
        //update UI in realtime
        //this is the alternative | fuelText.text = "Fuel: " + fuelManager.CurrentFuel.ToString();
        fuelText.text = $"Fuel: {fuelManager.currentFuel}";
        fuelSlider.value = (float)fuelManager.currentFuel / fuelManager.maxFuel;
    }
}
