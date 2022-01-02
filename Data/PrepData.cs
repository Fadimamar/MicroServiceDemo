using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;
using PlatformService.Data;
namespace PlatformService.Data
{
    public static class PrebData
    {
         public static void PrebPopulation(IApplicationBuilder app)
        {
            using ( var ServiceScope=app.ApplicationServices.CreateScope())
                {
                SeedData(ServiceScope.ServiceProvider.GetService<AppDbContext>());
                }

        }
        private static void SeedData(AppDbContext context)
        {
            if(!context.Platforms.Any())
            {
                Console.WriteLine("--> Seedng Data..");
                context.Platforms.AddRange(
                    new Platform(){Name="Dot Net", Publisher="Microsoft",Cost="Free"},
                    new Platform(){Name="SQL Server Express", Publisher="Microsoft",Cost="Free"},
                    new Platform(){Name="Kubernetes", Publisher="Cloud Native Computing Foundation",Cost="Free"}
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have Data");
            }
        }
    }
}