using Microsoft.EntityFrameworkCore;

using TinyCardLimits.Core.Model;

namespace TinyCardLimits.Core.Data
{
    public class TinyCardLimitsDBContext : DbContext
    {
        public DbSet<Card> Cards { get; private set; }
        public DbSet<CardLimit> CardLimits { get; private set; }

        public TinyCardLimitsDBContext(DbContextOptions<TinyCardLimitsDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Card>()
                .ToTable("Card");

            modelBuilder.Entity<CardLimit>()
                .ToTable("Limit");
        }
    }
}
