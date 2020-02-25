using PokerHandShowdown.Core.Classes;
using PokerHandShowdown.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHandShowdown.Core
{
    public class PokerEngine
    {
        public Dealer Deck { get; private set; } 
        private List<Player> _players { get; set; }
        public IReadOnlyList<Player> Players => _players;
        private List<Player> _winners { get; set; }
        public IReadOnlyList<Player> Winners => _winners;

        public PokerEngine()
        {
            Deck = new Dealer();
            _players = new List<Player>();
        }

        public PokerEngine(Dealer deck, List<Player> players) : this()
        {
            Deck = deck;
            _players = players;
        }

        public void  Simulate()
        {
            Deck.Shuffle();

            _players.Add(new Player { Name = "Joe" });
            _players.Add(new Player { Name = "Jen" });
            _players.Add(new Player { Name = "Bob" });
            _players.Add(new Player { Name = "Alice" });

            var maxNumberOfCardsOnHand = 5;

            for (int i = 0; i < maxNumberOfCardsOnHand; i++)
            {
                foreach (var player in Players)
                {
                    Deck.Deal(player);
                }
            }

            var playerBoard = GetWinningPlayers();
        }

        public void CheckPlayerHand()
        {
            _players.ForEach(player =>
            {
                if (player.Hand.Count < 5)
                {
                    for (int i = player.Hand.Count; i < 5; i++)
                    {
                        Deck.Deal(player);
                    }
                } 
            });
        }

        public Dictionary<Player, int> GetPlayerScores()
        {
            var playerScores = new Dictionary<Player, int>();

            foreach (var player in Players)
            {
                var baseScore = player.Hand.Sum(e => (int)e.Rank);
                var playerHighCard = player.Hand.OrderByDescending(e => e.Rank).First();

                if (player.Hand.IsFlush)
                    playerScores.Add(player, baseScore + (50 * (int)HandType.Flush) + (25 * (int)playerHighCard.Rank));
                else if (player.Hand.IsThreeOfAKind)
                    playerScores.Add(player, baseScore + (50 * (int)HandType.ThreeOfAKind) + (25 * (int)playerHighCard.Rank));
                else if (player.Hand.IsOnePair)
                    playerScores.Add(player, baseScore + (50 * (int)HandType.OnePair) + (25 * (int)playerHighCard.Rank));
                else
                    playerScores.Add(player, baseScore + (25 * (int)playerHighCard.Rank));
            }

            return playerScores;
        }

        public List<Player> GetWinningPlayers()
        {
            var playerBoard = GetPlayerScores();
            return _winners = playerBoard.Where(player => player.Value == playerBoard.Max(x => x.Value)).Select(x => x.Key).ToList();
        }
    }
}
