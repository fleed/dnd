// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the ServiceCollectionExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.Meta.Core
{
    using Dnd.Meta.Core.Abstractions;
    using Dnd.Meta.Core.Services;

    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Defines extension methods for the service collection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the Meta services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The container after registering the meta services.</returns>
        public static IServiceCollection AddMetaServices(this IServiceCollection services) =>
            services.AddSingleton<IMetaService, MetaService>();

        /// <summary>
        /// Gets the Azure DevOps credentials or throws the <see cref="AzureDevOpsMissingSettingsException"/> if the
        /// credentials are not available.
        /// </summary>
        /// <param name="settings">
        /// The settings.
        /// </param>
        /// <returns>
        /// The <see cref="AzureDevOpsCredentials"/>.
        /// </returns>
        public static AzureDevOpsCredentials GetAzureDevOpsCredentialsOrThrowException(this CliSettings settings)
        {
            if (!string.IsNullOrEmpty(settings?.AzureDevOps?.Credentials?.Username)
                && !string.IsNullOrEmpty(settings.AzureDevOps?.Credentials?.Token))
            {
                return settings.AzureDevOps.Credentials;
            }

            throw new AzureDevOpsMissingSettingsException("Azure DevOps credentials not available");
        }
    }
}