using Infrastructure.Persistence;
using Infrastructure.Persistence.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure.Extensions
{
    /// <summary>
    /// Extensão para a injeção de dependência
    /// </summary>
    public static class DependencyInjectionExtension
    {
        #region Constants

        /// <summary>
        /// Nome da seção de configuração do Redis
        /// </summary>
        private const string CONNECTION_NAME = "redis";

        #endregion

        #region Public Methods

        /// <summary>
        /// Adiciona as injeções de dependência da camada Application
        /// </summary>
        /// <param name="services">Serviços da aplicação</param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(configuration.GetConnectionString(CONNECTION_NAME)));

            services.AddStackExchangeRedisCache(options => options.Configuration = configuration.GetConnectionString(CONNECTION_NAME));

            services.AddScoped<IApplicationDb, ApplicationDb>();

            return services;
        }

        #endregion
    }
}
