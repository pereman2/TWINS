using UnityEngine;
using UnityEngine.Assertions;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance = null;
    public static LevelManager Instance
    {
        get { return instance; }
    }

    [SerializeField]
    private DefaultEndState endState;
    [SerializeField]
    private Pause pause;
    [SerializeField]
    private MatchRecognition matchRecognition;

    private Deck deck;

    void Awake()
    {
        EnsureSingleton();
    }

    private void Start()
    {
        Assert.IsNotNull(pause, "LevelManager.pause is null");
        Assert.IsNotNull(matchRecognition, "LevelManager.matchRecognition is null");
    }

    private void EnsureSingleton()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public bool AbleToFlip()
    {
        return matchRecognition.AbleToFlip();
    }
    
    public void MultiplayerTurnEnds()
    {
        (matchRecognition as MultiplayerMatchRecognition).FlipbackUpsideCards();
    }

    public void TimerEnds()
    {
        endState.TimerEnds();
    }

    public void SetChoosenDeck(Deck deck)
    {
        this.deck = deck;
    }

    public void PlayerMoves()
    {
        CountdownBase.Instance.PlayerMoves();
    }

    public void MatchedPairSFX()
    {
        SFXManager.Instance.PlayMatchingSFX();
    }

    public bool IsGamePaused()
    {
        return pause.GamePaused;
    }

    public void PauseTime()
    {
        pause.PauseTime();
    }

    public void MuteAll()
    {
        SFXManager.Instance.MuteAll();
    }

    public void MuteMusic()
    {
        SFXManager.Instance.MuteMusic();
    }

    public void MuteSFXs()
    {
        SFXManager.Instance.MuteSFXs();
    }

    public void ButtonSFX()
    {
        SFXManager.Instance.PlayButtonSFX();
    }
}



