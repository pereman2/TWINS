using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Subscriber
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get { return instance; }
    }

    public static int UnlockedLevels { get; set; } 
    public static int CurrentLevel { get; set; }

    [SerializeField]
    private Button[] levels;

    private static Deck deck;
    public Deck Deck
    {
        get { return deck; }
    }

    private SavesManager saves;

    protected void Awake()
    {
        saves = GetComponent<SavesManager>();
        EnsureSingleton();
        if (levels != null && levels[0] != null)
            UpdateLevels();
    }

    public void SetChosenDeck(Deck selectedDeck)
    {
        deck = selectedDeck;
    }

    private void EnsureSingleton()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void UpdateLevels()
    {
        for (int i = 0; i < UnlockedLevels && i < 10; i++)
            levels[i].interactable = true;
    }

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
            UpdateLevels();

        EventChannel.SubscribeTo(CustomEvent.EventType.allPairsFound, this);
    }

    public override void OnEvent(CustomEvent customEvent)
    {
        if (customEvent.Type == CustomEvent.EventType.allPairsFound)
            LevelAchieved();
    }

    public void LevelAchieved()
    {
        bool isOnLevelMode = CurrentLevel != -1;

        if (IsOnLastLevelUnlocked() && isOnLevelMode)
        {
            UnlockedLevels++;
            saves.Save();
        }
    }

    private bool IsOnLastLevelUnlocked()
    {
        return UnlockedLevels == CurrentLevel;
    }
}
