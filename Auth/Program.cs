using Auth.Data;
using Auth.Data.Seeder;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            string env;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var environment = scope.ServiceProvider.GetService<IHostEnvironment>();
                env = environment.EnvironmentName;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    var serviceProvider = services.GetRequiredService<IServiceProvider>();
                    var configuration = services.GetRequiredService<IConfiguration>();
                    IdentitySeeder.IdentitySeed(serviceProvider, context).Wait();

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            host.Run();
        }
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    };

}
