// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureDevOpsFeedsRestApiOptions.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the AzureDevOpsFeedsRestApiOptions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.FeedManagement.Core.Abstractions
{
    using System;

    public class AzureDevOpsFeedsRestApiOptions
    {
        public bool CacheFeedsInternally { get; set; }

        public TimeSpan InternalCacheTimeout { get; set; } = TimeSpan.FromSeconds(30);
    }
}