using UnityEngine;
using System.Collections;

public class MultiplayerTimer : Timer
{
    protected override void ManageTimer()
    {
        if (!isOnPause)
        {
            turnElapsedTime += Time.deltaTime;
            UpdateUI();
            CheckForEndOfTurn();
        }
    }
}
