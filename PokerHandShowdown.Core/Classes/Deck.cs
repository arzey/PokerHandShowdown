using PokerHandShowdown.Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PokerHandShowdown.Core
{
    public class Deck
    {
        private Random _random = new Random();
        public List<Card> Cards { get; private set; }

        public Deck()
        {
            Cards = new List<Card>();

            foreach (var suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (var rank in Enum.GetValues(typeof(Rank)))
                {
                    if ((Rank)rank != Rank.None)
                        Cards.Add(new Card { Rank = (Rank) rank, Suit = (Suit) suit });
                }
            }
        }

        public void Deal(Player player)
        {
            var card = Cards.First();

            player.Hand.Add(card);
            Cards.Remove(card);
        }

        public Hand FillHand()
        {
            var hand = new Hand();

            var cards = Cards.Take(5);
            foreach (var card in cards)
            {
                hand.Add(card);
                Cards.Remove(card);
            }

            return hand;
        }

        public void Remove(Card card)
        {
            Cards.Remove(card);
        }

        public void Shuffle()
        {
            var shuffledCards = new List<Card>();
            var check = new List<int>();

            for (int i = 0; i < Cards.Count; i++)
            {
                int number;

                do
                {
                    number = _random.Next(0, Cards.Count);
                }
                while (check.Exists(num => num == number));

                check.Add(number);

                var card = Cards[number];
                shuffledCards.Add(card);
            }

            Cards = shuffledCards;
        }
    }
}
