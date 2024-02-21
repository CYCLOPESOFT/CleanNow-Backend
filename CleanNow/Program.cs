using CleanNow.Core.Application;
using CleanNow.Extensions;
using CleanNow.Infrastructured.Identity;
using CleanNow.Infrastructured.Identity.Entities;
using CleanNow.Infrastructured.Identity.Seed;
using CleanNow.Infrastructured.Persistence;
using CleanNow.Infrastructured.Shared;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLayerApplication(builder.Configuration);
builder.Services.AddLayerIdentity(builder.Configuration);
builder.Services.AddLayerPersistence(builder.Configuration);
builder.Services.AddLayerShared(builder.Configuration);
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await DefautlRole.SeedAsync(userManager, roleManager);
        await DefaultBasicUser.SeedAsync(userManager, roleManager);
        await DefaultSuperUser.SeedAsync(userManager, roleManager);
    }
    catch (Exception ex) { }
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
