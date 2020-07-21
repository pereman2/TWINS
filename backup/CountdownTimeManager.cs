using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class CountdownTimeManager : Subject
{
    private static CountdownTimeManager instance = null;
    public static CountdownTimeManager Instance
    {
        get { return instance; }
    }

    [SerializeField]
    private Text timerUI;
    [SerializeField]
    private float startTime;
    private Timer activeTimer;

    private void Awake()
    {
        EnsureSingleton();
    }

    private void EnsureSingleton()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        InitializeTimer();

        Assert.AreNotEqual(startTime, 0, "The start time for the timer shouldn't be 0. Check its value on the CountdownTimeManager.");
    }

    private void InitializeTimer()
    {
        activeTimer = gameObject.AddComponent<Timer>();
        activeTimer.StartTime = startTime;
    }

    void Update()
    {

    }

    public void UpdateUI(float time)
    {
        timerUI.text = time.ToString("0");
    }

    public void ResumeTimer()
    {
        activeTimer.ResumeTimer();
    }

    public void StopTimer()
    {
        activeTimer.StopTimer();
    }

    public void TimerEnds()
    {
        activeTimer = null;
        Notify(new CustomEvent(this, CustomEvent.EventType.timerEnds));
    }
}
