using KalendarzPracowniczyApplication.CQRS.Commands.Cars.UpdateDeActivateCar;
using KalendarzPracowniczyApplication.CQRS.Commands.DayOff.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.DayOff.Delete;
using KalendarzPracowniczyApplication.CQRS.Commands.DayOff.Update;
using KalendarzPracowniczyApplication.CQRS.Commands.Events.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.Events.Delete;
using KalendarzPracowniczyApplication.CQRS.Commands.Events.Update;
using KalendarzPracowniczyApplication.CQRS.Commands.Users.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.Users.Delete;
using KalendarzPracowniczyApplication.CQRS.Commands.Users.Logout;
using KalendarzPracowniczyApplication.CQRS.Commands.Users.Update;
using KalendarzPracowniczyApplication.CQRS.Commands.Workers.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.Workers.Delete;
using KalendarzPracowniczyApplication.CQRS.Commands.Workers.Update;
using KalendarzPracowniczyApplication.CQRS.Commands.Works.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.Works.Delete;
using KalendarzPracowniczyApplication.CQRS.Commands.Works.Update;
using KalendarzPracowniczyApplication.CQRS.Queries.DayOff.GetDayOffById;
using KalendarzPracowniczyApplication.CQRS.Queries.Events.GetAll;
using KalendarzPracowniczyApplication.CQRS.Queries.Events.GetElementById;
using KalendarzPracowniczyApplication.CQRS.Queries.Users.GetAllUsers;
using KalendarzPracowniczyApplication.CQRS.Queries.Users.GetUserById;
using KalendarzPracowniczyApplication.CQRS.Queries.Users.Login;
using KalendarzPracowniczyApplication.CQRS.Queries.Workers.GetAllWorkers;
using KalendarzPracowniczyApplication.CQRS.Queries.Workers.GetWorkerById;
using KalendarzPracowniczyApplication.CQRS.Queries.Works.GetUserTaskById;
using KalendarzPracowniczyApplication.CQRS.Queries.Works.GetUserTasks;
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
        typeof(GetCarByIdQuery).Assembly,
        typeof(GetAllCarsQuery).Assembly,
        typeof(CreateCarCommand).Assembly,
        typeof(UpdateCarCommand).Assembly,
        typeof(DeleteCarCommand).Assembly,
        typeof(CreateWorkCommand).Assembly,
        typeof(DeleteWorkCommand).Assembly,
        typeof(UpdateWorkCommand).Assembly,
         typeof(LogOutCommand).Assembly,
         typeof(GetAllUsersQuery).Assembly,
        typeof(GetUserTasksQuery).Assembly,
        typeof(CreateDayOffCommand).Assembly,
        typeof(UpdateDayOffCommand).Assembly,
        typeof(DeleteDayOffCommand).Assembly,
          typeof(LoginQuery).Assembly,
        typeof(GetUserTaskByIdQuery).Assembly,
        typeof(CreateDayOffCommand).Assembly,
        typeof(UpdateDayOffCommand).Assembly,
        typeof(DeleteDayOffCommand).Assembly,
        typeof(GetDayOffByIdQuery).Assembly,
        typeof(UpdateDeActivateCarCommand).Assembly
           ));

            services.AddAutoMapper(typeof(MapperProfile));
        }
    }
}