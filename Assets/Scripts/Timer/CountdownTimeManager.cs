using UnityEngine;
using UnityEngine.UI;

public class CountdownTimeManager : CountdownBase
{
    [SerializeField]
    private Text timerUI;
    [SerializeField]
    private Text turnTimerUI;
    [SerializeField]
    private float startTime;
    [SerializeField]
    private float turnTime;
    private Timer activeTimer;

    [SerializeField]
    private GameObject turnTimeAllUI;
    [SerializeField]
    private GameObject timeAllUI;

    private void Start()
    {
        InitializeTimer();
    }

    public void SetStartTime(int time)
    {
        startTime = time;
    }

    public void NoTimeLimit()
    {
        timeAllUI.SetActive(false);
        turnTimeAllUI.SetActive(false);
        enabled = false;
    }

    protected override void InitializeTimer()
    {
        activeTimer = gameObject.AddComponent<Timer>();
        activeTimer.StartTime = startTime;
        activeTimer.TurnTime = turnTime;
    }

    public override void PlayerMoves()
    {
        activeTimer.ResetTurnTimer();
    }

    public void UpdateTimerUI(float time)
    {
        timerUI.text = time.ToString("0");
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

    public void TimerEnds()
    {
        activeTimer = null;
        SFXManager.Instance.StopTicTac();
        LevelManager.Instance.TimerEnds();
    }

    public override void TurnEnds()
    {
        ScoreManager.Instance.TurnEnds();
    }
}
