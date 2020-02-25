using NUnit.Framework;
using PokerHandShowdown.Core;
using PokerHandShowdown.UI;
using System.Linq;

namespace PokerHandShowdown.Console.Tests
{
    public class ConsoleTests
    {

        [Test]
        public void ProgramShouldHaveInitializedDeck()
        {
            Assert.AreEqual(52, Program._dealer.Deck.Count);
        }

        [Test]
        public void MapperShouldReturnValidCard()
        {
            var card = CardMapper.Map("ad");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(card.Rank == Rank.Ace);
                Assert.IsTrue(card.Suit == Suit.Diamond);
            });
        }

        [TestCase("Qd", ExpectedResult = "Queen of Diamond")]
        [TestCase("kD", ExpectedResult = "King of Diamond")]
        [TestCase("ts", ExpectedResult = "Ten of Spades")]
        [TestCase("9C", ExpectedResult = "Nine of Clubs")]
        public string MapperShouldReturnCardString(string code)
        {
            return code.Map().ToString();
        }

        [TestCase(" ")]
        [TestCase("")]
        [TestCase("3")]
        [TestCase("Joe")]
        [TestCase(null)]
        public void MapperShouldReturnNull(string code)
        {
            Assert.IsNull(CardMapper.Map(code));
        }

        [TestCase("-Alice -Bob 5d -Jen")]
        public void ProgramShouldTakeInput(string input)
        {
            var sets = Program.TakeUserInput(input);

            Assert.AreEqual(3, sets.Count);
        }

        [Test]
        [Repeat(150)]
        public void ProgramShouldRunSimulation()
        {
            Program.SimulateRound();

            Assert.GreaterOrEqual(Program._engine.Winners.Count, 1);
        }

        [TestCase("-rj 5d ad 3d 6d td -alice ts 3c 3h 5s 5h 4d ts tc th")]
        public void OverflowCardsShouldNotBeAddedToHand(string args)
        {
            var keys = Program.TakeUserInput(args);
            var players = Program.ProcessUserInputFromKeys(keys);

            Assert.Multiple(() =>
            {
                foreach (var player in players)
                {
                    Assert.LessOrEqual(5, player.Hand.Count);
                }
            });
        }

        [TestCase("-rj 5d ad 3d 6d td -alice 4s -ben -joe test 6s 6c")]
        public void ShouldGenerateFourPlayersWithCompletedHand(string args)
        {
            var keys = Program.TakeUserInput(args);
            var players = Program.ProcessUserInputFromKeys(keys);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(players[0].Hand.Count == 5);
                Assert.IsTrue(players[1].Hand.Count == 1);
                Assert.IsTrue(players[2].Hand.Count == 0);
                Assert.IsTrue(players[3].Hand.Count == 2);
            });
        }

        [TestCase("-rj 5d ad 3d 6d td -alice 4s -ben -joe test 6s 6c")]
        public void RJShouldWinTestRound(string args)
        {
            var keys = Program.TakeUserInput(args);
            var players = Program.ProcessUserInputFromKeys(keys);

            Program.CompleteRound(players);

            Assert.IsTrue(Program._engine.Winners.Single().Name == "rj");
        }
    }
}