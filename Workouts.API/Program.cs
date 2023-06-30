using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Workouts.API.DatabaseOperations;
using Workouts.API.JWT;
using Workouts.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

#region JWT

builder.Services.AddOptions<JWTSettings>()
    .Bind(builder.Configuration.GetSection("JWTSettings"))
    .ValidateDataAnnotations();

builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddScheme<JwtBearerOptions, JwtBearerHandler>(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        JWTSettings settings = builder.Configuration.GetSection("JWTSettings").Get<JWTSettings>();
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));

        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = settings.Issuer,
            ValidAudience = settings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SigningKey)),
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddScoped<JWTTokenGenerator>();

#endregion;

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

#region Swagger

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter Token",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {

                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[] { "Bearer "}
        }
    });
});

#endregion;

#region Db Access

builder.Services.AddSingleton<AuditEntityInterceptor>();

string connectionString = builder.Configuration.GetConnectionString("Local");
builder.Services.AddDbContext<WorkoutContext>((sp, optBuilder) =>
{
    AuditEntityInterceptor auditEntityInterceptor = sp.GetRequiredService<AuditEntityInterceptor>();

    optBuilder
    .UseSqlServer(connectionString)
    .AddInterceptors(auditEntityInterceptor);
});


#endregion


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

app.UseAuthentication();
app.UseAuthorization();


app.UseExceptionMiddleware();

app.MapControllers();


app.Run();
