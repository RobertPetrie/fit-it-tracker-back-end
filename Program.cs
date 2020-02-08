using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fix_it_tracker_back_end.Model;
using fix_it_tracker_back_end.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace fix_it_tracker_back_end
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // we are creating a reference to the host, but choosing to run it later
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var dataContext = services.GetRequiredService<DataContext>();
                    dataContext.Database.Migrate();
                    SeedData.SeedCustomers(dataContext);
                    SeedData.SeedFaults(dataContext);
                    SeedData.SeedItemTypes(dataContext);
                    SeedData.SeedResolutions(dataContext);
                    SeedData.SeedRepairs(dataContext);
                    SeedData.SeedItems(dataContext);
                    SeedData.SeedRepairItems(dataContext);
                    SeedData.SeedRepairItemFaultsAndResolutions(dataContext);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred during migration");
                }

                host.Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
