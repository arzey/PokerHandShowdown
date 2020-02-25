using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHandShowdown.Core.Tests
{
    public class DealerTests
    {
        [Test]
        public void Dealer_WhenInstantiatedDeckShouldHaveFiftyTwoCards()
        {
            var dealer = new Dealer();

            Assert.AreEqual(52, dealer.Deck.Count);
        }

        [Test]
        public void Dealer_WhenShuffledShouldHaveFiftyTwoCards()
        {
            var dealer = new Dealer();

            dealer.Shuffle();

            Assert.AreEqual(52, dealer.Deck.Count);
        }

        [Test]
        public void Dealer_NewDeckShouldNotHaveDuplicateCards()
        {
            var dealer = new Dealer();

            var hasDuplicate = dealer.Deck.GroupBy(card => card)
                .Where(card => card.Count() > 1)
                .Any();

            Assert.IsFalse(hasDuplicate);
        }

        [Test]
        public void Dealer_ShuffledDeckShouldNotHaveDuplicateCards()
        {
            var dealer = new Dealer();

            dealer.Shuffle();

            var hasDuplicate = dealer.Deck.GroupBy(card => card)
                .Where(card => card.Count() > 1)
                .Any();

            Assert.IsFalse(hasDuplicate);
        }

        [Test]
        public void FillHand_ShouldGenerateHandWithCards()
        {
            var dealer = new Dealer();
            dealer.Shuffle();

            Assert.AreEqual(5, dealer.FillHand().Count);
        }

        [Test]
        public void FillHand_ShouldRemoveCardsFromDeck()
        {
            var dealer = new Dealer();
            dealer.Shuffle();
            dealer.FillHand();

            Assert.AreEqual(47, dealer.Deck.Count);
        }

        [Test]
        public void Deal_ShouldGivePlayerACard()
        {
            var dealer = new Dealer();
            var player = new Player { Name = "Joe" };

            dealer.Shuffle();
            dealer.Deal(player);

            Assert.AreEqual(1, player.Hand.Count);
        }

        [Test]
        public void Deal_ShouldRemoveCardFromDeck()
        {
            var dealer = new Dealer();
            var player = new Player { Name = "Joe" };

            dealer.Shuffle();
            dealer.Deal(player);

            Assert.AreEqual(51, dealer.Deck.Count);
        }

        [Test]
        public void Remove_ShouldRemoveCard()
        {
            var deck = new Dealer();
            var card = new Card { Rank = Rank.Two, Suit = Suit.Clubs };

            deck.Remove(card);

            Assert.IsFalse(deck.Deck.ToList().Exists(c => c == card));
        }
    }
}
