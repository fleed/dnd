// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedViewPackageLinks.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the FeedViewPackageLinks type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.FeedManagement.Core.Abstractions
{
    /// <summary>
    /// Defines the links available for a package.
    /// </summary>
    public class FeedViewPackageLinks
    {
        /// <summary>
        /// Gets or sets the link to the versions for the package.
        /// </summary>
        public Link Versions { get; set; }
    }
}