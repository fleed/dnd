// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NugetPackageVersion.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the NugetPackageVersion type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.FeedManagement.Core.Abstractions
{
    using System;

    /// <summary>
    /// Defines a Nuget package version.
    /// </summary>
    public class NugetPackageVersion
    {
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the publish dte.
        /// </summary>
        public DateTimeOffset PublishDate { get; set; }
    }
}