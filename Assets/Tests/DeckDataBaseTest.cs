using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace Tests
{
    public class DeckDataBaseTest
    {
        Deck testDeck;

        [SetUp]
        public void SetUp()
        {
            testDeck = Utils.ArrangeTestDeck();
        }

        [Test]
        public void Deck_Is_Serialized()
        {
            DeckDataBase.SaveDeck(testDeck);

            FileAssert.Exists(DeckDataBase.PersistentDataPath + testDeck.Name + ".json");
        }

        [Test]
        public void Deck_Is_Deserialized()
        {
            DeckDataBase.SaveDeck(testDeck);
            List<Deck> decks = DeckDataBase.GetDecks();

            Deck deserialized = decks.Find(deck => deck.Name == testDeck.Name);
            Assert.True(testDeck.Equals(deserialized));
        }

        [Test]
        public void Deck_Is_Modified()
        {
            string previousName = testDeck.Name;
            testDeck.Name = "ModifiedDeck";

            DeckDataBase.Modify(testDeck, previousName);

            List<Deck> decks = DeckDataBase.GetDecks();
            Assert.True(decks.Exists(deck => deck.Name == testDeck.Name));
            Assert.False(decks.Exists(deck => deck.Name == previousName));
            FileAssert.Exists(DeckDataBase.PersistentDataPath + testDeck.Name + ".json");
            FileAssert.DoesNotExist(DeckDataBase.PersistentDataPath + previousName + ".json");
        }

        [Test]
        public void Unique_Categories_Are_Retrieved()
        {
            for (int i = 0; i < 10; i++)
            {
                Deck testDeck = Utils.ArrangeTestDeck();
                testDeck.Name = testDeck.Name + i;
                DeckDataBase.SaveDeck(testDeck);
            }

            List<Deck> testDecks = DeckDataBase.GetDecks();
            List<string> uniqueCategories = DeckDataBase.GetUniqueCategories();

            foreach (Deck testDeck in testDecks)
                foreach (string category in testDeck.Categories)
                    Assert.Contains(category, uniqueCategories);
        }

        [TearDown]
        public void TearDown()
        {
            foreach (Deck testDeck in DeckDataBase.GetDecks())
                if (testDeck.Name.StartsWith(Utils.TestDeckName))
                    File.Delete(DeckDataBase.PersistentDataPath + testDeck.Name + ".json");

            File.Delete(DeckDataBase.PersistentDataPath + "ModifiedDeck.json");
        }
    }
}
