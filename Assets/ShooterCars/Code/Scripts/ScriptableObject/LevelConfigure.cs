using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "ScriptableObject/Level")]
public class LevelConfigure : ScriptableObject
{
    public int level;
    public GameObject boss;
    public float completeDelay;
}
