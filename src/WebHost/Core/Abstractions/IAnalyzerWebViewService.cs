// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAnalyzerWebViewService.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the IAnalyzerWebViewService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.WebHost.Core.Abstractions
{
    using System.Threading.Tasks;

    using Dnd.WebHost.Core.Models;

    /// <summary>
    /// Defines the service to get data for the web view.
    /// </summary>
    public interface IAnalyzerWebViewService
    {
        /// <summary>
        /// Gets the feed with the given name.
        /// </summary>
        /// <param name="name">The name of the feed.</param>
        /// <returns>The feed.</returns>
        Task<Feed> GetFeedAsync(string name);
    }
}