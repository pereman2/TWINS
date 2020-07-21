using UnityEngine;
using System.Collections;

public abstract class CountdownBase : MonoBehaviour
{
    private static CountdownBase instance = null;
    public static CountdownBase Instance
    {
        get { return instance; }
    }

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

    protected abstract void InitializeTimer();

    public abstract void StopTimer();
    public abstract void ResumeTimer();
    public abstract void UpdateTurnUI(float time);
    public abstract void TurnEnds();

    public abstract void PlayerMoves();
}
