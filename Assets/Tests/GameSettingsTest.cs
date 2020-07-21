using NUnit.Framework;
using UnityEngine;
using TestScripts;

namespace Tests
{
    public class GameSettingsTest
    {
        [Test]
        public void Settings_Are_Updated()
        {
            GameObject go = new GameObject();
            go.AddComponent<GameManagerMock>().Awake();
            Deck deck = Utils.ArrangeTestDeck();
            Sprite board = Resources.Load<Sprite>("Boards/colorido");
            bool timeLimit = Random.Range(0, 1) == 1;
            int time = Random.Range(1, 100);

            GameSettings.SaveSettings(deck, board, timeLimit, time);

            Assert.AreEqual(deck, GameSettings.deck);
            Assert.AreEqual(timeLimit, GameSettings.timeLimit);
            Assert.AreEqual(time, GameSettings.time);
            Assert.AreEqual(board, GameSettings.board);
        }
    }
}
