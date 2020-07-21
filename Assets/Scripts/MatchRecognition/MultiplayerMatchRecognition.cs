using UnityEngine;
using System.Collections;

public class MultiplayerMatchRecognition : StandartMatchRecognition
{
    public void FlipbackUpsideCards()
    {
        firstCard?.ForceFlipCard();
        CleanUpCardPairReferences();
    }
}
