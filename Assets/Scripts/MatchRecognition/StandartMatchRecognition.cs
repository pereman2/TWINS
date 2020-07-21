public class StandartMatchRecognition : MatchRecognition
{
    protected override bool IsPairLegit()
    {
        return CardsHaveSameID();
    }

    protected override void PairSuccess()
    {
        cardsFlipped += 2;
    }
}
