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
        private readonly Dealer _dealer = new Dealer();

        [Test]
        public void Simulate_ShouldGenerateFourPlayers()
        {
            var engine = new PokerEngine();
            engine.Simulate();

            Assert.AreEqual(4, engine.Players.Count);
        }

        [Test]
        public void Simulate_ShouldGenerateAtLeastOneWinner()
        {
            var engine = new PokerEngine();
            engine.Simulate();

            Assert.GreaterOrEqual(1, engine.Winners.Count);
        }

        [Test]
        public void Engine_WhenInitializedShouldHaveTheCorrectPlayers()
        {
            var player1 = new Player { Name = "Alice", Hand = _dealer.FillHand() };
            var player2 = new Player { Name = "Joe", Hand = _dealer.FillHand() };

            var playerList = new List<Player> { player1, player2 };

            var engine = new PokerEngine(_dealer, playerList);

            Assert.Multiple(() =>
            {
                playerList.ForEach(player =>
                {
                    Assert.IsTrue(engine.Players.Contains(player));
                });
            });
        }

        [Test]
        public void GetWinningPlayers_ShouldGenerateWinners()
        {
            var player1 = new Player { Name = "Alice", Hand = _dealer.FillHand() };
            var player2 = new Player { Name = "Joe", Hand = _dealer.FillHand() };

            var playerList = new List<Player> { player1, player2 };

            var engine = new PokerEngine(_dealer, playerList);
            engine.GetWinningPlayers();

            Assert.GreaterOrEqual(1, engine.Winners.Count);
        }

        [Test]
        public void GetWinningPlayers_ShouldProduceTwoWinners()
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

            var engine = new PokerEngine(_dealer, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(2, engine.Winners.Count);
        }

        [Test]
        public void GetWinningPlayers_OnePairShouldWinAgainstHighCard()
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

            var engine = new PokerEngine(_dealer, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(player1, engine.Winners.Single());
        }

        [Test]
        public void GetWinningPlayers_PlayerTwoShouldWinTieBreaker()
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

            var engine = new PokerEngine(_dealer, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(player2, engine.Winners.Single());
        }

        [Test]
        public void GetWinningPlayers_ThreeOfKindShouldWinAgainstOnePair()
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

            var engine = new PokerEngine(_dealer, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(player2, engine.Winners.Single());
        }

        [Test]
        public void GetWinningPlayers_ThreeOfAKindShouldWinAgainstHighCard()
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

            var engine = new PokerEngine(_dealer, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(player2, engine.Winners.Single());
        }

        [Test]
        public void GetWinningPlayers_FlushShouldWinAgainstThreeOfAKind()
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

            var engine = new PokerEngine(_dealer, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(player1, engine.Winners.Single());
        }

        [Test]
        public void GetWinningPlayers_FlushShouldWinAgainstOnePair()
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

            var engine = new PokerEngine(_dealer, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(player1, engine.Winners.Single());
        }

        [Test]
        public void GetWinningPlayers_FlushShouldWinAgainstHighCard()
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

            var engine = new PokerEngine(_dealer, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(player1, engine.Winners.Single());
        }

        [Test]
        public void GetWinningPlayers_FlushWithAceShouldWin()
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

            var engine = new PokerEngine(_dealer, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(player2, engine.Winners.Single());
        }

        [Test]
        public void GetWinningPlayers_FlushWithSameValueShouldGenerateTwoWinners()
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

            var engine = new PokerEngine(_dealer, playerList);
            engine.GetWinningPlayers();

            Assert.AreEqual(2, engine.Winners.Count);
        }

        [Test]
        public void CheckPlayerHand_EnsurePlayersHasCorrectNumberOfCards()
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

            var engine = new PokerEngine(_dealer, playerList);

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
