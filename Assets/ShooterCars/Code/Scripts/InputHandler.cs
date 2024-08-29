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

    //public start
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

    //public confirm update change
    public void Validate()
    {
        //declare input to inputfield
        string input = inputField.text;

        if (string.IsNullOrEmpty(input))
        {
            //editProfile.text = "No Input";
            Debug.Log("No Input");
        }
        else
        {
            //editProfile.text = "Input Success";
            Name.text = input; //input Name.text with the input text
            PlayerPrefs.SetString("name", input); //save the input to playerprefs
            Debug.Log("Your Name is saved to " + PlayerPrefs.GetString("name"));

        }
    }
}
