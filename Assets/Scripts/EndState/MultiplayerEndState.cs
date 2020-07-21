using UnityEngine;
using UnityEngine.UI;

public class MultiplayerEndState : EndState
{
    [SerializeField]
    private Text player1Score;
    [SerializeField]
    private Text player2Score;

    protected override void ShowEndStateWin()
    {
        SetScoreEndState();
        endStateCanvas.SetActive(true);
        LevelManager.Instance.PauseTime();
    }

    protected override void SetScoreEndState()
    {
        MultiplayerScoreManager instance = ScoreManager.Instance as MultiplayerScoreManager;

        player1Score.text = instance.Score.ToString("0");
        player2Score.text = instance.Player2Score.ToString("0");
    }
}
