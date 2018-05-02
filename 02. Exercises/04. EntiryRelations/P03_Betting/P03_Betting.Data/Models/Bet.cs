using System;
using System.Collections.Generic;
using System.Text;

namespace P03_FootballBetting.Data.Models
{
    public class Bet
    {
        public Bet()
        {

        }

        public int BetId { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

        public decimal Amount { get; set; }

        public int Prediction { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime DateTime { get; set; }
    }

}