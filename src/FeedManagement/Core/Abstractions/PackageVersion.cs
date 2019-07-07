// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageVersion.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the PackageVersion type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.FeedManagement.Core.Abstractions
{
    using System;

    /// <summary>
    /// Defines a version for a package.
    /// </summary>
    public class PackageVersion
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the publish date.
        /// </summary>
        public DateTimeOffset PublishDate { get; set; }
    }
}