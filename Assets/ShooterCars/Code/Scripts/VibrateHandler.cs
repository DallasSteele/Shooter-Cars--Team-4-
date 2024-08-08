using UnityEngine.UI;
using UnityEngine;
using CandyCoded.HapticFeedback;

public class VibrateHandler : MonoBehaviour
{
    [SerializeField]
    private Button ButtonVibrateHandler;
    [SerializeField]
    private Button lightVibration;
    [SerializeField]
    private Button mediumVibration;
    [SerializeField]
    private Button heavyVibration;


    // Start is called before the first frame update
    private void OnEnable()
    {
        ButtonVibrateHandler.onClick.AddListener(DefaultVibration);

        lightVibration.onClick.AddListener(LightVibration);
        mediumVibration.onClick.AddListener(MediumVibration);
        heavyVibration.onClick.AddListener(HeavyVibration);
    }

    // Update is called once per frame
    private void OnDisable()
    {
        ButtonVibrateHandler.onClick.AddListener(DefaultVibration);
        
        lightVibration.onClick.RemoveListener(LightVibration);
        mediumVibration.onClick.RemoveListener(MediumVibration);
        heavyVibration.onClick.RemoveListener(HeavyVibration);
    }

    private void DefaultVibration()
    {
        Debug.Log("Default Vibration");
        Handheld.Vibrate();
    }

    private void LightVibration()
    {
        Debug.Log("Light Vibration");
        HapticFeedback.LightFeedback();
    }

    private void MediumVibration()
    {
        Debug.Log("Medium Vibration");
        HapticFeedback.MediumFeedback();
    }

    private void HeavyVibration()
    {
        Debug.Log("Heavy Vibration");
        HapticFeedback.HeavyFeedback();
    }

}
