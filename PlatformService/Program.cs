using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    if (builder.Environment.IsDevelopment())
    {
        Console.WriteLine("In Memory Db");
        opt.UseInMemoryDatabase("InMemory");
    }
    else
    {
        Console.WriteLine("Postgres Db");
        opt.UseNpgsql(builder.Configuration.GetConnectionString("PlatformServiceConnection"));
    }
});
builder.Services.AddScoped<IPlatformRepo, InMemoryRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddLogging();

var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

PrepDb.PrepPopulation(app, builder.Environment);

app.Run();