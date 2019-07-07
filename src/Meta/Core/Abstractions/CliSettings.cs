// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CliSettings.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the CliSettings type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.Meta.Core.Abstractions
{
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the settings for the Cli.
    /// </summary>
    public class CliSettings
    {
        /// <summary>
        /// Gets or sets the Azure DevOps settings.
        /// </summary>
        [JsonProperty("azureDevOps")]
        public AzureDevOpsSettings AzureDevOps { get; set; } = new AzureDevOpsSettings();
    }
}