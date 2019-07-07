// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetFeedsResult.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the GetFeedsResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.FeedManagement.Core.Abstractions
{
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the object returned by the GetFeeds request.
    /// </summary>
    public class GetFeedsResult
    {
        /// <summary>
        /// Gets or sets the feeds.
        /// </summary>
        [JsonProperty("value")]
        public Feed[] Feeds { get; set; }
    }
}