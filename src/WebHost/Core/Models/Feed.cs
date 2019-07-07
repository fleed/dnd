// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Feed.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the Feed type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.WebHost.Core.Models
{
    using System.Collections.Generic;

    public class Feed
    {
        public string Name { get; set; }

        public IDictionary<string, Package> Packages { get; set; } = new Dictionary<string, Package>();
    }
}