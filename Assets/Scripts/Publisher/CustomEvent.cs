using UnityEngine;
using System.Collections;

public class CustomEvent
{
    public object Sender { get; }
    public EventType Type { get; }

    public enum EventType
    {
        cardFlipsToFrontside,
        cardFlips,
        playerMoves,
        allPairsFound
    }

    public CustomEvent(EventType eventType, object sender = null)
    {
        Sender = sender;
        Type = eventType;
    }
}
