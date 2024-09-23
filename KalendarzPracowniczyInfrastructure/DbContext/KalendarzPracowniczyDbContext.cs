using KalendarzPracowniczyDomain.Entities.Events;
using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyDomain.Entities.Workers;
using KalendarzPracowniczyDomain.Entities.Works;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KalendarzPracowniczyInfrastructureDbContext
{
    public class KalendarzPracowniczyDbContext : IdentityDbContext<User>
    {
        public DbSet<Event>? Events { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Worker>? Workers { get; set; }
        public DbSet<Work>? Works { get; }

        public KalendarzPracowniczyDbContext(DbContextOptions<KalendarzPracowniczyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Events)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}