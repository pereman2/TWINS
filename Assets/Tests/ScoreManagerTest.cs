using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class ScoreManagerTest
    {
        private GameObject go;
        ScoreManager scoreManager;

        [SetUp]
        public void SetUp()
        {
            go = new GameObject();
            scoreManager = go.AddComponent<ScoreManager>();
        }

        [Test]
        public void Score_Is_Updated()
        {
            int update = (int)Random.Range(0, 100);
            int expected = scoreManager.Score + update;

            scoreManager.UpdateScoreBy(update);

            Assert.AreEqual(expected, scoreManager.Score);
        }

        [Test]
        public void Score_Is_Never_Negative()
        {
            int update = (int)Random.Range(-100, -1);
            int expected = 0;
            
            scoreManager.UpdateScoreBy(update);

            Assert.AreEqual(expected, scoreManager.Score);
        }

        [Test]
        public void Score_Is_Updated_On_Turn_Ends()
        {
            int previous = 10;
            int expected = 8;
            scoreManager.UpdateScoreBy(previous);

            scoreManager.TurnEnds();

            Assert.AreEqual(expected, scoreManager.Score);
        }
    }
}
