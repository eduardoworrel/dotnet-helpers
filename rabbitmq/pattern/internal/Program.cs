using Data;
using Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/CreateOrUpdate",
    (
        ApplicationDbContext _ctx,
        Request request
    )=>
{
    var entity = _ctx.Requests.FirstOrDefault(x => x.Id == request.Id);

    if(entity == null)
    {
        _ctx.Requests.Add(request);
    }
    else
    {
        entity.Status = request.Status;
    }

    _ctx.SaveChanges();

    return entity;
})
.WithName("CreateOrUpdate")
.WithOpenApi();


app.MapGet("/tracking", (
    ApplicationDbContext _ctx,
    Guid id
) =>
{
    var entity = _ctx.Requests.FirstOrDefault(x => x.Id == id);

    return entity;
})
.WithName("tracking")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
