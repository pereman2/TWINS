using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck
{
    public string Name { get; set; }
    public Sprite backSide { get; set; }
    public List<Sprite> CardsImages { get; set; }
    public List<string> Categories { get; }

    public Deck(string name, Sprite backSide, List<Sprite> cardsImages, List<string> categories)
    {
        this.backSide = backSide;
        Name = name;
        CardsImages = cardsImages;
        Categories = categories;
    }

    public SerializableDeck ToSerializable()
    {
        return new SerializableDeck(this);
    }

    public new bool Equals(object obj)
    {
        Deck toCompare = obj as Deck;

        if (toCompare == null)
            return false;

        bool sameName = toCompare.Name == Name;
        bool sameCardsImages = toCompare.CardsImages.Count == CardsImages.Count;
        bool sameCategories = toCompare.Categories.SequenceEqual(Categories);

        return sameName && sameCardsImages && sameCategories;
    }
}
