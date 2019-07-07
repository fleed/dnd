// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageProfile.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the PackageProfile type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.WebHost.Core.Profiles
{
    using AutoMapper;

    using Dnd.FeedManagement.Core.Abstractions;

    /// <summary>
    /// Defines the mapping profile for the <see cref="Package"/> type.
    /// </summary>
    public class PackageProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PackageProfile"/> class.
        /// </summary>
        public PackageProfile()
        {
            this.CreateMap<NugetPackage, Models.Package>();
        }
    }
}