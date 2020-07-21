using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CategoryMatchType : StandardMatchType
{
    [SerializeField]
    private Text categoryToFindUI;

    private CategoryFinder categoryFinder;

    public string CategoryToFind
    {
        get;
        private set; 
    }

    private List<Card> remainingCardsInCategory = new List<Card>();

    void Start()
    {
        categoryFinder = new CategoryFinder(GetBoardCards());
        ShowNewCategoryToFind();
    }

    private void ShowNewCategoryToFind()
    {
        CategoryToFind = categoryFinder.ChooseNewCategoryToFind();
        remainingCardsInCategory = GetCardsInCategory();

        categoryToFindUI.text = CategoryToFind;
    }

    public void MatchFound(Card first, Card second)
    {
        remainingCardsInCategory.Remove(first);
        remainingCardsInCategory.Remove(second);

        if (remainingCardsInCategory.Count == 0)
            ShowNewCategoryToFind();
    }

    private List<Card> GetCardsInCategory()
    {
        List<Card> cardsInCategory = new List<Card>();

        foreach (Card card in GetBoardCards())
            if (card.Category == CategoryToFind)
                cardsInCategory.Add(card);

        return cardsInCategory;
    }
}
