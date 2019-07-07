// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureDevOpsSettings.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the AzureDevOpsSettings type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.Meta.Core.Abstractions
{
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the settings for Azure DevOps.
    /// </summary>
    public class AzureDevOpsSettings
    {
        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        [JsonProperty("credentials")]
        public AzureDevOpsCredentials Credentials { get; set; } = new AzureDevOpsCredentials();

        [JsonProperty("organization")]
        public string Organization { get; set; }
    }
}