// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMetaService.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the IMetaService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.Meta.Core.Abstractions
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the Meta component to execute operations concerning the tool itself.
    /// </summary>
    public interface IMetaService
    {
        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <returns>
        /// The <see cref="CliSettings"/>.
        /// </returns>
        Task<CliSettings> GetSettingsAsync();

        /// <summary>
        /// Saves the Azure DevOps credentials.
        /// </summary>
        /// <param name="organization">The name of the organization.</param>
        /// <param name="username">The username.</param>
        /// <param name="token">The password.</param>
        /// <returns>A <see cref="Task"/> that can be awaited.</returns>
        Task SaveAzureCredentialsAsync(string organization, string username, string token);
    }
}