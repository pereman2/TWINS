using UnityEngine;

public class GameStartSettings : MonoBehaviour
{
    void Start()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        CountdownTimeManager timeManager = CountdownTimeManager.Instance as CountdownTimeManager;

        GameObject board = GameObject.Find("Background");
        board.GetComponent<SpriteRenderer>().sprite = GameSettings.board;

        if (GameSettings.timeLimit)
            timeManager?.SetStartTime(GameSettings.time);
        else
            timeManager?.NoTimeLimit();
    }
}
