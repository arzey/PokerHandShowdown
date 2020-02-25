using NUnit.Framework;
using PokerHandShowdown.Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHandShowdown.Core.Tests
{
    public class PokerEngineTests
    {
        private readonly Dealer _deck = new Dealer();

        [Test]
        public void SimulateShouldGenerateFourPlayers()
        {
            var engine = new PokerEngine();
            engine.Simulate();

            Assert.AreEqual(4, engine.Players.Count);
        }

        [Test]
        public void SimulateShouldGenerateAtLeastOneWinner()
        {
            var engine = new PokerEngine();
            engine.Simulate();

            Assert.GreaterOrEqual(1, engine.Winners.Count);
        }

        [Test]
        public void InitializeWithParametersShouldReturnCorrectPlayers()
        {
            var player1 = new Player { Name = "Alice", Hand = _deck.FillHand() };
            var player2 = new Player { Name = "Joe", Hand = _deck.FillHand() };

            var playerList = new List<Player> { player1, player2 };

            var engine = new PokerEngine(_deck, playerList);

            Assert.Multiple(() =>
            {
                playerList.ForEach(player =>
                {
                    Assert.IsTrue(engine.Players.Contains(player));
                });
            });
        }

        [Test]
        public void EngineShouldGenerateWinner()
        {
            var player1 = new Player { Name = "Alice", Hand = _deck.FillHand() };
            var player2 = new Player { Name = "Joe", Hand = _deck.FillHand() };

            var playerList = new List<Player> { player1, player2 };

            var engine = new PokerEngine(_deck, playerList);
            engine.GetWinningPlayers();

            Assert.GreaterOrEqual(1, engine.Winners.Count);
        }

        [Test]
        public void SplitWinnersFromOnePair()
        {
            var hand1 = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Diamond },
                new Card { Rank = Rank.Two, Suit = Suit.Hearts },
                new Card { Rank = Rank.Jack, Suit = Suit.Diamond },
                new Card { Rank = Rank.Queen, Suit = Suit.Clubs },
                new Card { Rank = Rank.King, Suit = Suit.Spades },
            };

            var hand2 = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Spades },
                new Card { Rank = Rank.Two, Suit = Suit.Clubs },
                new Card { Rank = Rank.Jack, Suit = Suit.Hearts },
                new Card { Rank = Rank.King, Suit = Suit.Diamond },
                new Card { Rank = Rank.Queen, Suit = Suit.Diamond },
            };

            var player1 = new Player { Name = "Alice", Hand = hand1 };
            var player2 = new Player { Name = "Joe", Hand = hand2 };

            var playerList = new List<Player> { player1, player2 };

            var engine = new PokerEngine(_deck, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(2, engine.Winners.Count);
        }

        [Test]
        public void OnePairShouldWinAgainstHighCard()
        {
            var hand1 = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Diamond },
                new Card { Rank = Rank.Two, Suit = Suit.Hearts },
                new Card { Rank = Rank.Jack, Suit = Suit.Diamond },
                new Card { Rank = Rank.Queen, Suit = Suit.Clubs },
                new Card { Rank = Rank.King, Suit = Suit.Spades },
            };

            var hand2 = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Spades },
                new Card { Rank = Rank.Three, Suit = Suit.Clubs },
                new Card { Rank = Rank.Jack, Suit = Suit.Hearts },
                new Card { Rank = Rank.King, Suit = Suit.Diamond },
                new Card { Rank = Rank.Ace, Suit = Suit.Diamond },
            };

            var player1 = new Player { Name = "Alice", Hand = hand1 };
            var player2 = new Player { Name = "Joe", Hand = hand2 };

            var playerList = new List<Player> { player1, player2 };

            var engine = new PokerEngine(_deck, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(player1, engine.Winners.Single());
        }

        [Test]
        public void Player2ShouldWinOnePairTieBreaker()
        {
            var hand1 = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Diamond },
                new Card { Rank = Rank.Two, Suit = Suit.Hearts },
                new Card { Rank = Rank.Jack, Suit = Suit.Diamond },
                new Card { Rank = Rank.Queen, Suit = Suit.Clubs },
                new Card { Rank = Rank.King, Suit = Suit.Spades },
            };

            var hand2 = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Spades },
                new Card { Rank = Rank.Two, Suit = Suit.Clubs },
                new Card { Rank = Rank.Jack, Suit = Suit.Hearts },
                new Card { Rank = Rank.King, Suit = Suit.Diamond },
                new Card { Rank = Rank.Ace, Suit = Suit.Diamond },
            };

            var player1 = new Player { Name = "Alice", Hand = hand1 };
            var player2 = new Player { Name = "Joe", Hand = hand2 };

            var playerList = new List<Player> { player1, player2 };

            var engine = new PokerEngine(_deck, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(player2, engine.Winners.Single());
        }

        [Test]
        public void ThreeOfAKindShouldWinAgainstOnePair()
        {
            var hand1 = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Diamond },
                new Card { Rank = Rank.Two, Suit = Suit.Hearts },
                new Card { Rank = Rank.Three, Suit = Suit.Diamond },
                new Card { Rank = Rank.Queen, Suit = Suit.Clubs },
                new Card { Rank = Rank.King, Suit = Suit.Spades },
            };

            var hand2 = new Hand
            {
                new Card { Rank = Rank.Three, Suit = Suit.Spades },
                new Card { Rank = Rank.Three, Suit = Suit.Clubs },
                new Card { Rank = Rank.Three, Suit = Suit.Hearts },
                new Card { Rank = Rank.King, Suit = Suit.Diamond },
                new Card { Rank = Rank.Ace, Suit = Suit.Diamond },
            };

            var player1 = new Player { Name = "Alice", Hand = hand1 };
            var player2 = new Player { Name = "Joe", Hand = hand2 };

            var playerList = new List<Player> { player1, player2 };

            var engine = new PokerEngine(_deck, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(player2, engine.Winners.Single());
        }

        [Test]
        public void ThreeOfAKindShouldWinAgainstHighCard()
        {
            var hand1 = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Diamond },
                new Card { Rank = Rank.Seven, Suit = Suit.Hearts },
                new Card { Rank = Rank.Eight, Suit = Suit.Diamond },
                new Card { Rank = Rank.Ace, Suit = Suit.Clubs },
                new Card { Rank = Rank.King, Suit = Suit.Spades },
            };

            var hand2 = new Hand
            {
                new Card { Rank = Rank.Three, Suit = Suit.Spades },
                new Card { Rank = Rank.Three, Suit = Suit.Clubs },
                new Card { Rank = Rank.Three, Suit = Suit.Hearts },
                new Card { Rank = Rank.King, Suit = Suit.Diamond },
                new Card { Rank = Rank.Ace, Suit = Suit.Diamond },
            };

            var player1 = new Player { Name = "Alice", Hand = hand1 };
            var player2 = new Player { Name = "Joe", Hand = hand2 };

            var playerList = new List<Player> { player1, player2 };

            var engine = new PokerEngine(_deck, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(player2, engine.Winners.Single());
        }

        [Test]
        public void FlushShouldWinAgainstThreeOfAKind()
        {
            var hand1 = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Diamond },
                new Card { Rank = Rank.Seven, Suit = Suit.Diamond },
                new Card { Rank = Rank.Three, Suit = Suit.Diamond },
                new Card { Rank = Rank.Ten, Suit = Suit.Diamond },
                new Card { Rank = Rank.Nine, Suit = Suit.Diamond },
            };

            var hand2 = new Hand
            {
                new Card { Rank = Rank.Three, Suit = Suit.Spades },
                new Card { Rank = Rank.Three, Suit = Suit.Clubs },
                new Card { Rank = Rank.Three, Suit = Suit.Hearts },
                new Card { Rank = Rank.King, Suit = Suit.Diamond },
                new Card { Rank = Rank.Ace, Suit = Suit.Diamond },
            };

            var player1 = new Player { Name = "Alice", Hand = hand1 };
            var player2 = new Player { Name = "Joe", Hand = hand2 };

            var playerList = new List<Player> { player1, player2 };

            var engine = new PokerEngine(_deck, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(player1, engine.Winners.Single());
        }

        [Test]
        public void FlushShouldWinAgainstOnePair()
        {
            var hand1 = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Diamond },
                new Card { Rank = Rank.Seven, Suit = Suit.Diamond },
                new Card { Rank = Rank.Three, Suit = Suit.Diamond },
                new Card { Rank = Rank.Ten, Suit = Suit.Diamond },
                new Card { Rank = Rank.Nine, Suit = Suit.Diamond },
            };

            var hand2 = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Spades },
                new Card { Rank = Rank.Two, Suit = Suit.Clubs },
                new Card { Rank = Rank.Seven, Suit = Suit.Hearts },
                new Card { Rank = Rank.King, Suit = Suit.Diamond },
                new Card { Rank = Rank.Ace, Suit = Suit.Diamond },
            };

            var player1 = new Player { Name = "Alice", Hand = hand1 };
            var player2 = new Player { Name = "Joe", Hand = hand2 };

            var playerList = new List<Player> { player1, player2 };

            var engine = new PokerEngine(_deck, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(player1, engine.Winners.Single());
        }

        [Test]
        public void FlushShouldWinAgainstHighCard()
        {
            var hand1 = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Diamond },
                new Card { Rank = Rank.Seven, Suit = Suit.Diamond },
                new Card { Rank = Rank.Three, Suit = Suit.Diamond },
                new Card { Rank = Rank.Ten, Suit = Suit.Diamond },
                new Card { Rank = Rank.Nine, Suit = Suit.Diamond },
            };

            var hand2 = new Hand
            {
                new Card { Rank = Rank.Three, Suit = Suit.Spades },
                new Card { Rank = Rank.Four, Suit = Suit.Clubs },
                new Card { Rank = Rank.Five, Suit = Suit.Hearts },
                new Card { Rank = Rank.Six, Suit = Suit.Diamond },
                new Card { Rank = Rank.Eight, Suit = Suit.Diamond },
            };

            var player1 = new Player { Name = "Alice", Hand = hand1 };
            var player2 = new Player { Name = "Joe", Hand = hand2 };

            var playerList = new List<Player> { player1, player2 };

            var engine = new PokerEngine(_deck, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(player1, engine.Winners.Single());
        }

        [Test]
        public void FlushWithAceShouldWin()
        {
            var hand1 = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Diamond },
                new Card { Rank = Rank.Six, Suit = Suit.Diamond },
                new Card { Rank = Rank.Jack, Suit = Suit.Diamond },
                new Card { Rank = Rank.Queen, Suit = Suit.Diamond },
                new Card { Rank = Rank.King, Suit = Suit.Diamond },
            };

            var hand2 = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Spades },
                new Card { Rank = Rank.Three, Suit = Suit.Spades },
                new Card { Rank = Rank.Jack, Suit = Suit.Spades },
                new Card { Rank = Rank.Ace, Suit = Suit.Spades },
                new Card { Rank = Rank.Queen, Suit = Suit.Spades },
            };

            var player1 = new Player { Name = "Alice", Hand = hand1 };
            var player2 = new Player { Name = "Joe", Hand = hand2 };

            var playerList = new List<Player> { player1, player2 };

            var engine = new PokerEngine(_deck, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(player2, engine.Winners.Single());
        }

        [Test]
        public void FlushWithSameValueShouldGenerateTwoWinners()
        {
            var hand1 = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Diamond },
                new Card { Rank = Rank.Six, Suit = Suit.Diamond },
                new Card { Rank = Rank.Jack, Suit = Suit.Diamond },
                new Card { Rank = Rank.Queen, Suit = Suit.Diamond },
                new Card { Rank = Rank.King, Suit = Suit.Diamond },
            };

            var hand2 = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Spades },
                new Card { Rank = Rank.Six, Suit = Suit.Spades },
                new Card { Rank = Rank.Jack, Suit = Suit.Spades },
                new Card { Rank = Rank.Queen, Suit = Suit.Spades },
                new Card { Rank = Rank.King, Suit = Suit.Spades },
            };

            var player1 = new Player { Name = "Alice", Hand = hand1 };
            var player2 = new Player { Name = "Joe", Hand = hand2 };

            var playerList = new List<Player> { player1, player2 };

            var engine = new PokerEngine(_deck, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(2, engine.Winners.Count);
        }

        [Test]
        public void EnsurePlayersHasCorrectNumberOfCards()
        {
            var hand1 = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Diamond }
            };

            var hand2 = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Spades },
                new Card { Rank = Rank.Six, Suit = Suit.Spades },
                new Card { Rank = Rank.Jack, Suit = Suit.Spades }
            };

            var player1 = new Player { Name = "Alice", Hand = hand1 };
            var player2 = new Player { Name = "Joe", Hand = hand2 };

            var playerList = new List<Player> { player1, player2 };

            var engine = new PokerEngine(_deck, playerList);

            engine.CheckPlayerHand();

            Assert.Multiple(() =>
            {
                playerList.ForEach(player =>
                {
                    Assert.IsTrue(player.Hand.Count == 5);
                });
            });
        }
    }
}
