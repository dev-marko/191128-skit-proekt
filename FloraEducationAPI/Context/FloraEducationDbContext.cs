using FloraEducationAPI.Domain.Models;
using FloraEducationAPI.Domain.Models.Authentication;
using FloraEducationAPI.Domain.Relations;
using Microsoft.EntityFrameworkCore;

namespace FloraEducationAPI.Context
{
    public class FloraEducationDbContext : DbContext
    {
        public FloraEducationDbContext(DbContextOptions<FloraEducationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Plant> Plants { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Badge> Badges { get; set; }
        public virtual DbSet<UserBadges> UserBadges { get; set; }
        public virtual DbSet<UserLikedPlants> UserLikedPlants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Defining Primary Keys
            modelBuilder
                .Entity<User>()
                .HasKey(e => e.Username);

            modelBuilder
                .Entity<Plant>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder
                .Entity<Comment>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder
                .Entity<MiniQuiz>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder
                .Entity<QuizQuestion>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder
                .Entity<Badge>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<UserBadges>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<UserLikedPlants>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            // Defining Foreign Keys
            modelBuilder
                .Entity<Comment>()
                .HasOne(e => e.Plant)
                .WithMany(e => e.Comments)
                .HasConstraintName("FK_PlantId");

            modelBuilder
                .Entity<Comment>()
                .HasOne(e => e.Author);

            modelBuilder
                .Entity<MiniQuiz>()
                .HasOne(e => e.Plant);

            modelBuilder
                .Entity<QuizQuestion>()
                .HasOne(e => e.Quiz)
                .WithMany(e => e.Questions);

            modelBuilder
                .Entity<UserBadges>()
                .HasOne(e => e.User)
                .WithMany(e => e.Badges)
                .HasForeignKey(e => e.Username)
                .HasConstraintName("FK_Username");

            modelBuilder
                .Entity<UserBadges>()
                .HasOne(e => e.Badge)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.BadgeId)
                .HasConstraintName("FK_BadgeId");

            modelBuilder
                .Entity<UserLikedPlants>()
                .HasOne(e => e.User) 
                .WithMany(e => e.LikedPlants)
                .HasForeignKey(e => e.Username)
                .HasConstraintName("FK_Username");
        }
    }
}
