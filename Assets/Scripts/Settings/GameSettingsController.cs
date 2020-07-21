using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsController : MonoBehaviour
{
    [SerializeField]
    private MainMenu mainMenu;
    [SerializeField]
    private Dropdown deckDropdown;
    [SerializeField]
    private Dropdown boardDropdown;
    [SerializeField]
    private InputField timeLimit;
    [SerializeField]
    private GameObject errorMessage;

    [SerializeField]
    private GameObject musicMuteLine;
    [SerializeField]
    private GameObject sfxMuteLine;

    [SerializeField]
    private Toggle timeLimitCheckbox;

    private List<Deck> decks;

    private void OnEnable()
    {
        CheckSounds();
    }

    void Start()
    {
        InitializeConfiguration();
    }

    private void InitializeConfiguration()
    {
        PopulateDecksDropdown();
        boardDropdown.value = 0;
        timeLimitCheckbox.isOn = GameSettings.timeLimit;
        timeLimit.text = GameSettings.time.ToString();
    }

    private void PopulateDecksDropdown()
    {
        deckDropdown.ClearOptions();

        decks = DeckDataBase.GetDecks();

        List<string> deckNames = new List<string>();

        foreach (Deck deck in decks)
            deckNames.Add(deck.Name);

        deckDropdown.AddOptions(deckNames);
        deckDropdown.value = 0;
    }

    public void Reset()
    {
        deckDropdown.value = 0;
        boardDropdown.value = 0;
        timeLimit.text = "60";
        timeLimitCheckbox.isOn = true;
    }

    private void CheckSounds()
    {
        musicMuteLine.SetActive(SFXManager.Instance.AreMusicsMuted());
        sfxMuteLine.SetActive(SFXManager.Instance.AreSFXsMuted());
    }

    public void ToggleInput()
    {
        timeLimit.enabled = timeLimitCheckbox.isOn;

        if (timeLimit.enabled)
            timeLimit.image.color = new Color(1, 1, 1, 1);
        else
            timeLimit.image.color = new Color(1, 1, 1, 0.4f);
    }

    public void SaveSettings()
    {
        try
        {
            Deck deck = decks[deckDropdown.value];

            GameSettings.SaveSettings(deck, GetBoardBackground(), timeLimitCheckbox.isOn, int.Parse(timeLimit.text));

            mainMenu.BackFromMatchSettings();
        }
        catch (FormatException)
        {
            errorMessage.SetActive(true);
        }
    }

    private Sprite GetBoardBackground()
    {
        Sprite sprite = null;

        switch (boardDropdown.value)
        {
            case 0:
                sprite = Resources.Load<Sprite>("Boards/moderno");
                break;
            case 1:
                sprite = Resources.Load<Sprite>("Boards/cibernetico");
                break;
            case 2:
                sprite = Resources.Load<Sprite>("Boards/colorido");
                break;
            case 3:
                sprite = Resources.Load<Sprite>("Boards/florido");
                break;
        }

        return sprite;
    }
}
