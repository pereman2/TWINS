using System.Collections.Generic;
using System.Linq;

public class CategoryFinder : Finder
{
    public CategoryFinder(List<Card> cards) : base(cards)
    {
    }

    public string ChooseNewCategoryToFind()
    {
        Card chosen = ChooseElementToFind();

        if (chosen != null)
            return chosen.Category;

        return null;
    }

    protected override void RemovePairFromElegibleCards(Card chosen)
    {
        elegibleCards.RemoveAll(card => card.Category == chosen.Category);
    }
}
