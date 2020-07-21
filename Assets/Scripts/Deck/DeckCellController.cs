using System.Collections.Generic;
using UnityEngine;

public class DeckCellController : MonoBehaviour
{
    private DeckMenuController menuController;

    private Deck deck;
    public Deck Deck
    {
        get;
        set;
    }

    private void Start()
    {
        menuController = FindObjectOfType<DeckMenuController>();
    }

    public void SelectDeck()
    {
        if (TWINSSceneManager.Instance.IsGameOnDeckCustomizationMenu())
            Edit();
        else
            GameManager.Instance.SetChosenDeck(Deck); 
    }

    private void Edit()
    {
        GameObject customizationCanvas = menuController.OpenCustomizationEdit();
        customizationCanvas.GetComponent<DeckCustomization>().EditDeck(Deck);
    }

    public void Erase()
    {
        DeckDataBase.Delete(Deck.Name);
        Destroy(gameObject);
    }
}
