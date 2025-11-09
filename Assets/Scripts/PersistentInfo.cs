using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PersistentInfo", menuName = "Scriptable Objects/PersistentInfo")]
public class PersistentInfo : ScriptableObject
{
    private readonly Dictionary<int, uint> starsPerLevel;
    private uint lastLevelUnlocked = 1;
    private bool firstTime = true;

    public void AddStars(int levelID, uint starCount)
    {
        starsPerLevel[levelID] = starCount;
    }

    public uint LevelStars(int levelID)
    {
        return starsPerLevel[levelID];
    }

    public void IncrementUnlockedCount()
    {
        lastLevelUnlocked++;
    }

    public uint GetUnlockedCount()
    {
        return lastLevelUnlocked;
    }
}
