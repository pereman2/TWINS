using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject modeSelectionMenu;
    [SerializeField]
    private GameObject matchSelectionMenu;
    [SerializeField]
    private GameObject levelSelectionMenu;
    [SerializeField]
    private GameObject matchSettings;

    private GameMode mode;
    private SubMode subMode;

    private void Start()
    {
        CheckSoundsMute();
    }

    public enum GameMode
    {
        levels,
        card,
        standard,
        category,
        multiplayer
    }

    public enum SubMode
    {
        card,
        standard,
        category
    }

    public void GoToModeSelection()
    {
        SwapMenus(mainMenu, modeSelectionMenu);
    }

    public void GoToMatchSettings()
    {
        SwapMenus(matchSelectionMenu, matchSettings);
    }

    public void BackFromMatchSettings()
    {
        SwapMenus(matchSettings, matchSelectionMenu);
    }

    public void GoToLevelSelection()
    {
        SwapMenus(modeSelectionMenu, levelSelectionMenu);
    }

    public void BackFromLevelSelection()
    {
        SwapMenus(levelSelectionMenu, modeSelectionMenu);
    }

    public void GoToMatchSelection()
    {
        GameManager.Instance.SetChosenDeck(GameSettings.deck);
        SwapMenus(modeSelectionMenu, matchSelectionMenu);
    }

    public void BackFromMatchSelection()
    {
        SwapMenus(matchSelectionMenu, modeSelectionMenu);
    }

    public void BackFromModeSelection()
    {
        SwapMenus(modeSelectionMenu, mainMenu);
        CheckSoundsMute();
    }

    public void Exit()
    {
        TWINSSceneManager.Instance.ExitGame();
    }

    public void SetModeLevels()
    {
        mode = GameMode.levels;
    }

    public void SetModeStandard()
    {
        mode = GameMode.standard;
    }

    public void SetModeCard()
    {
        mode = GameMode.card;
    }

    public void SetModeCategory()
    {
        mode = GameMode.category;
    }

    public void SetModeMultiplayer()
    {
        mode = GameMode.multiplayer;
    }

    public void StartGame()
    {
        GameManager.CurrentLevel = -1;

        switch (mode)
        {
            case GameMode.standard:
                TWINSSceneManager.Instance.StartStandardGame();
                break;

            case GameMode.card:
                TWINSSceneManager.Instance.StartCardGame();
                break;

            case GameMode.category:
                TWINSSceneManager.Instance.StartCategoryGame();
                break;

            case GameMode.multiplayer:
                GameManager.Instance.SetChosenDeck(DeckDataBase.GetDefaultDeck());
                TWINSSceneManager.Instance.StartMultiplayerStandardGame();
                break;
        }
    }

    public void StartLevelGame(int levelNumber)
    {
        GameManager.CurrentLevel = levelNumber;
        TWINSSceneManager.Instance.StartLevelGame(levelNumber);
    }

    public void SetStandardSubmode()
    {
        subMode = SubMode.standard;
    }

    public void SetCardSubmode()
    {
        subMode = SubMode.card;
    }

    public void SetCategorySubmode()
    {
        subMode = SubMode.category;
    }

    private void SwapMenus(GameObject oldMenu, GameObject newMenu)
    {
        ToggleMenu(oldMenu);
        ToggleMenu(newMenu);
    }

    private void ToggleMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeInHierarchy);
    }

    private void CheckSoundsMute()
    {
        Transform muteButton = mainMenu.transform.Find("MuteAllButton");

        if (SFXManager.Instance.AreSFXsMuted() && SFXManager.Instance.AreMusicsMuted())
            muteButton.GetChild(0).gameObject.SetActive(true);
        else
            muteButton.GetChild(0).gameObject.SetActive(false);
    }
}
