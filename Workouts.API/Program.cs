using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Workouts.API.DatabaseOperations;
using Workouts.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlServer<WorkoutContext>(builder.Configuration.GetConnectionString("Local"));

builder.Services.AddScoped<WorkoutContext>();


var app = builder.Build();

#region Db Migration Check

using var scope = app.Services.CreateScope();
WorkoutContext context = scope.ServiceProvider.GetService<WorkoutContext>();
if (context.Database.GetPendingMigrations().Any())
    context.Database.Migrate();

#endregion



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionMiddleware();

app.MapControllers();


app.Run();
