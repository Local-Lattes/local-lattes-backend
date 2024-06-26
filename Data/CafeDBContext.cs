using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using cafeRecAPI.Models;
using static cafeRecAPI.Models.CafeLocation;
namespace cafeRecAPI.Data
{

	public class CafeDBContext : DbContext
	{ 
    	public CafeDBContext(DbContextOptions<CafeDBContext> options) : base(options) { }
    	public DbSet<Review> Reviews { get; set; }
        public DbSet<Cafe> Cafes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<SearchLocation> Locations { get; set; }
        public DbSet<CafeSearchLocation> CafeSearchLocations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchLocation>()
                .HasIndex(sl => sl.Location)
                .IsUnique();

            modelBuilder.Entity<CafeSearchLocation>()
               .HasKey(csl => new { csl.CafeId, csl.SearchLocationId });

            modelBuilder.Entity<CafeSearchLocation>()
                .HasOne(csl => csl.Cafe)
                .WithMany(c => c.CafeSearchLocations)
                .HasForeignKey(csl => csl.CafeId);

            modelBuilder.Entity<CafeSearchLocation>()
                .HasOne(csl => csl.SearchLocation)
                .WithMany(sl => sl.CafeSearchLocations)
                .HasForeignKey(csl => csl.SearchLocationId);

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
