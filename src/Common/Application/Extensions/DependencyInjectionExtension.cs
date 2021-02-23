using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// Adiciona as injeções de dependência da camada Infrastructure
        /// </summary>
        /// <param name="services">Serviços da aplicação</param>
        /// <returns></returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
