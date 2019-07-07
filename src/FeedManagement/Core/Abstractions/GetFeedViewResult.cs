// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetFeedViewResult.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the GetFeedViewResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.FeedManagement.Core.Abstractions
{
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the object returned by the GetFeedViews request.
    /// </summary>
    public class GetFeedViewResult
    {
        /// <summary>
        /// Gets or sets the feeds.
        /// </summary>
        [JsonProperty("_links")]
        public FeedViewLinks Links { get; set; }
    }
}