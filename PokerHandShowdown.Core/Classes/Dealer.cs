using PokerHandShowdown.Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PokerHandShowdown.Core
{
    public class Dealer
    {
        private Random _random = new Random();
        public List<Card> Deck { get; private set; }

        public Dealer()
        {
            Deck = new List<Card>();

            foreach (var suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (var rank in Enum.GetValues(typeof(Rank)))
                {
                    if ((Rank)rank != Rank.None)
                        Deck.Add(new Card { Rank = (Rank) rank, Suit = (Suit) suit });
                }
            }
        }

        public void Deal(Player player)
        {
            var card = Deck.First();

            player.Hand.Add(card);
            Deck.Remove(card);
        }

        public Hand FillHand()
        {
            var hand = new Hand();

            var cards = Deck.Take(5);
            foreach (var card in cards)
            {
                hand.Add(card);
                Deck.Remove(card);
            }

            return hand;
        }

        public void Remove(Card card)
        {
            Deck.Remove(card);
        }

        public void Shuffle()
        {
            var shuffledCards = new List<Card>();
            var check = new List<int>();

            for (int i = 0; i < Deck.Count; i++)
            {
                int number;

                do
                {
                    number = _random.Next(0, Deck.Count);
                }
                while (check.Exists(num => num == number));

                check.Add(number);

                var card = Deck[number];
                shuffledCards.Add(card);
            }

            Deck = shuffledCards;
        }
    }
}
