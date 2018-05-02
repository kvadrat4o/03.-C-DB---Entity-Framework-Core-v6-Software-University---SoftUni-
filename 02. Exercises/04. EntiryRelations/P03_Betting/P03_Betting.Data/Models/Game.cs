﻿using System;
using System.Collections.Generic;
using System.Text;

namespace P03_FootballBetting.Data.Models
{
    public class Game
    {
        public Game()
        {

        }

        public int GameId { get; set; }

        public Team AwayTeam { get; set; }
        public double AwayTeamBetRate { get; set; }
        public int AwayTeamGoals { get; set; }
        public int AwayTeamId { get; set; }

        public double DrawBetRate { get; set; }

        public Team HomeTeam { get; set; }
        public double HomeTeamBetRate { get; set; }
        public int HomeTeamGoals { get; set; }
        public int HomeTeamId { get; set; }

        public int Result { get; set; }

        public DateTime DateTime { get; set; }

        public ICollection<Bet> Bets { get; set; } = new List<Bet>();

        public ICollection<PlayerStatistic> PlayerStatistics { get; set; } = new List<PlayerStatistic>();
    }
}
