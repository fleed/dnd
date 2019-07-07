// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedProfile.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the FeedProfile type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.WebHost.Core.Profiles
{
    using AutoMapper;

    using Dnd.FeedManagement.Core.Abstractions;

    /// <summary>
    /// Defines the mapping profile for the <see cref="Feed"/> type.
    /// </summary>
    public class FeedProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedProfile"/> class.
        /// </summary>
        public FeedProfile()
        {
            this.CreateMap<NugetFeed, Models.Feed>();
        }
    }
}