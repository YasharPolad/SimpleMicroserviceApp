using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data;

public static class  PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app, IWebHostEnvironment env)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), env.IsDevelopment());
    }

    private static void SeedData(AppDbContext context, bool isDev)
    {
        if (!isDev)
        {
            Console.WriteLine("--> Applying migrations...");
            try
            {
                context.Database.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Could not run migrations: {e.Message}");
            }
        }
        
        if (!context.Platforms.Any())
        {
            Console.WriteLine("==> Seeding Data...");
            
            context.Platforms.AddRange(
                new Platform(){Name = "Dot Net", Publisher = "Microsoft", Cost = "Free"},
                new Platform(){Name = "Sql Server Express", Publisher = "Microsoft", Cost = "Free"},
                new Platform(){Name = "Kubernetes", Publisher = "Google", Cost = "Free"}
                );
            
            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("==> We already have data");
        }
    }
}