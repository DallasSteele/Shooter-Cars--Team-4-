using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateRunner : MonoBehaviour
{
   public void Shake()
    {
        VibrateHandler.Vibration();
        VibrateHandler.Vibration(220);
        Debug.Log("Did you thought?");
    }
}
