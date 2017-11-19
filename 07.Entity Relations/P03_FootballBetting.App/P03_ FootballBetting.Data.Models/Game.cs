

using System.Collections;
using System.Collections.Generic;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Models
{
    using System;
    public class Game
    {
        public int GameId { get; set; }
        public DateTime DateTime { get; set; }
        
         public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }
        public int HomeTeamGoals { get; set; }
        public double HomeTeamBetRate { get; set; }

        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }
        public int AwayTeamGoals { get; set; }
        public float AwayTeamBetRate { get; set; }


        public Bet Bet { get; set; }
        public float DrawBetRate { get; set; }

        public GameResult Result { get; set; }

        public ICollection<Bet> Bets { get; set; }=new HashSet<Bet>();
        public ICollection<PlayerStatistic> PlayerStatistics { get; set; }=new HashSet<PlayerStatistic>();

    }
}