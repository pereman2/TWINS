using UnityEngine;
using System.Collections;

[System.Serializable]
internal class GameData
{
    public int UnlockedLevels
    {
        get;
        set;
    }

    public GameData(int unlockedLevels)
    {
        UnlockedLevels = unlockedLevels;
    }
}
