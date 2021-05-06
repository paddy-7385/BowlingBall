using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingBall.Tests
{
    [TestClass]
    public class GameFixture
    {
        [TestMethod]
        public void BowlingBall_game_score_should_be_zero_test()
        {
            var game = new Game();
            Roll(game, 0, 20);
            Assert.AreEqual(0, game.GetScore());
        }

        [TestMethod]
        public void BowlingBall_game_score_should_be_highest_in_all_strike_test()
        {
            var game = new Game();
            Roll(game, 10, 12);
            Assert.AreEqual(300, game.GetScore());
        }

        [TestMethod]
        public void BowlingBall_game_score_should_be_150_in_all_spare_with_five_score_in_each_roll_test()
        {
            var game = new Game();
            Roll(game, 5, 21);
            Assert.AreEqual(150, game.GetScore());
        }

        [TestMethod]
        public void BowlingBall_game_score_should_be_80_in_all_spare_with_four_score_in_each_roll_test()
        {
            var game = new Game();
            Roll(game, 4, 21);
            Assert.AreEqual(80, game.GetScore());
        }

        [TestMethod]
        public void BowlingBall_game_score_should_be_20_in_all_spare_with_four_score_in_each_roll_test()
        {
            var game = new Game();
            Roll(game, 1, 21);
            Assert.AreEqual(20, game.GetScore());
        }

        [TestMethod]
        public void BowlingBall_game_score_should_be_90_without_strike_test()
        {
            var game = new Game();
            Roll(game, 9,0, 21);
            Assert.AreEqual(90, game.GetScore());
        }

        [TestMethod]
        public void BowlingBall_game_score_should_be_190_without_strike_test()
        {
            var game = new Game();
            Roll(game, 9, 1, 21);
            Assert.AreEqual(190, game.GetScore());
        }

        private void Roll(Game game, int pins, int times)
        {
            for (int i = 0; i < times; i++)
            {
                game.Roll(pins);
            }
        }

        private void Roll(Game game, int pinsFirst, int pinLast, int times)
        {
            var current = pinsFirst;
            for (int i = 0; i < times; i++)
            {
                game.Roll(current);
                current = current == pinsFirst ? pinLast : pinsFirst;
            }
        }
    }
}
