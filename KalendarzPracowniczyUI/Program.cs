using KalendarzPracowniczyApplication.Extensions;
using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyInfrastructure.Extensions;
using KalendarzPracowniczyInfrastructureDbContext;
using KalendarzPracowniczyUI.Components;
using KalendarzPracowniczyUI.Service;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedAccount = false;
})
        .AddEntityFrameworkStores<KalendarzPracowniczyDbContext>()
        .AddDefaultTokenProviders();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHttpClient<EventServiceUI>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7164");
});
builder.Services.AddHttpClient<UserServiceUI>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7164");
});
builder.Services.AddHttpClient<WorkerServiceUI>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7164");
});
builder.Services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<KalendarzPracowniczyDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();