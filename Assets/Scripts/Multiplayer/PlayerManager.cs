public class PlayerManager
{
    private static PlayerManager instance = null;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
                instance = new PlayerManager();

            return instance;
        }
    }

    public enum Player
    {
        player1,
        player2
    }

    private Player activePlayer = Player.player1;
    public Player ActivePlayer
    {
        get { return activePlayer; }
    }

    public string Player1Name
    {
        get { return "Player 1"; }
    }

    public string Player2Name
    {
        get { return "Player 2"; }
    }

    public string ActivePlayerName
    {
        get
        {
            if (activePlayer == Player.player1)
                return Player1Name;

            return Player2Name;
        }
    }

    private PlayerManager()
    {
    }

    public Player ChangeActivePlayer()
    {
        switch (activePlayer)
        {
            case Player.player1:
                activePlayer = Player.player2;
                break;

            case Player.player2:
                activePlayer = Player.player1;
                break;

            default:
                break;
        }

        return activePlayer;
    }
}
