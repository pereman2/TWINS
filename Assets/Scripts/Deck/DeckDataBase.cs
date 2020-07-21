using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DeckDataBase
{
    public static string PersistentDataPath { get; } = Application.persistentDataPath + "/Decks/";

    private static Deck defaultDeck;
    private static List<Deck> decks = new List<Deck>();
    private static BinaryFormatter formatter = new BinaryFormatter();

    public static Deck GetDefaultDeck()
    {
        if (decks.Count == 0)
            GetDecks();

        return decks.Find((deck) => deck.Name == "Default");
    }

    public static List<Deck> GetDecks()
    {
        decks.Clear();

        if (!Directory.Exists(PersistentDataPath))
            Directory.CreateDirectory(PersistentDataPath);

        LoadDecksFromPath();

        return decks;
    }

    private static void LoadDecksFromPath()
    {
        string[] fileNames = Directory.GetFiles(PersistentDataPath);

        for (int i = 0; i < fileNames.Length; i++)
        {
            if (IsFileElegible(fileNames[i]))
            {
                FileStream stream = new FileStream(fileNames[i], FileMode.Open);
                SerializableDeck serializedDeck = formatter.Deserialize(stream) as SerializableDeck;
                stream.Close();

                decks.Add(serializedDeck.ToCommonDeck());
            }
            else
                Debug.LogError("Couldn't open file: " + fileNames[i]
                    + ". Either it's not .JSON or path doesn't exists.");
        }
    }

    private static bool IsFileElegible(string fileName)
    {
        return File.Exists(fileName) && Path.GetExtension(fileName) == ".json";
    }

    public static List<string> GetUniqueCategories()
    {
        List<string> categories = new List<string>();

        foreach (Deck deck in decks)
            foreach (string category in deck.Categories)
                if (!categories.Contains(category.ToUpper().Trim()))
                    categories.Add(category.ToUpper().Trim());

        return categories;
    }

    public static void SaveDeck(Deck deck)
    {
        if (!Directory.Exists(PersistentDataPath))
            Directory.CreateDirectory(PersistentDataPath);

        FileStream stream = new FileStream(PersistentDataPath + deck.Name + ".json", FileMode.Create);

        formatter.Serialize(stream, deck.ToSerializable());
        stream.Close();
    }

    public static void Modify(Deck deck, string previousName)
    {
        Delete(previousName);

        FileStream stream = new FileStream(PersistentDataPath + deck.Name + ".json", FileMode.Create);

        formatter.Serialize(stream, deck.ToSerializable());
        stream.Close();
    }

    public static void Delete(string toDeleteName)
    {
        decks.Remove(decks.Find(deck => deck.Name == toDeleteName));
        File.Delete(PersistentDataPath + toDeleteName + ".json");
    }
}
