using NUnit.Framework;

namespace Tests
{
    public class MultiplayerTests
    {
        PlayerManager playerManager;

        [SetUp]
        public void SetUp()
        {
            playerManager = PlayerManager.Instance;
        }

        [Test]
        public void Change_Active_Player()
        {
            string initialPlayer = playerManager.ActivePlayerName;

            playerManager.ChangeActivePlayer();

            Assert.AreNotEqual(initialPlayer, playerManager.ActivePlayerName);
        }
    }
}