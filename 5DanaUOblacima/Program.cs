using _5DanaUOblacima.Exceptions;
using _5DanaUOblacima.Services;
using _5DanaUOblacima.Validations;
using DataAcess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GameContext>(options => 
options
        .UseInMemoryDatabase("InMemoryDb")
        .UseLazyLoadingProxies()

);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<PlayerService>();
builder.Services.AddScoped<TeamService>();
builder.Services.AddScoped<MatchService>();


builder.Services.AddScoped<PlayerValidation>();
builder.Services.AddScoped<TeamValidation>();
builder.Services.AddScoped<MatchValidation>();


var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

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
