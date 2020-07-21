using System.Collections.Generic;
using System.Linq;

public abstract class Finder
{
    protected static System.Random rng = new System.Random();
    protected List<Card> elegibleCards = new List<Card>();

    public Finder(List<Card> cards)
    {
        elegibleCards = cards.OrderBy(a => rng.Next()).ToList();
    }

    protected Card ChooseElementToFind()
    {
        if (elegibleCards.Count > 0)
        {
            Card chosen = elegibleCards[0];

            RemovePairFromElegibleCards(chosen);

            return chosen;
        }
        return null;
    }

    protected abstract void RemovePairFromElegibleCards(Card chosen);
}
