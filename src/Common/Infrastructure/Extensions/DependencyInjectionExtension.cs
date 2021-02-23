using Infrastructure.Persistence;
using Infrastructure.Persistence.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// Adiciona as injeções de dependência da camada Application
        /// </summary>
        /// <param name="services">Serviços da aplicação</param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRedisConnection, RedisConnection>();

            return services;
        }
    }
}
