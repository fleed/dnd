// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageVersionProfile.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the PackageVersionProfile type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.WebHost.Core.Profiles
{
    using AutoMapper;

    using Dnd.FeedManagement.Core.Abstractions;

    /// <summary>
    /// Defines the profile for the package version type.
    /// </summary>
    public class PackageVersionProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PackageVersionProfile"/> class.
        /// </summary>
        public PackageVersionProfile()
        {
            this.CreateMap<NugetPackageVersion, Models.PackageVersion>().ForMember(
                version => version.Sha,
                expression => expression.Ignore());
        }
    }
}