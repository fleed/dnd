// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetFeedViewPackageVersionsResult.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the GetFeedViewPackageVersionsResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.FeedManagement.Core.Abstractions
{
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the result of a GetFeedViewPackageVersions request.
    /// </summary>
    public class GetFeedViewPackageVersionsResult
    {
        /// <summary>
        /// Gets or sets the links.
        /// </summary>
        [JsonProperty("value")]
        public PackageVersion[] Versions { get; set; }
    }
}