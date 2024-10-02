using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyDomain.Interfaces;
using KalendarzPracowniczyInfrastructure.Repositories;
using KalendarzPracowniczyInfrastructureDbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KalendarzPracowniczyInfrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = services.AddDbContext<KalendarzPracowniczyDbContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("KalendarzPracowniczy")));

            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IWorkerRepository, WorkerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWorkRepository, WorkRepository>();

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
        .AddEntityFrameworkStores<KalendarzPracowniczyDbContext>()
        .AddDefaultTokenProviders();
        }
    }
}