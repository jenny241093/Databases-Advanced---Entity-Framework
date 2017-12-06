﻿using System;
using Microsoft.EntityFrameworkCore;
using TeamBuilder.Data.Configuration;
using TeamBuilder.Models;

namespace TeamBuilder.Data
{
    public class TeamBuilderContext:DbContext
    {
        public TeamBuilderContext(DbContextOptions options) : base(options)
        {
        }

        public TeamBuilderContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<EventTeam> TeamEvents { get; set; }
        public DbSet<UserTeam> UserTeams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionConfiguration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new EventTeamConfiguration());
            modelBuilder.ApplyConfiguration(new UserTeamConfiguration());
            modelBuilder.ApplyConfiguration(new InvitationConfiguration());
            modelBuilder.ApplyConfiguration(new TeamConfiguration());
        }
    }
}
