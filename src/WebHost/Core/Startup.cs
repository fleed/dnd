// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the Startup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.WebHost.Core
{
    using Configuration;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Defines the Startup class for the web application.
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
            .AddHostedService<Services.BackgroundDataFetcherService>()
            .Configure<AppConfiguration>(this.Configuration.GetSection("AppConfiguration"))
            .ConfigureOptions(typeof(WebHostConfigureOptions));
                services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        /// <summary>
        /// Configures the request pipeline.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        /// <param name="env">
        /// The env.
        /// </param>
        /// <param name="loggerFactory">
        /// The logger factory.
        /// </param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}