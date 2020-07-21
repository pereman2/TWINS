using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class StandardMatchType : MonoBehaviour
{
    [SerializeField]
    protected GameObject cards;

    protected List<Card> GetBoardCards()
    {
        return cards.GetComponentsInChildren<Card>().ToList();
    }
}
