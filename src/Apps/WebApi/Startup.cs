using Application.Extensions;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace WebApi
{
    public class Startup
    {
        #region Public Properties

        /// <summary>
        /// Configura��o da aplica��o
        /// </summary>
        public IConfiguration Configuration { get; }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="configuration">Configura��o da aplica��o</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Esse m�todo � chamado em tempo de execu��o. Use esse m�todo para adicionar servi�os ao container.
        /// </summary>
        /// <param name="services">Servi�os da aplica��o</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();

            services.AddInfrastructure(Configuration);

            services.AddControllers();

            services.AddHealthChecks();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Profits Sharing API", Version = "v1" });
            });
        }

        /// <summary>
        /// Esse m�todo � chamado em tempo de execu��o. Use esse m�todo para configurar pipeline de requisi��o HTTP.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHealthChecks("/health");

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Profits Sharing API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #endregion
    }
}
