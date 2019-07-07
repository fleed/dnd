// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NugetFeed.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the NugetFeed type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.FeedManagement.Core.Abstractions
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a Nuget feed.
    /// </summary>
    public class NugetFeed
    {
        /// <summary>
        /// Gets or sets the name of the feed.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the packages.
        /// </summary>
        /// <remarks>The key is the Id of the package.</remarks>
        public IDictionary<string, NugetPackage> Packages { get; set; }
    }
}