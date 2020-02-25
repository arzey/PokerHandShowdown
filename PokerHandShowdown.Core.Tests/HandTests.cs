using NUnit.Framework;
using PokerHandShowdown.Core.Classes;
using System;
using System.Collections.Generic;

namespace PokerHandShowdown.Core.Tests
{
    public class HandTests
    {
        [Test]
        public void Hand_AddingSixthCardShouldThrowException()
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
        public void Hand_RankTwoShouldBeTheHighestRank()
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
        public void HandProperty_IsFlushShouldBeTrue()
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

        [Test]
        public void ShouldNotBeAbleToAddSameCard()
        {
            Assert.Throws(typeof(Exception), () =>
            {
                var hand = new Hand();

                hand.Add(new Card { Rank = Rank.Two, Suit = Suit.Clubs });
                hand.Add(new Card { Rank = Rank.Two, Suit = Suit.Clubs });
            }, "Card already exists in hand: Two of Clubs");
        }
    }
}
