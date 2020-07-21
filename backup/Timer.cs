using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : Subject
{
    private bool isOnPause;
    [SerializeField]
    private float time;

    public float StartTime
    {
        get;
        set;
    }

    void Start()
    {
        InitializeTimer();
    }

    private void InitializeTimer()
    {
        time = StartTime;
    }

    void Update()
    {
        ManageTimer();
    }

    private void ManageTimer()
    {
        if (!isOnPause)
        {
            time -= Time.deltaTime;
            KeepTimerWithinBounds();
            UpdateUI();
        }
    }

    private void KeepTimerWithinBounds()
    {
        if (time <= 0)
        {
            time = 0;
            Destroy(this);
        }
    }

    private void UpdateUI()
    {
        CountdownTimeManager.Instance.UpdateUI(time);
    }

    public void ResumeTimer()
    {
        isOnPause = false;
    }

    public void StopTimer()
    {
        isOnPause = true;
    }

    private void OnDestroy()
    {
        CountdownTimeManager.Instance.TimerEnds();
    }
}
