using System.Globalization;
using System.Threading.Tasks;
using Bravure.Entities;
using Bravure.Entities.Seed;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace Bravure
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var cultureInfo = new CultureInfo("en-AU");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            CreateWebHostBuilder(args).Build()
                .MigrateDbContext<BravureDbContext>((context, services) =>
                {
                    var env = services.GetService(typeof(IWebHostEnvironment)) as IWebHostEnvironment;
                    SeedData.EnsureSeedData(services);
                }).Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseIISIntegration()
            .UseIIS()
            .UseStartup<Startup>();
    }
}
