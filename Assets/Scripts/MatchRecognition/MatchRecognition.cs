using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MatchRecognition : Subscriber
{
    private List<Card> cardHistory = new List<Card>();
    private int timesFailingWithSameCard;

    protected Card firstCard;
    protected Card secondCard;
    protected int cardsFlipped = 0;
    private int numberOfCards = 0;

    [SerializeField]
    private GameObject cards;

    protected void Start()
    {
        numberOfCards = GetNumberOfCards();
        EventChannel.SubscribeTo(CustomEvent.EventType.cardFlipsToFrontside, this);
    }

    private int GetNumberOfCards()
    {
        return cards.GetComponentsInChildren<Card>().Length;
    }

    public bool AbleToFlip()
    {
        return secondCard == null;
    }

    public override void OnEvent(CustomEvent customEvent)
    {
        CardFlippedToFrontside(customEvent.Sender as Card);
    }

    public void CardFlippedToFrontside(Card flippledCard)
    {
        if (IsFirstCardOnPair())
        {
            firstCard = flippledCard;
            firstCard.DisableFlip();
        }
        else if (flippledCard != firstCard)
        {
            secondCard = flippledCard;
            CheckForMatchedPair();
            LevelManager.Instance.PlayerMoves();
        }
    }

    private bool IsFirstCardOnPair()
    {
        return firstCard == null;
    }

    private void CheckForMatchedPair()
    {
        if (IsPairLegit())
            MatchedPairFound();
        else
            MatchedPairNotFound();
    }

    protected abstract bool IsPairLegit();

    protected bool CardsHaveSameID()
    {
        return firstCard.PairID == secondCard.PairID;
    }

    private void MatchedPairFound()
    {
        PairSuccess();
        DisablePairFlipping();

        CleanUpCardPairReferences();
        ClearCardHistory();

        ScoreManager.Instance.UpdateScoreBy(10);
        LevelManager.Instance.MatchedPairSFX();

        CheckForMatchEnd();
    }

    protected abstract void PairSuccess();

    private void DisablePairFlipping()
    {
        firstCard?.DisableFlip();
        secondCard?.DisableFlip();
    }

    protected void CleanUpCardPairReferences()
    {
        firstCard = null;
        secondCard = null;
    }

    private void ClearCardHistory()
    {
        cardHistory.Clear();
        timesFailingWithSameCard = 0;
    }

    private void CheckForMatchEnd()
    {
        if (cardsFlipped == numberOfCards)
            EventChannel.Send(new CustomEvent(CustomEvent.EventType.allPairsFound));
    }

    private void MatchedPairNotFound()
    {
        firstCard?.EnableFlip();
        StartCoroutine(WaitAndFlipBack(1));

        if (FailingAgainWithSameCard())
        {
            timesFailingWithSameCard++;
            int penalizationMultiplier = timesFailingWithSameCard + 1;

            ScoreManager.Instance.UpdateScoreBy(penalizationMultiplier * (-1));
        }
        else
        {
            ClearCardHistory();

            ScoreManager.Instance.UpdateScoreBy(-1);
        }

        cardHistory.Add(firstCard);
        cardHistory.Add(secondCard);
    }

    private IEnumerator WaitAndFlipBack(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        FlipLastPair();
        CleanUpCardPairReferences();
    }

    private bool FailingAgainWithSameCard()
    {
        if (cardHistory.Count == 0)
            return false;
        return IsAnyCardInPairFoundInHistory();
    }

    private bool IsAnyCardInPairFoundInHistory()
    {
        return cardHistory[cardHistory.Count - 1] == firstCard || cardHistory[cardHistory.Count - 1] == secondCard
            || cardHistory[cardHistory.Count - 2] == firstCard || cardHistory[cardHistory.Count - 2] == secondCard;
    }

    private void FlipLastPair()
    {
        firstCard?.FlipCard();
        secondCard?.FlipCard();
    }
}