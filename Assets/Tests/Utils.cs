using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tests
{
    public class Utils
    {
        public static string TestDeckName { get; set; } = "TestDeck";

        public static Deck ArrangeTestDeck()
        {
            string resourcesPath = "DefaultDeck/";
            Sprite backside = Resources.Load<Sprite>(resourcesPath + "Backside/Downside");
            List<Sprite> cardsImages = Resources.LoadAll<Sprite>(resourcesPath + "Sprites/").ToList();
            List<string> categories = SetTestDeckCategories(cardsImages);

            return new Deck(TestDeckName, backside, cardsImages, categories);
        }

        private static List<string> SetTestDeckCategories(List<Sprite> cardsImages)
        {
            List<string> categories = new List<string>();

            for (int i = 0; i < cardsImages.Count; i++)
            {
                string category = cardsImages[i].name + " category";
                categories.Add(category.ToUpper());
            }

            return categories;
        }
    }
}

