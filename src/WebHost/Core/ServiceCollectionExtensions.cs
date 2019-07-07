// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the ServiceCollectionExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.WebHost.Core
{
    using Dnd.WebHost.Core.Abstractions;
    using Dnd.WebHost.Core.Services;

    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Defines the extension methods to add services to the container.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the services to the collection.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        /// <returns>
        /// The <see cref="IServiceCollection"/>.
        /// </returns>
        public static IServiceCollection AddWebServices(this IServiceCollection services) =>
            services
                .AddSingleton<IAnalyzerWebViewService, AnalyzerWebViewService>();

        /// <summary>
        /// Adds the services to the collection.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        /// <returns>
        /// The <see cref="IServiceCollection"/>.
        /// </returns>
        public static IServiceCollection AddWebHostServices(this IServiceCollection services) =>
            services
                .AddSingleton<IAnalyzerWebViewService, AnalyzerWebViewService>()
                .AddSingleton<IAnalyzerWebHost, AnalyzerWebHost>();
    }
}