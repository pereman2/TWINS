using UnityEngine;

public class Timer : MonoBehaviour
{
    protected bool isOnPause;
    private bool tictacRinging;

    [SerializeField]
    private float time;
    protected float turnElapsedTime;

    public float StartTime { get; set; }
    public float TurnTime { get; set; }

    void Start()
    {
        InitializeTimer();
    }

    private void InitializeTimer()
    {
        time = StartTime;
    }

    public void ResumeTimer()
    {
        isOnPause = false;
    }

    public void StopTimer()
    {
        isOnPause = true;
    }

    public void ResetTurnTimer()
    {
        turnElapsedTime = 0;
    }

    void Update()
    {
        ManageTimer();
    }

    protected virtual void ManageTimer()
    {
        if (!isOnPause)
        {
            time -= Time.deltaTime;
            turnElapsedTime += Time.deltaTime;
            UpdateUI();
            CheckForTimeRunningOut();
            CheckForEndOfTimer();
            CheckForEndOfTurn();
        }
    }

    private void CheckForTimeRunningOut()
    {
        if (time <= 10f && !tictacRinging)
        {
            SFXManager.Instance.PlayTicTac();
            tictacRinging = true;
        }
    }

    protected void UpdateUI()
    {
        (CountdownTimeManager.Instance as CountdownTimeManager)?.UpdateTimerUI(time);
        CountdownTimeManager.Instance.UpdateTurnUI(TurnTime - turnElapsedTime);
    }

    private void CheckForEndOfTimer()
    {
        if (time <= 0)
        {
            time = 0;
            (CountdownTimeManager.Instance as CountdownTimeManager).TimerEnds();
            Destroy(this);
        }
    }

    protected virtual void CheckForEndOfTurn()
    {
        if (turnElapsedTime >= TurnTime)
        {
            CountdownTimeManager.Instance.TurnEnds();
            ResetTurnTimer();
        }
    }
}
