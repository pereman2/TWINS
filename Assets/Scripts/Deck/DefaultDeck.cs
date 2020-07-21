using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DefaultDeck : MonoBehaviour
{
    [SerializeField]
    private Sprite backside;
    [SerializeField]
    private List<Sprite> cardsImages;
    [SerializeField]
    private List<string> categories;

    private void Start()
    {
        StoreInDB();
    }

    private void StoreInDB()
    {
        DeckDataBase.SaveDeck(ToCommonDeck());
    }

    private Deck ToCommonDeck()
    {
        return new Deck("Default", backside, cardsImages, categories);
    }
}
