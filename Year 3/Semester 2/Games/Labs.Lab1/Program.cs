using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Labs.Lab1.Database;
using Labs.Lab1.Services;
using System.Threading.Tasks;

namespace Labs.Lab1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = ConfigureServices();
            var dbParser = services.GetService<DbParser>();
            await dbParser.ParseJsonsToDatabaseAsync();
        }

        static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            services.AddDbContext<GameDataDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("GameDatabase")));

            services.AddTransient<ILogger>(_ => 
                new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger());

            services.AddTransient<DbParser>();

            return services.BuildServiceProvider();
        }
    }
}
