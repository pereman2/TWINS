using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardFinder : Finder
{
    public CardFinder(List<Card> cards) : base(cards)
    {
    }

    public Card ChooseCardToFind()
    {
        return ChooseElementToFind();
    }

    protected override void RemovePairFromElegibleCards(Card choosen)
    {
        elegibleCards.RemoveAll(card => card.PairID == choosen.PairID);
    }
}
