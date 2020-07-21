using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using System;

public class CardMatchRecognition : MatchRecognition
{
    [SerializeField]
    private CardMatchType cardMatchType;

    protected new void Start()
    {
        base.Start();

        Assert.IsNotNull(cardMatchType, "CardMatchRecognition.cardMatchType is null, " +
            "look if its correctly referenced on editor");
    }

    protected override bool IsPairLegit()
    {
        if (CardsMeetCardMatchRequirements())
            return true;

        return false;
    }

    private bool CardsMeetCardMatchRequirements()
    {
        Card toFind = cardMatchType.CardToFind;

        return CardsHaveSameID() && (firstCard == toFind || secondCard == toFind);
    }

    protected override void PairSuccess()
    {
        cardsFlipped += 2;
        cardMatchType.ChangeCardToFind();
    }
}
