using System;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace WebApi
{
    /// <summary>
    /// Principal
    /// </summary>
    public class Program
    {
        #region Public Methods

        /// <summary>
        /// Método Principal da aplicação
        /// </summary>
        /// <param name="args">Argumentos</param>
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var connection = services.GetRequiredService<IRedisConnection>();

                    DatabaseSeed.SeedSampleEmployeeData(connection);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "Um erro ocorreu ao popular a base de dados.");

                    throw;
                }
            }

            host.RunAsync();
        }

        /// <summary>
        /// Builder da criação do host
        /// </summary>
        /// <param name="args">Argumentos</param>
        /// <returns>Instância do builder de host</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(((context, configuration) =>
                {
                    configuration.Enrich.FromLogContext()
                        .Enrich.WithMachineName()
                        .WriteTo.Console()
                        .WriteTo.Elasticsearch(
                            new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticConfiguration:Uri"]))
                            {
                                IndexFormat =
                                    $"{context.Configuration["ApplicationName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                                AutoRegisterTemplate = true,
                                NumberOfShards = 2,
                                NumberOfReplicas = 1
                            })
                        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                        .ReadFrom.Configuration(context.Configuration);
                }))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        #endregion
    }
}
