// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Pacakge.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the Package type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.FeedManagement.Core.Abstractions
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// Defines a package.
    /// </summary>
    public class Package
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the links.
        /// </summary>
        [JsonProperty("_links")]
        public FeedViewPackageLinks Links { get; set; }

        /// <summary>
        /// Gets or sets the versions.
        /// </summary>
        public PackageVersion[] Versions { get; set; }
    }
}