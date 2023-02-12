using Microsoft.EntityFrameworkCore;
using Redis_ASP.NET_Way.Db;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SalesContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SalesDbConnection"));
});

builder.Services.AddStackExchangeRedisCache(x => x.ConfigurationOptions = new ConfigurationOptions
{
    EndPoints = { "localhost:6379" },
    Password = ""
});
// add session service
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// tell app to use session.
app.UseSession();

app.MapControllers();

app.Run();
