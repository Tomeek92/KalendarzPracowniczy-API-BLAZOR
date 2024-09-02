using KalendarzPracowniczyApplication.CQRS.Commands.Events.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.Events.Delete;
using KalendarzPracowniczyApplication.CQRS.Commands.Events.Update;
using KalendarzPracowniczyApplication.CQRS.Commands.Users.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.Users.Delete;
using KalendarzPracowniczyApplication.CQRS.Commands.Users.Update;
using KalendarzPracowniczyApplication.CQRS.Commands.Workers.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.Workers.Delete;
using KalendarzPracowniczyApplication.CQRS.Commands.Workers.Update;
using KalendarzPracowniczyApplication.CQRS.Queries.Events.GetAll;
using KalendarzPracowniczyApplication.CQRS.Queries.Events.GetElementById;
using KalendarzPracowniczyApplication.CQRS.Queries.Users.GetUserById;
using KalendarzPracowniczyApplication.CQRS.Queries.Workers.GetAllWorkers;
using KalendarzPracowniczyApplication.CQRS.Queries.Workers.GetWorkerById;
using KalendarzPracowniczyApplication.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace KalendarzPracowniczyApplication.Extensions
{
    public static class DependancyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
        typeof(CreateEventCommand).Assembly,
        typeof(UpdateEventCommand).Assembly,
        typeof(DeleteEventCommand).Assembly,
        typeof(GetAllEventQuery).Assembly,
        typeof(GetElementByIdEventQuery).Assembly,
        typeof(CreateUserCommand).Assembly,
        typeof(DeleteUserCommand).Assembly,
        typeof(UpdateUserCommand).Assembly,
        typeof(GetUserByIdQuery).Assembly,
        typeof(GetWorkerByIdQuery).Assembly,
        typeof(GetAllWorkersQuery).Assembly,
        typeof(CreateWorkerCommand).Assembly,
        typeof(UpdateWorkerCommand).Assembly,
        typeof(DeleteWorkerCommand).Assembly
           ));

            services.AddAutoMapper(typeof(MapperProfile));
        }
    }
}