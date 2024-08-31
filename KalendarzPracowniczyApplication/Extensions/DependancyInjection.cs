using KalendarzPracowniczyApplication.CQRS.Commands.Events.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.Events.Delete;
using KalendarzPracowniczyApplication.CQRS.Commands.Events.Update;
using KalendarzPracowniczyApplication.CQRS.Queries.Events.GetAll;
using KalendarzPracowniczyApplication.CQRS.Queries.Events.GetElementById;
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
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateEventCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateEventCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteEventCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllEventQuery).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetElementByIdEventQueryHandler).Assembly));
            services.AddScoped<IWorkerService, WorkerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddAutoMapper(typeof(MapperProfile));
        }
    }
}