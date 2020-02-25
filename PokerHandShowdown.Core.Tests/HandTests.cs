using NUnit.Framework;
using PokerHandShowdown.Core.Classes;
using System;
using System.Collections.Generic;

namespace PokerHandShowdown.Core.Tests
{
    public class HandTests
    {
        [Test]
        public void ShouldThrowException()
        {
            var hand = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Spades },
                new Card { Rank = Rank.Three, Suit = Suit.Spades },
                new Card { Rank = Rank.Four, Suit = Suit.Spades },
                new Card { Rank = Rank.Five, Suit = Suit.Spades },
                new Card { Rank = Rank.King, Suit = Suit.Spades }
            };

            Assert.Throws(typeof(Exception), () =>
            {
                hand.Add(new Card { Rank = Rank.Ace, Suit = Suit.Hearts });
            }, "Hand is already at maximum limit.");
        }

        [Test]
        public void FlushOfClubsHandValueShouldEqualsToTwenty()
        {
            var hand = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Clubs },
                new Card { Rank = Rank.Three, Suit = Suit.Clubs },
                new Card { Rank = Rank.Four, Suit = Suit.Clubs },
                new Card { Rank = Rank.Five, Suit = Suit.Clubs },
                new Card { Rank = Rank.Six, Suit = Suit.Clubs },
            };

            Assert.AreEqual(20, hand.HandValue);
        }


        [Test]
        public void TwoOneOfAKindShouldReturnHighestPair()
        {
            var hand = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Diamond },
                new Card { Rank = Rank.Two, Suit = Suit.Hearts },
                new Card { Rank = Rank.Three, Suit = Suit.Clubs },
                new Card { Rank = Rank.Three, Suit = Suit.Diamond },
                new Card { Rank = Rank.Jack, Suit = Suit.Spades },
            };

            Assert.AreEqual(Rank.Three, hand.RankOfPairs);
        }

        [Test]
        public void HandShouldReturnIsFlushToTrue()
        {
            var hand = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Clubs },
                new Card { Rank = Rank.Three, Suit = Suit.Clubs },
                new Card { Rank = Rank.Four, Suit = Suit.Clubs },
                new Card { Rank = Rank.Five, Suit = Suit.Clubs },
                new Card { Rank = Rank.Six, Suit = Suit.Clubs },
            };

            Assert.IsTrue(hand.IsFlush);
        } 

        [TestCase]
        public void FlushOfSpadesHandValueShouldEqualsToTwenty()
        {
            var hand = new Hand
            {
                new Card { Rank = Rank.Two, Suit = Suit.Spades },
                new Card { Rank = Rank.Three, Suit = Suit.Spades },
                new Card { Rank = Rank.Four, Suit = Suit.Spades },
                new Card { Rank = Rank.Five, Suit = Suit.Spades },
                new Card { Rank = Rank.King, Suit = Suit.Spades }
            };

            Assert.AreEqual(27, hand.HandValue);
        }

        [Test]
        public void ShouldNotBeAbleToAddSameCard()
        {
            Assert.Throws(typeof(Exception), () =>
            {
                var hand = new Hand();

                hand.Add(new Card { Rank = Rank.Two, Suit = Suit.Clubs });
                hand.Add(new Card { Rank = Rank.Two, Suit = Suit.Clubs });
            }, "Card already exists in hand.");
        }
    }
}
