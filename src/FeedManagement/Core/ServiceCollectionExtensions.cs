// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the ServiceCollectionExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.FeedManagement.Core
{
    using System;

    using Dnd.FeedManagement.Core.Abstractions;
    using Dnd.FeedManagement.Core.Services;

    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Extension methods to register services in a container.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the services required to interact with packages.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        /// <param name="configureOptions">The optional action to configure the options.</param>
        /// <returns>
        /// The <see cref="IServiceCollection"/>.
        /// </returns>
        public static IServiceCollection AddPackagesServices(
            this IServiceCollection services,
            Action<AzureDevOpsFeedsRestApiOptions> configureOptions = null)
        {
            if (configureOptions != null)
            {
                services.Configure(configureOptions);
            }

            services.AddHttpClient<IAzureDevOpsFeedsService, AzureDevOpsFeedsService>();
            return services.AddScoped<IAzureDevOpsFeedsRestApiClient, AzureDevOpsFeedsRestApiClient>();
        }
    }
}