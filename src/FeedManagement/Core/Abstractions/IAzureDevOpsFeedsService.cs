// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAzureDevOpsFeedsService.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the IAzureDevOpsFeedsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.FeedManagement.Core.Abstractions
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the service that interacts with Azure DevOps to retrieve information about feeds.
    /// </summary>
    public interface IAzureDevOpsFeedsService
    {
        /// <summary>
        /// Gets the feeds.
        /// </summary>
        /// <param name="user">The information used to authenticate the request.</param>
        /// <returns>The result of the request with the available feeds.</returns>
        Task<GetFeedsResult> GetFeedsAsync(ApiUser user);

        /// <summary>
        /// Gets the views for the specified feed.
        /// </summary>
        /// <param name="user">The information used to authenticate the request.</param>
        /// <param name="feedId">The identifier of the feed.</param>
        /// <returns>The result of the request with the available views for the feed.</returns>
        Task<GetFeedViewsResult> GetFeedViewsAsync(ApiUser user, Guid feedId);

        /// <summary>
        /// Gets the view for the feed.
        /// </summary>
        /// <param name="user">The information used to authenticate the request.</param>
        /// <param name="feedId">The identifier of the feed.</param>
        /// <param name="viewId">The identifier of the feed view.</param>
        /// <returns>The result of the request with the information about the feed view.</returns>
        Task<GetFeedViewResult> GetFeedViewAsync(ApiUser user, Guid feedId, Guid viewId);

        /// <summary>
        /// Gets the packages for the feed view.
        /// </summary>
        /// <param name="user">The information used to authenticate the request.</param>
        /// <param name="link">The link to the feed packages.</param>
        /// <returns>The result of the request with the available packages for the feed view.</returns>
        Task<GetFeedViewPackagesResult> GetFeedViewPackagesAsync(ApiUser user, string link);

        /// <summary>
        /// Gets the packages for the feed view.
        /// </summary>
        /// <param name="user">The information used to authenticate the request.</param>
        /// <param name="link">The link to the package versions.</param>
        /// <returns>The result of the request with the available packages for the feed view.</returns>
        Task<GetFeedViewPackageVersionsResult> GetFeedViewPackageVersionsAsync(ApiUser user, string link);
    }
}