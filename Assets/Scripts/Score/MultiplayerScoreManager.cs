using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class MultiplayerScoreManager : ScoreManager
{
    [SerializeField]
    private Text player2ScoreUI;

    private int player2Score;
    public int Player2Score
    {
        get { return player2Score; }
        private set { player2Score = value >= 0 ? value : 0; }
    }

    private void Update()
    {
        UpdatePlayersScoreUI();
    }

    private void UpdatePlayersScoreUI()
    {
        scoreUI.text = Score.ToString();
        player2ScoreUI.text = Player2Score.ToString();
    }

    public override void UpdateScoreBy(int update)
    {
        switch (PlayerManager.Instance.ActivePlayer)
        {
            case PlayerManager.Player.player1:
                Score += update;
                break;

            case PlayerManager.Player.player2:
                Player2Score += update;
                break;

            default:
                break;
        }
    }
}
