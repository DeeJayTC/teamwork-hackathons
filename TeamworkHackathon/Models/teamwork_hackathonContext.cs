using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Teamwork_Hackathon.Models
{
    public partial class teamwork_hackathonContext : DbContext
    {

        public teamwork_hackathonContext() { }
        public teamwork_hackathonContext(DbContextOptions<teamwork_hackathonContext> options) : base(options){ }


        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Hackathon> Hackathon { get; set; }
        public virtual DbSet<HackathonMembers> HackathonMembers { get; set; }
        public virtual DbSet<HackathonSearchMembers> HackathonSearchMembers { get; set; }
        public virtual DbSet<HackathonTeam> HackathonTeam { get; set; }
        public virtual DbSet<HackathonTeamOfferings> HackathonTeamOfferings { get; set; }
        public virtual DbSet<HackathonVotingCategory> HackathonVotingCategory { get; set; }
        public virtual DbSet<HackathonVoting> HackathonVoting { get; set; }
        public virtual DbSet<HackathonVotingVote> HackathonVotingVote { get; set; }
        public virtual DbSet<HackathonVotingTeams> HackathonVotingTeams { get; set; }
        public virtual DbSet<VotingTeamOverview> VotingTeams { get; set; }

        public virtual DbSet<VotingResultsTotal> VotingResultsTotal { get; set; }
        public virtual DbSet<VotingResultsByCategory> VotingResultsByCategory { get; set; }
        public virtual DbSet<VotingResultsTotalNonTech> VotingResultsTotalNonTech { get; set; }
        public virtual DbSet<VotingResultsByCategoryNonTech> VotingResultsByCategoryNonTech { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                 optionsBuilder.UseSqlServer(@"Data Source=tcp:twhackdbserver.database.windows.net,1433;Initial Catalog=twhack_db;User Id=teamworkadmin;Password=Gehheim12!_");
                //optionsBuilder.UseSqlServer(@"Server=localhost\sqlexpress;Database=twhackathon_db;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 
            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

  
            modelBuilder.Entity<Hackathon>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("date");
            });

            modelBuilder.Entity<HackathonMembers>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DisplayName).HasMaxLength(450);

                entity.Property(e => e.ProfileImage).HasMaxLength(800);

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(450);

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.HackathonMembers)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_tHackathonMembers_tHackathonTeam");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.HackathonMembers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_tHackathonMembers_AspNetUsers");
            });

            modelBuilder.Entity<HackathonSearchMembers>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Text).HasColumnType("ntext");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(450);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.InverseIdNavigation)
                    .HasForeignKey<HackathonSearchMembers>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HackathonSearchMembers_HackathonSearchMembers");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.HackathonSearchMembers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_HackathonSearchMembers_AspNetUsers");
            });

            modelBuilder.Entity<HackathonTeam>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.HackathonId).HasColumnName("HackathonID");

                entity.Property(e => e.Logo).HasMaxLength(500);

                entity.Property(e => e.TeamDescription).HasColumnType("ntext");

                entity.Property(e => e.TeamName).HasMaxLength(255);

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasMaxLength(500);

                entity.HasOne(d => d.Hackathon)
                    .WithMany(p => p.HackathonTeam)
                    .HasForeignKey(d => d.HackathonId)
                    .HasConstraintName("FK_tHackathonTeam_tHackathon");
            });

            modelBuilder.Entity<HackathonTeamOfferings>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.Text).HasColumnType("ntext");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.HackathonTeamOfferings)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HackathonTeamOfferings_HackathonTeam");
            });

            modelBuilder.Entity<HackathonVoting>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.HackathonId).HasColumnName("HackathonID");

                entity.Property(e => e.VoteEnabledFrom).HasColumnType("datetime");

                entity.Property(e => e.VoteStartDate).HasColumnType("datetime");

            });

            modelBuilder.Entity<HackathonVotingCategory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.HackathonId).HasColumnName("HackathonID");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.Hackathon)
                    .WithMany(p => p.HackathonVotingCategory)
                    .HasForeignKey(d => d.HackathonId)
                    .HasConstraintName("FK_HackathonVotingCategory_Hackathon");
            });

            modelBuilder.Entity<HackathonVotingVote>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(450);

                entity.Property(e => e.VoteDate).HasColumnType("datetime");

                entity.Property(e => e.VotingId).HasColumnName("VotingID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.HackathonVotingVote)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_HackathonVotingSingle_HackathonVotingCategory");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.HackathonVotingVote)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_HackathonVotingSingle_HackathonTeam");

                entity.HasOne(d => d.Voting)
                    .WithMany(p => p.HackathonVotingVote)
                    .HasForeignKey(d => d.VotingId)
                    .HasConstraintName("FK_HackathonVotingSingle_HackathonVoting");


            });

            modelBuilder.Entity<HackathonVotingTeams>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Teamid).HasColumnName("teamid");

                entity.Property(e => e.VotingId).HasColumnName("VotingID");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.HackathonVotingTeams)
                    .HasForeignKey(d => d.Teamid)
                    .HasConstraintName("FK_HackathonVotingTeams_HackathonTeam");

                entity.HasOne(d => d.Voting)
                    .WithMany(p => p.HackathonVotingTeams)
                    .HasForeignKey(d => d.VotingId)
                    .HasConstraintName("FK_HackathonVotingTeams_HackathonVoting");
            });



        }
    }
}
