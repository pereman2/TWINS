using UnityEngine;
using UnityEngine.UI;

public class DefaultEndState : EndState
{
    public void TimerEnds()
    {
        ShowEndStateGameOver();
        SFXManager.Instance.PlayGameOverMusic();
    }

    protected override void ShowEndStateWin()
    {
        ChangeEndStateText("You Win!");
        SetScoreEndState();
        endStateCanvas.SetActive(true);
        LevelManager.Instance.PauseTime();
    }

    private void ShowEndStateGameOver()
    {
        ChangeEndStateText("Game Over");
        SetScoreEndState();
        endStateCanvas.SetActive(true);
        LevelManager.Instance.PauseTime();
    }

    private void ChangeEndStateText(string text)
    {
        Transform child = endStateCanvas.transform.Find("Title");
        Text title = child.GetComponent<Text>();

        title.text = text;
    }

    protected override void SetScoreEndState()
    {
        Transform child = endStateCanvas.transform.Find("Score");
        Text score = child.GetComponent<Text>();

        score.text = ScoreManager.Instance.Score.ToString("0");
    }
}
