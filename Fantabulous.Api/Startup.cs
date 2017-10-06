using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fantabulous.Api
{
    /// <summary>
    /// The startup class for the API.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration Configuration;

        /// <summary>
        /// Create the startup object.
        /// </summary>
        /// <param name="configuration">
        /// The API configuration
        /// </param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Add all the required servcies to a dependency injection container.
        /// </summary>
        /// <param name="services">
        /// A service collection
        /// </param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcWithFilters();
            services.AddSessionServices(Configuration);
            services.AddMysqlServices(Configuration);
            services.AddAuthServices(Configuration);
            services.AddUserServices(Configuration);
            services.AddPseudServices(Configuration);
            services.AddTagServices(Configuration);
            services.AddWorkServices(Configuration);
            services.AddChapterServices(Configuration);
            services.AddSeriesServices(Configuration);
        }

        /// <summary>
        /// Configure the application.
        /// </summary>
        /// <param name="app">
        /// The application builder.
        /// </param>
        /// <param name="env">
        /// The hosting environment.
        /// </param>
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();
            app.UseMvc();
        }
    }
}
