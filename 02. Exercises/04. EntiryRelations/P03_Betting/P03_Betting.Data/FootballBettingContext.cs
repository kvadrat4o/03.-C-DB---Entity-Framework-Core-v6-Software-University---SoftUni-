using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace P03_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {

        public FootballBettingContext()
        {

        }

        public FootballBettingContext(DbContextOptions options)
            :base(options)
        {

        }

        public DbSet<Bet> Bets { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<PlayerStatistic>()
                .HasKey(ps => new { ps.GameId, ps.PlayerId });

            builder.Entity<Bet>()
                .Property(b => b.Prediction).IsRequired(true);

            builder.Entity<Color>()
                .HasMany(c => c.PrimaryKitTeams)
                .WithOne(t => t.PrimaryKitColor)
                .HasForeignKey(fk => fk.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Color>()
                .HasMany(c => c.SecondaryKitTeams)
                .WithOne(t => t.SecondaryKitColor)
                .HasForeignKey(fk => fk.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Town>()
                .HasMany(t => t.Teams)
                .WithOne(te => te.Town)
                .HasForeignKey(fk => fk.TownId);

            builder.Entity<Team>()
               .HasMany(t => t.Players)
               .WithOne(p => p.Team)
               .HasForeignKey(fk => fk.TeamId);

            builder.Entity<Team>()
                .HasMany(t => t.HomeGames)
                .WithOne(g => g.HomeTeam)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(fk => fk.HomeTeamId);

            builder.Entity<Team>()
                .HasMany(t => t.AwayGames)
                .WithOne(g => g.AwayTeam)
                .HasForeignKey(fk => fk.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Country>()
                .HasMany(c => c.Towns)
                .WithOne(t => t.Country)
                .HasForeignKey(fk => fk.CountryId);

            builder.Entity<Position>()
                .HasMany(p => p.Players)
                .WithOne(pl => pl.Position)
                .HasForeignKey(fk => fk.PositionId);

            builder.Entity<Game>()
                .HasMany(g => g.Bets)
                .WithOne(b => b.Game)
                .HasForeignKey(fk => fk.GameId);

            builder.Entity<User>()
                .HasMany(u => u.Bets)
                .WithOne(b => b.User)
                .HasForeignKey(fk => fk.UserId);

            builder.Entity<PlayerStatistic>()
                .HasOne(ps => ps.Player)
                .WithMany(p => p.PlayerStatistics)
                .HasForeignKey(fk => fk.PlayerId);

            builder.Entity<PlayerStatistic>()
                .HasOne(ps => ps.Game)
                .WithMany(g => g.PlayerStatistics)
                .HasForeignKey(fk => fk.PlayerId);

            //builder.Entity<Bet>()
            //    .HasOne(bt => bt.Game)
            //    .WithMany(g => g.Bets)
            //    .HasForeignKey(fk => fk.BetId);

            //builder.Entity<Bet>()
            //    .HasOne(bt => bt.User)
            //    .WithMany(u => u.Bets)
            //    .HasForeignKey(fk => fk.BetId);


            //builder.Entity<Team>()
            //    .HasOne(t => t.Town)
            //    .WithMany(tw => tw.Teams)
            //    .HasForeignKey(fk => fk.TeamId);
            
            //builder.Entity<Game>()
            //    .HasMany(g => g.PlayerStatistics)
            //    .WithOne(ps => ps.Game)
            //    .HasForeignKey(fk => fk.GameId);

            //builder.Entity<Game>()
            //    .HasOne(g => g.HomeTeam)
            //    .WithMany(ht => ht.HomeGames)
            //    .HasForeignKey(fk => fk.GameId);

            //builder.Entity<Game>()
            //    .HasOne(g => g.AwayTeam)
            //    .WithMany(at => at.AwayGames)
            //    .HasForeignKey(fk => fk.GameId);

            //builder.Entity<Player>()
            //    .HasOne(p => p.Position)
            //    .WithMany(ps => ps.Players)
            //    .HasForeignKey(fk => fk.PlayerId);

            //builder.Entity<Player>()
            //    .HasOne(p => p.Team)
            //    .WithMany(t => t.Players)
            //    .HasForeignKey(fk => fk.PlayerId);

            //builder.Entity<Player>()
            //    .HasMany(p => p.PlayerStatistics)
            //    .WithOne(ps => ps.Player)
            //    .HasForeignKey(fk => fk.PlayerId);

            //builder.Entity<Team>()
            //    .HasOne(t => t.PrimaryKitColor)
            //    .WithMany(c => c.PrimaryKitColorTeams)
            //    .HasForeignKey(fk => fk.TeamId);

            //builder.Entity<Team>()
            //    .HasOne(t => t.SecondaryKitColor)
            //    .WithMany(c => c.SecondaryKitColorTeams)
            //    .HasForeignKey(fk => fk.TeamId);
        }
    }
}
