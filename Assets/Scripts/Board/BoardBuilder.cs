using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardBuilder : MonoBehaviour
{
    [SerializeField]
    private float distanceInX;
    [SerializeField]
    private float distanceInY;

    [SerializeField]
    private GameObject north;
    [SerializeField]
    private GameObject south;
    [SerializeField]
    private GameObject east;
    [SerializeField]
    private GameObject west;

    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private float scale;
    [SerializeField]
    private GameObject cardParent;

    private Deck deck;
    private Sprite backsideSprite;
    private List<GameObject> cards = new List<GameObject>();
    private Dictionary<Sprite, string> spritesCategory = new Dictionary<Sprite, string>();
    private readonly int numberOfPairsInBoard = 6;

    void Start()
    {
        deck = GameManager.Instance.Deck;

        GetCardsFromDeck();
        BuildCards();
        DistributeCardsInBoard();
    }

    private void GetCardsFromDeck()
    {
        List<string> categories = deck.Categories;
        List<Sprite> cardsImages = deck.CardsImages;

        backsideSprite = deck.backSide;

        for (int i = 0; i < cardsImages.Count; i++)
            spritesCategory.Add(cardsImages[i], categories[i]);
    }

    private void BuildCards()
    {
        List<Sprite> sprites = spritesCategory.Keys.ToList();

        for (int id = 0; id < sprites.Count && id < numberOfPairsInBoard; id++)
        {
            GameObject card = Instantiate(cardPrefab);
            BuildCard(sprites[id], id, card);
            CreatePair(card);
        }

        cardParent.transform.localScale = new Vector3(scale, scale, 1f);
    }

    private void BuildCard(Sprite sprite, int id, GameObject card)
    {
        card.GetComponent<Card>().PairID = id + 1;
        card.GetComponent<Card>().Sprite = sprite;
        card.GetComponent<Card>().Category = spritesCategory[sprite];
        cards.Add(card);

        ApplySprite(card);

        card.transform.SetParent(cardParent.transform);
    }

    private void CreatePair(GameObject card)
    {
        GameObject cardAux = Instantiate(card);
        cards.Add(cardAux);

        ApplySprite(cardAux);

        cardAux.transform.SetParent(cardParent.transform);
    }

    private void ApplySprite(GameObject card)
    {
        card.GetComponent<Card>().SetCardSpriteToCanvas();
        card.GetComponentsInChildren<SpriteRenderer>().ElementAt(2).sprite = backsideSprite;
    }

    private void DistributeCardsInBoard()
    {
        float maxX = east.transform.position.x;
        float minX = west.transform.position.x;
        float maxY = north.transform.position.y;
        float minY = south.transform.position.y;

        List<GameObject> cardsCopy = new List<GameObject>(cards);

        for (float y = maxY; y >= minY; y -= distanceInY)
        {
            for (float x = minX; x < maxX && cardsCopy.Count > 0; x += distanceInX)
            {
                int randomInt = Random.Range(0, cardsCopy.Count);
                cardsCopy[randomInt].transform.position = new Vector2(x, y);
                cardsCopy.RemoveAt(randomInt);
            }
        }
    }
}
