using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinTur.Domain.BusinessEntities;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace MinTur.DataAccess.Contexts
{
    [ExcludeFromCodeCoverage]
    public class NaturalUruguayContext : DbContext
    {
        public DbSet<Region> Regions { get; set; }
        public DbSet<TouristPoint> TouristPoints { get; set; }
        public DbSet<ChargingPoint> ChargingPoints { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TouristPointCategory> TouristPointCategories { get; set; }
        public DbSet<Resort> Resorts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationState> ReservationStates { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<AuthorizationToken> AuthorizationTokens { get; set; }
        public DbSet<GuestGroup> GuestGroups { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public NaturalUruguayContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TouristPointCategory>().HasKey(t => t.Id);
            modelBuilder.Entity<TouristPointCategory>().HasIndex(t => new { t.CategoryId, t.TouristPointId }).IsUnique(true);
            modelBuilder.Entity<TouristPointCategory>().HasOne(t => t.Category)
                .WithMany(p => p.TouristPointCategory).HasForeignKey(t => t.CategoryId);
            modelBuilder.Entity<TouristPointCategory>().HasOne(t => t.TouristPoint)
                .WithMany(p => p.TouristPointCategory).HasForeignKey(t => t.TouristPointId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string directory = Directory.GetCurrentDirectory();

                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();

                var connectionString = configuration.GetConnectionString(@"NaturalUruguayDB");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }

}