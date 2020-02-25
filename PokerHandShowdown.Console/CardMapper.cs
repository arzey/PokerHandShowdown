using PokerHandShowdown.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerHandShowdown.UI
{
    public static class CardMapper
    {
        public static Card Map(this string code)
        {
            if (string.IsNullOrWhiteSpace(code) || char.GetNumericValue(code[0]) == 0 || code.Length < 2)
                return null;

            string codeToLowerCase = code.ToLower();

            Rank rank;

            Dictionary<char, Rank> rankLetterCodes = new Dictionary<char, Rank>()
            {
                {'2', Rank.Two},
                {'3', Rank.Three},
                {'4', Rank.Four},
                {'5', Rank.Five},
                {'6', Rank.Six},
                {'7', Rank.Seven},
                {'8', Rank.Eight},
                {'9', Rank.Nine},
                {'t', Rank.Ten},
                {'a', Rank.Ace},
                {'k', Rank.King},
                {'q', Rank.Queen},
                {'j', Rank.Jack},
            };

            Dictionary<char, Suit> suitLetterCodes = new Dictionary<char, Suit>()
            {
                {'c', Suit.Clubs},
                {'h', Suit.Hearts},
                {'s', Suit.Spades},
                {'d', Suit.Diamond},
            };


            if (!rankLetterCodes.TryGetValue(codeToLowerCase[0], out rank))
                return null;

            if (!suitLetterCodes.TryGetValue(codeToLowerCase[1], out var suit))
                return null;

            return new Card(rank, suit);
        }
    }
}
