// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureDevOpsCredentials.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the AzureDevOpsCredentials type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.Meta.Core.Abstractions
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Defines the credentials to interact with Azure DevOps.
    /// </summary>
    public class AzureDevOpsCredentials
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}