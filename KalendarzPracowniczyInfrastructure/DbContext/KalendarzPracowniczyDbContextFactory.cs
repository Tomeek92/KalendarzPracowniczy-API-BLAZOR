using KalendarzPracowniczyInfrastructureDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KalendarzPracowniczyInfrastructure.DbContext
{
    public class KalendarzPracowniczyDbContextFactory : IDesignTimeDbContextFactory<KalendarzPracowniczyDbContext>
    {
        public KalendarzPracowniczyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<KalendarzPracowniczyDbContext>();
            optionsBuilder.UseSqlServer("Data Source=byd-tfryckowski;Initial Catalog=KalendarzPracowniczy;Integrated Security=True;TrustServerCertificate=True");
            return new KalendarzPracowniczyDbContext(optionsBuilder.Options);
        }
    }
}