using Application.Contracts;
using Application.Services;
using Domain.Repositories;
using Infrastructure.RepositoriesPostgreEF;
using Microsoft.EntityFrameworkCore;
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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
