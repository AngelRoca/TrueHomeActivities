using Application.Contracts;
using Application.Services;
using Domain.Repositories;
using Infrastructure.RepositoriesPostgreEF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using TrueHomeActivities.Api.Auth;
using TrueHomeActivities.Api.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(o =>
    o.Filters.Add(new ExceptionFilter())
);

builder.Services.AddDbContext<TrueHomeDataContext>(options =>
    options
    .UseNpgsql(builder.Configuration.GetConnectionString("TrueHomeDB"))
);

builder.Services.AddTransient<IPropertiesRespository, PropertiesRespositoryPostgreEF>();
builder.Services.AddTransient<IActivitiesRespository, ActivitiesRespositoryPostgreEF>();

builder.Services.AddScoped<IListActivities, ListActivities>();
builder.Services.AddScoped<ICancelActivity, CancelActivity>();
builder.Services.AddScoped<IReScheduleActivity, ReScheduleActivity>();
builder.Services.AddScoped<IScheduleActivity, ScheduleActivity>();
builder.Services.AddSingleton<AuthenticationAndAuthorization>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Authorization Header for Jwt",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
        };
    });

builder.Services.AddAuthorization(options => {
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    .RequireAuthenticatedUser()
    .Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
