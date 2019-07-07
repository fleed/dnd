// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NugetPackage.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the NugetPackage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.FeedManagement.Core.Abstractions
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a Nuget package.
    /// </summary>
    public class NugetPackage
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the last version.
        /// </summary>
        public string LastVersion { get; set; }

        /// <summary>
        /// Gets or sets the versions.
        /// </summary>
        public IDictionary<string, NugetPackageVersion[]> Versions { get; set; }
    }
}