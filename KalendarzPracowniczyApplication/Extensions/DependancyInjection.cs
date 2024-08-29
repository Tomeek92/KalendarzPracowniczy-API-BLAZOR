using KalendarzPracowniczyApplication.Interfaces;
using KalendarzPracowniczyApplication.Mapper;
using KalendarzPracowniczyApplication.Service;
using Microsoft.Extensions.DependencyInjection;

namespace KalendarzPracowniczyApplication.Extensions
{
    public static class DependancyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IWorkerService, WorkerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddAutoMapper(typeof(MapperProfile));
        }
    }
}