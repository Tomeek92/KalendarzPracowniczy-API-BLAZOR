using KalendarzPracowniczyDomain.Entities.Cars;
using KalendarzPracowniczyDomain.Entities.Events;
using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyDomain.Entities.Works;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KalendarzPracowniczyInfrastructureDbContext
{
    public class KalendarzPracowniczyDbContext : IdentityDbContext<User>
    {
        public DbSet<Event>? Events { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Car>? Cars { get; set; }
        public DbSet<Work>? Works { get; set; }

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
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                 .HasMany(u => u.Works)
                 .WithOne(w => w.User)
                 .HasForeignKey(w => w.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Work>()
                .Property(w => w.StartDate)
                .HasColumnType("date");

            modelBuilder.Entity<Work>()
                .Property(w => w.EndDate)
                .HasColumnType("date");

            modelBuilder.Entity<Work>()
                .Property(w => w.CreatedAt)
                .HasColumnType("date");
        }
    }
}