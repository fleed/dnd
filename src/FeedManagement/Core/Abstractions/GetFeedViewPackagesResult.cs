// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetFeedViewPackagesResult.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the GetFeedViewPackagesResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.FeedManagement.Core.Abstractions
{
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the result of a GetFeedViewPackages request.
    /// </summary>
    public class GetFeedViewPackagesResult
    {
        /// <summary>
        /// Gets or sets the packages.
        /// </summary>
        [JsonProperty("value")]
        public Package[] Packages { get; set; }
    }
}