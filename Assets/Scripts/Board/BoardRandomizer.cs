using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BoardRandomizer : MonoBehaviour
{
    [SerializeField]
    private GameObject cardParent;

    void Start()
    {
        Shuffle();
    }

    private void Shuffle()
    {
        List<GameObject> cards = GetCardsInParent();

        foreach (GameObject card in cards)
        {
            int index = cards.IndexOf(card);
            int randomInt = Random.Range(0, cards.Count);

            Vector2 originalPosition = cards[index].transform.position;
            cards[index].transform.position = cards[randomInt].transform.position;
            cards[randomInt].transform.position = originalPosition;
        }
    }

    private List<GameObject> GetCardsInParent()
    {
        List<GameObject> cards = new List<GameObject>();

        foreach (Card card in cardParent.GetComponentsInChildren<Card>().ToList())
            cards.Add(card.gameObject);

        return cards;
    }
}
