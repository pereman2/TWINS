using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MultiplayerCountdownTimeManager : CountdownBase
{
    [SerializeField]
    private Text turnTimerUI;
    [SerializeField]
    private float turnTime;
    private Timer activeTimer;

    [SerializeField]
    private Text playerTurnText;

    private void Start()
    {
        InitializeTimer();
        playerTurnText.text = "Player 1";
    }

    protected override void InitializeTimer()
    {
        activeTimer = gameObject.AddComponent<MultiplayerTimer>();
        activeTimer.TurnTime = turnTime;
    }

    public override void PlayerMoves()
    {
        ChangePlayerTurn();
        activeTimer.ResetTurnTimer();
    }

    public override void UpdateTurnUI(float time)
    {
        turnTimerUI.text = time.ToString("0");
    }

    public override void ResumeTimer()
    {
        activeTimer.ResumeTimer();
    }

    public override void StopTimer()
    {
        activeTimer.StopTimer();
    }

    public override void TurnEnds()
    {
        MultiplayerScoreManager.Instance.TurnEnds();
        LevelManager.Instance.MultiplayerTurnEnds();
        ChangePlayerTurn();
    }

    private void ChangePlayerTurn()
    {
        PlayerManager.Instance.ChangeActivePlayer();
        playerTurnText.text = PlayerManager.Instance.ActivePlayerName;
    }
}
