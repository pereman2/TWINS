using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardMatchType : StandardMatchType
{
    public Image cardToFindImage;

    private CardFinder cardFinder;
    private Card cardToFind;
    public Card CardToFind
    {
        get
        {
            if (cardToFind == null)
                cardToFind = cardFinder.ChooseCardToFind();
            return cardToFind;
        }
        private set { cardToFind = value; }
    }

    void Start()
    {
        cardFinder = new CardFinder(GetBoardCards());
        ShowCardToFindAtMatchStarting();
    }

    private void ShowCardToFindAtMatchStarting()
    {
        ShowCard();
    }

    public void ChangeCardToFind()
    {
        CardToFind = cardFinder.ChooseCardToFind();

        if (CardToFind != null)
            ShowCardToFindAtPlayTime();
    }

    private void ShowCardToFindAtPlayTime()
    {
        StartCoroutine(CardCoroutine());
    }

    private IEnumerator CardCoroutine()
    {
        yield return new WaitForSeconds(0.65f);
        ShowCard();
    }

    private void ShowCard()
    {
        Card card = CardToFind;
        cardToFindImage.sprite = card.Sprite;
    }
}
