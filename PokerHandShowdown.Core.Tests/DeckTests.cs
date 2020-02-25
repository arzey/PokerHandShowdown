using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHandShowdown.Core.Tests
{
    public class DeckTests
    {
        [Test]
        public void NewDeckShouldHaveFiftyTwoCards()
        {
            var deck = new Deck();

            Assert.AreEqual(52, deck.Cards.Count);
        }

        [Test]
        public void ShuffledDeckShouldHaveFiftyTwoCards()
        {
            var deck = new Deck();
            deck.Shuffle();

            Assert.AreEqual(52, deck.Cards.Count);
        }

        [Test]
        public void NewDeckShouldNotHaveDuplicateCards()
        {
            var deck = new Deck();

            var hasDuplicate = deck.Cards.GroupBy(card => card)
                .Where(card => card.Count() > 1)
                .Any();

            Assert.IsFalse(hasDuplicate);
        }

        [Test]
        public void ShuffledDeckShouldNotHaveDuplicateCards()
        {
            var deck = new Deck();

            deck.Shuffle();

            var hasDuplicate = deck.Cards.GroupBy(card => card)
                .Where(card => card.Count() > 1)
                .Any();

            Assert.IsFalse(hasDuplicate);
        }

        [Test]
        public void FillHandShouldGenerateHandWithCards()
        {
            var deck = new Deck();
            deck.Shuffle();

            Assert.AreEqual(5, deck.FillHand().Count);
        }

        [Test]
        public void FillHandShouldRemoveCardsFromDeck()
        {
            var deck = new Deck();
            deck.Shuffle();
            deck.FillHand();

            Assert.AreEqual(47, deck.Cards.Count);
        }

        [Test]
        public void DealShouldGivePlayerACard()
        {
            var deck = new Deck();
            var player = new Player { Name = "Joe" };

            deck.Shuffle();
            deck.Deal(player);

            Assert.AreEqual(1, player.Hand.Count);
        }

        [Test]
        public void DealShouldRemoveCardFromDeck()
        {
            var deck = new Deck();
            var player = new Player { Name = "Joe" };

            deck.Shuffle();
            deck.Deal(player);

            Assert.AreEqual(51, deck.Cards.Count);
        }

        [Test]
        public void RemoveShouldRemoveCard()
        {
            var deck = new Deck();
            var card = new Card { Rank = Rank.Two, Suit = Suit.Clubs };

            deck.Remove(card);

            Assert.IsFalse(deck.Cards.Exists(c => c == card));
        }
    }
}
