
using KalendarzPracowniczyInfrastructure.Extensions;
using KalendarzPracowniczyApplication.Extensions;
using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyInfrastructureDbContext;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
builder.Services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<KalendarzPracowniczyDbContext>()
        .AddDefaultTokenProviders();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
