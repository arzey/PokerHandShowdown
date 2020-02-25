using PokerHandShowdown.Core;
using PokerHandShowdown.Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHandShowdown.UI
{
    public static class Program
    {
        public static readonly Dealer _dealer = new Dealer();
        public static PokerEngine _engine;

        static Program()
        {
            _dealer.Shuffle();
        }

        public static void Main(string[] args)
        {
            Start(args);
        }

        public static void Start(string[] args)
        {
            // Take user input, separate players using the dash (-) sign.
            var sets = TakeUserInput(Console.ReadLine());

            // If no input is recieved, the system will automatically 
            // simulate a showdown between pre-generated players.
            if (sets.Count == 1 && sets[0] == "simulate")
            {
                SimulateRound();
                return;
            }

            // After the successfully splitting the strings, create the players object and
            // the player hand that contains the card value input (if any).
            var players = ProcessUserInputFromKeys(sets);

            // Complete the round and display the winner to the console.
            CompleteRound(players);
        }

        public static List<string> TakeUserInput(string input)
        {
            List<string> sets = new List<string>();

            if (string.IsNullOrWhiteSpace(input))
            {
                sets.Add("simulate");
                return sets;
            }

            sets = input.Split("-", StringSplitOptions.RemoveEmptyEntries)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();

            while (sets.Count == 0)
            {
                Console.Clear();
                TakeUserInput(Console.ReadLine());
            }

            return sets;
        }

        public static List<Player> ProcessUserInputFromKeys(List<string> sets)
        {
            List<Player> players = new List<Player>();
            
            // Each set will contain the user data and card information. For example 
            // "RJ 3s 5d 4c 5h 10s" will generate a Player with the name RJ and will
            // create the hand with the following cards; Three of Spades, Five of Diamond
            // Four of Clubs and Ten of Spades.
            //
            // Note 1: If the card mapping is unsuccesful creating a Card out of the code,
            // it will return null and nothing will be added.
            //
            // Note 2: A hand can only hold a maximum of 5 cards. Adding more card code to the player
            // will automatically get trimmed down.
            //
            // Note 3: If less 5 cards have been specified by the input, the system will always fill
            // the missing card to complete the maximum card count required per hand.
            foreach (var set in sets)
            {
                var keys = set.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .ToArray();

                var name = keys[0];

                if (keys.Length == 1)
                {
                    players.Add(new Player { Name = name });
                }
                else
                {
                    var hand = new Hand();

                    for (int i = 1; i < keys.Length; i++)
                    {
                        var code = keys[i];
                        var card = code.Map();

                        if (card != null)
                        {
                            if (hand.Count < 5)
                            {
                                hand.Add(card);
                                _dealer.Remove(card);
                            }
                        }
                    }

                    players.Add(new Player { Name = name, Hand = hand });
                }
                
            }

            return players;
        }

        public static void SimulateRound()
        {
            _engine = new PokerEngine();
            _engine.Simulate();

            var winners = _engine.Winners;
            DisplayWinners(winners.ToList());
        }

        public static void CompleteRound(List<Player> players)
        {
            if (players.Count != 4)
            {
                var names = new string[4] { "Joe", "Jen", "Bob", "Alice" };
                for (int i = players.Count; i < 4; i++)
                {
                    var player = new Player { Name = names[i - 1], Hand = _dealer.FillHand() };

                    players.Add(player);
                }
            }

            _engine = new PokerEngine(_dealer, players);
            _engine.CheckPlayerHand();

            var winners = _engine.GetWinningPlayers();

            DisplayWinners(winners);
        }

        private static void DisplayWinners(List<Player> players)
        {
            if (players.Count == 1)
            {
                Console.WriteLine($"{players.Single().Name} wins!");
            }
            else
            {
                var names = players.Select(player => player.Name).ToArray();
                var print = $"{string.Join(", ", names, 0, names.Length - 1)} and {names[names.Length - 1]} wins!";

                Console.WriteLine(print);
            }
        }
    }
}
