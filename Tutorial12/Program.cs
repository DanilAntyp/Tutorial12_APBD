using Microsoft.EntityFrameworkCore;
using Tutorial12.Data;
using Tutorial12.Repositories;
using Tutorial12.Services;

namespace Tutorial12;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddAuthorization();
        builder.Services.AddOpenApi();
        builder.Services.AddScoped<IClient, ClientRep>();
        builder.Services.AddScoped<ITrip, TripRep>();
        builder.Services.AddScoped<ITripServise, TripServise>();
        builder.Services.AddDbContext<MasterContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddOpenApi();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        
        app.UseHttpsRedirection();

        app.UseAuthorization();
    }
}