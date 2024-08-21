using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputHandler : MonoBehaviour
{
    //Inputs needed for value change
    [SerializeField] private InputField inputField;
    [SerializeField] private TextMeshProUGUI editProfile;
    [SerializeField] private TextMeshProUGUI Name;

    public void Start()
    {
        //load saved name
        string savedName = PlayerPrefs.GetString("name", "");
        if (!string.IsNullOrEmpty(savedName))
        {
            Name.text = savedName;
            inputField.text = savedName;
        }
    }


    public void Validation()
    {

        string input = inputField.text;

        if (string.IsNullOrEmpty(input))
        {
           // editProfile.text = "No Input";
            Debug.Log("No Input");
        }
        else 
        {
           // editProfile.text = "Input success";
            Name.text = input;  // Update Name.text with the input text
            PlayerPrefs.SetString("name", input);  // Save the input text to PlayerPrefs
            Debug.Log("Your Name saved to " + PlayerPrefs.GetString("name"));
        }
    }

    //
}
