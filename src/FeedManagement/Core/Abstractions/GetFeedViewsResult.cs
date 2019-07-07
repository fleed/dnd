// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetFeedViewsResult.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the GetFeedViewsResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.FeedManagement.Core.Abstractions
{
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the object returned by the GetFeedViews request.
    /// </summary>
    public class GetFeedViewsResult
    {
        /// <summary>
        /// Gets or sets the feeds.
        /// </summary>
        [JsonProperty("value")]
        public FeedView[] FeedViews { get; set; }
    }
}