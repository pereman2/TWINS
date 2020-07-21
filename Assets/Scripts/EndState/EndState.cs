using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class EndState : Subscriber
{
    [SerializeField]
    protected GameObject endStateCanvas;

    protected void Start()
    {
        Assert.IsNotNull(endStateCanvas, "endStateCanvas is null on EndState. Check if it's properly referenced.");

        EventChannel.SubscribeTo(CustomEvent.EventType.allPairsFound, this);
    }

    public override void OnEvent(CustomEvent customEvent)
    {
        AllPairsFound();
    }

    private void AllPairsFound()
    {
        ShowWinState();
        SFXManager.Instance.PlayWinStateMusic();
    }

    private void ShowWinState()
    {
        StartCoroutine(WaitAndShowWin(1));
    }

    private IEnumerator WaitAndShowWin(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        ShowEndStateWin();
    }

    protected abstract void ShowEndStateWin();
    protected abstract void SetScoreEndState();
}
