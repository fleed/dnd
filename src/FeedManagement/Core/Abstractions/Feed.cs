// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Feed.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the Feed type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.FeedManagement.Core.Abstractions
{
    using System;

    /// <summary>
    /// Defines a feed.
    /// </summary>
    public class Feed
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}