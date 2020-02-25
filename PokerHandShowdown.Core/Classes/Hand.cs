using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PokerHandShowdown.Core.Classes
{
    public class Hand : IList<Card>
    {
        readonly IList<Card> _cards = new List<Card>();

        public Card this[int index] { get => _cards[index]; set => _cards[index] = value; }

        public int Count => _cards.Count;

        public bool IsReadOnly => _cards.IsReadOnly;

        public bool IsFlush => _cards.Select(card => card.Suit).Distinct().Count() == 1;

        public bool IsOnePair => _cards.GroupBy(card => card.Rank).Where(rank => rank.Count() == 2).Any();

        public bool IsThreeOfAKind => _cards.GroupBy(card => card.Rank).Where(rank => rank.Count() >= 3).Any();

        public Rank RankOfPairs => GetRankOfPairs();

        public Card HighestCard => GetHighestCard();

        public int HandValue => CalculateHandValue();

        public void Add(Card item)
        {
            CheckCardIfInHand(item);

            if (_cards.Count == 5)
                throw new Exception("Hand is already at maximum limit.");

            if (_cards.Count == 4)
            {
                _cards.Add(item);
                return;
            }

            _cards.Add(item);
        }

        public void Insert(int index, Card item)
        {
            CheckCardIfInHand(item);

            if (_cards.Count == 5)
                throw new Exception("Hand is already at maximum limit.");

            if (_cards.Count == 4)
            {
                _cards.Add(item);
                return;
            }

            _cards.Insert(index, item);
        }

        public bool Remove(Card item)
        {
            return _cards.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _cards.RemoveAt(index);
        }

        public void Clear()
        {
            _cards.Clear();
        }

        #region Default Member Implementation

        public bool Contains(Card item)
        {
            return _cards.Contains(item);
        }

        public void CopyTo(Card[] array, int arrayIndex)
        {
            _cards.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return _cards.GetEnumerator();
        }

        public int IndexOf(Card item)
        {
            return _cards.IndexOf(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _cards.GetEnumerator();
        }

        #endregion


        private Rank GetRankOfPairs()
        {
            if (IsThreeOfAKind)
            {
                return _cards.GroupBy(c => c.Rank).Where(rank => rank.Count() >= 3).Max(x => x.Key);
            }
            else if (IsOnePair)
            {
                return _cards.GroupBy(c => c.Rank).Where(rank => rank.Count() == 2).Max(x => x.Key);
            }
            else
            {
                return Rank.None;
            }
        } 

        private Card GetHighestCard()
        {
            return _cards.OrderByDescending(card => card.Rank).First();
        }

        private int CalculateHandValue()
        {
            return _cards.Sum(card => (int)card.Rank);
        }

        private void CheckCardIfInHand(Card card)
        {
            if (_cards.Any(c => c.Rank == card.Rank && c.Suit == card.Suit))
                throw new Exception($"Card already exists in hand: {card.ToString()}");
        }

        public override string ToString()
        {
            var cardNames = _cards.Select(card => card.ToString()).ToArray();

            return $"{string.Join(", ", cardNames, 0, cardNames.Length - 1)} and {cardNames[cardNames.Length - 1]}";
        }
    }
}
