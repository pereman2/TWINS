using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckListController : MonoBehaviour
{
    private List<Deck> decks = new List<Deck>();
    [SerializeField]
    private GameObject deckPrefab;

    void Start()
    {
        PopulateDeckList();
    }

    public void PopulateDeckList()
    {
        ClearList();

        foreach (Deck deck in DeckDataBase.GetDecks())
            if (deck.Name != "Default")
                AddDeckToDeckList(deck);
    }

    private void ClearList()
    {
        decks.Clear();

        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    private bool IsDefaultDeckLoaded()
    {
        return decks.Exists((deck) => deck.Name == "Default");
    }

    private void AddDeckToDeckList(Deck deck)
    {
        GameObject deckCell = Instantiate(deckPrefab, transform);

        deckCell.GetComponentInChildren<Text>(true).text = deck.Name;
        deckCell.GetComponent<DeckCellController>().Deck = deck;
    }
}
