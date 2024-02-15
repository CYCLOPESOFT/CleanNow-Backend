using CleanNow.Core.Application;
using CleanNow.Infrastructured.Identity;
using CleanNow.Infrastructured.Persistence;
using CleanNow.Infrastructured.Shared;

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
