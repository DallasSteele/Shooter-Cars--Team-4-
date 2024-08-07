using UnityEngine.UI;
using UnityEngine;

public class VibrateHandler : MonoBehaviour
{
    [SerializeField]
    private Button ButtonVibrateHandler;

    // Start is called before the first frame update
    private void OnEnable()
    {
        ButtonVibrateHandler.onClick.AddListener(DefaultVibration);
    }

    // Update is called once per frame
    private void OnDisable()
    {
        ButtonVibrateHandler.onClick.AddListener(DefaultVibration);
    }

    private void DefaultVibration()
    {
        Debug.Log("Default Vibration");
        Handheld.Vibrate();
    }
}
