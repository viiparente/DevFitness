using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFitness.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((hostingContext, config) =>
                 {
                     var settings = config.Build();
                     Log.Logger = new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .WriteTo.MSSqlServer(settings.GetConnectionString("DevFitnessCs"),
                            sinkOptions: new MSSqlServerSinkOptions()
                            {
                                AutoCreateSqlTable = true,
                                TableName = "Logs"
                            }).CreateLogger();
                 }).UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
