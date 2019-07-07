// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAzureDevOpsFeedsRestApiClient.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the IAzureDevOpsFeedsRestApiClient type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.FeedManagement.Core.Abstractions
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.Options;

    /// <summary>
    /// Defines a component that interacts with the Azure DevOps feeds to information about the available Artifact
    /// feeds.
    /// </summary>
    public interface IAzureDevOpsFeedsRestApiClient
    {
        /// <summary>
        /// Gets the latest version of a specific branch for the specified package in the feed.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="feed">
        /// The feed.
        /// </param>
        /// <param name="view">
        /// The view.
        /// </param>
        /// <param name="packageId">
        /// The package id.
        /// </param>
        /// <param name="branchName">
        /// The name of the branch.
        /// </param>
        /// <returns>
        /// The latest version for the package.
        /// </returns>
        Task<string> GetLatestVersionForBranchAsync(
            ApiUser user,
            string feed,
            string view,
            string packageId,
            string branchName);

        /// <summary>
        /// Gets the feed.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="feed">
        /// The feed.
        /// </param>
        /// <param name="view">
        /// The view.
        /// </param>
        /// <returns>
        /// An object representing the feed.
        /// </returns>
        Task<NugetFeed> GetFeedAsync(
            ApiUser user,
            string feed,
            string view);
    }
}