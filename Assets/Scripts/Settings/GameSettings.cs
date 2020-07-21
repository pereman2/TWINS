using UnityEngine;

public class GameSettings
{
    public static Deck deck = DeckDataBase.GetDefaultDeck();
    public static Sprite board = Resources.Load<Sprite>("Boards/moderno");
    public static bool timeLimit = true;
    public static int time = 60;

    public static void SaveSettings(Deck deck, Sprite board, bool timeLimit, int time)
    {
        GameSettings.deck = deck;
        GameSettings.board = board;
        GameSettings.timeLimit = timeLimit;
        GameSettings.time = time;

        GameManager.Instance.SetChosenDeck(deck);
    }
}
