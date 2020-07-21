using UnityEngine;
using UnityEngine.Assertions;

public class CategoryMatchRecognition : MatchRecognition
{
    [SerializeField]
    private CategoryMatchType categoryMatchType;

    protected new void Start()
    {
        base.Start();

        Assert.IsNotNull(categoryMatchType, "CategoryMatchType.categoryMatchType is null, " +
            "look if its correctly referenced on editor");
    }

    protected override bool IsPairLegit()
    {
        if (MatchingCategory())
            return true;

        return false;
    }

    private bool MatchingCategory()
    {
        string toFind = categoryMatchType.CategoryToFind;

        return CardsHaveSameID() && (firstCard.Category == toFind && secondCard.Category == toFind);
    }

    protected override void PairSuccess()
    {
        cardsFlipped += 2;
        categoryMatchType.MatchFound(firstCard, secondCard);
    }
}
