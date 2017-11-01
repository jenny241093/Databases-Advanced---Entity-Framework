using System;
using System.Collections.Generic;
using System.Linq;

namespace Csharp.OOP.Basics.Encapsulation._6.FootballTeamGenerator
{
    partial class Program
    {
        private class Team
        {
            private string _name;
            public HashSet<Player> Players { get; }

            public string Name
            {
                get { return _name; }
                private set
                {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new ArgumentException("A name should not be empty.");

                    _name = value;
                }
            }

            public int Rating
            {
                get
                {
                    if (this.Players.Count == 0)
                        return 0;

                    return (int)Math.Round(this.Players.SelectMany(x => x.Stats.Select(y => y.Amount)).Average());
                }
            }

            public void AddPlayer(Player player)
            {
                this.Players.Add(player);
            }

            public void RemovePlayer(string playerName)
            {
                var player = this.Players.FirstOrDefault(x => x.Name == playerName);

                if (player == null)
                    throw new ArgumentException($"Player {playerName} is not in {this.Name} team.");

                this.Players.Remove(player);
            }

            public override string ToString()
            {
                return $"{this.Name} - {this.Rating}";
            }

            public Team(string name)
            {
                this.Name = name;
                this.Players = new HashSet<Player>();
            }
        }
    }
}