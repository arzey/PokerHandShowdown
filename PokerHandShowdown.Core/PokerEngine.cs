using PokerHandShowdown.Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHandShowdown.Core
{
    public class PokerEngine
    {
        public Deck Deck { get; private set; } 
        public List<Player> Players { get; private set; }
        public List<Player> Winners { get; private set; }

        public PokerEngine()
        {
            Deck = new Deck();
            Players = new List<Player>();
        }

        public PokerEngine(Deck deck, List<Player> players) : this()
        {
            Deck = deck;
            Players = players;
        }

        public void  Simulate()
        {
            Deck.Shuffle();

            Players.Add(new Player { Name = "Joe" });
            Players.Add(new Player { Name = "Jen" });
            Players.Add(new Player { Name = "Bob" });
            Players.Add(new Player { Name = "Alice" });

            var maxNumberOfCardsOnHand = 5;

            for (int i = 0; i < maxNumberOfCardsOnHand; i++)
            {
                foreach (var player in Players)
                {
                    Deck.Deal(player);
                }
            }

            GetWinningPlayers();
        }

        public void CheckPlayerHand()
        {
            Players.ForEach(player =>
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

        public List<Player> GetWinningPlayers()
        {
            Winners = new List<Player>();

            var players = Players.Where(player => player.Hand.IsFlush).ToList();

            if (players.Count > 0)
            {
                if (players.Count > 1)
                {
                    return Winners = CalculateHandTotal(players);
                }

                return Winners = players;
            }

            players = Players.Where(player => player.Hand.IsThreeOfAKind).ToList();

            if (players.Count > 0)
            {
                if (players.Count > 1)
                {
                    return Winners = TieBreakPlayersWithPairs(players);
                }

                return Winners = players;
            }

            players = Players.Where(player => player.Hand.IsOnePair).ToList();

            if (players.Count > 0)
            {
                if (players.Count > 1)
                {
                    return Winners = TieBreakPlayersWithPairs(players);
                }

                return Winners = players;
            }

            return Winners = CalculateHandTotal(Players);
        }

        private List<Player> CalculateHandTotal(List<Player> players)
        {
            var ranks = players.Select(player => player.Hand.HighestCard.Rank).ToList();
            var highestRank = ranks.Max(rank => rank);
            var topPlayers = players.Where(player => player.Hand.HighestCard.Rank == highestRank).ToList();

            if (topPlayers.Count > 1)
            {
                var highestValue = topPlayers.Max(player => player.Hand.HandValue);

                return topPlayers.Where(player => player.Hand.HandValue == highestValue).ToList();
            }

            return topPlayers;
        }

        private List<Player> TieBreakPlayersWithPairs(List<Player> players) 
        {
            var ranks = players.Select(player => player.Hand.RankOfPairs).ToList();
            var highestRank = ranks.Max(rank => rank);
            var topPlayers = players.Where(player => player.Hand.RankOfPairs == highestRank).ToList();

            if (topPlayers.Count > 1)
            {
                var highestValue = topPlayers.Max(player => player.Hand.HandValue);
                return topPlayers.Where(player => player.Hand.HandValue == highestValue).ToList(); ;
            } 
            else
            {
                return topPlayers;
            }
        }
    }
}
