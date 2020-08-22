using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            /*
            Then We Shoud Access To Database Context But We Can Do This Becouse We Are OutSide Of Start Up
             That Db Connection (In Services Container)There 
             We Dont Have A Controll Of Life time Of Controller So We Shoud Use Key Word (Useing Statment)
             **************
                a Using Statment Means That Any Code Inside Of This Is Going To Be Disposed 
                as soon as We Have Finshed Of The Method that We Dont Warry About Cleaning Up After Finshing The Method Or Service
              ************

            */
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var cotext = services.GetRequiredService<StoreContext>();
                    //To Create Database If Not Exist
                    await cotext.Database.MigrateAsync();
                    //Create Data From ContextDataSeed If Not Found
                    await StoreContextSeed.SeedAsync(cotext, loggerFactory);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An Error Happend During The Migration");
                }
            }
            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
