using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustableBool : MonoBehaviour
{
    // Adjustable Event for animation
    public GameObject Animbool;

    // just for activating and closing gameobject, mainly animations
    public void Add()
    {
        Animbool.SetActive(true);
    }
    public void Remove()
    {
        Animbool.SetActive(false);
    }
}
