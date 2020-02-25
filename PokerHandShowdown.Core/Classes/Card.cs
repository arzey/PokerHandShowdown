using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHandShowdown.Core
{
    public class Card
    {
        public Rank Rank { get; set; }
        public Suit Suit { get; set; }

        public Card() { }

        public Card(Rank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public override string ToString()
        {
            return $"{Rank.ToString()} of {Suit.ToString()}";
        }
    }
}
