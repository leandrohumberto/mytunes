using Microsoft.EntityFrameworkCore;
using MyTunes.Core.Entities;
using System.Reflection;

namespace MyTunes.Infrastructure.Persistence
{
    public class MyTunesDbContext : DbContext
    {
        public MyTunesDbContext(DbContextOptions<MyTunesDbContext> options) : base(options) { }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
