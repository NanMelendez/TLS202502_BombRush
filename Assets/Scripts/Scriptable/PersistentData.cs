using UnityEngine;

[CreateAssetMenu(fileName = "PersistentData", menuName = "Scriptable Objects/PersistentData")]
public class PersistentData : ScriptableObject
{
    public bool gameIsBooted;
    public  bool areCreditsUnlocked;

    void Awake()
    {
        gameIsBooted = areCreditsUnlocked = false;
    }
}
