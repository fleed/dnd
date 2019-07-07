// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnalyzerWebHost.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the AnalyzerWebHost type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.WebHost.Core
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;

    using Dnd.FeedManagement.Core;
    using Dnd.Meta.Core;
    using Dnd.WebHost.Core.Abstractions;
    using Dnd.WebHost.Core.Services;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Host a website to analyze the repository.
    /// </summary>
    internal class AnalyzerWebHost : IAnalyzerWebHost
    {
        /// <inheritdoc />
        public async Task RunAsync(string[] args)
        {
            var host = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder<Startup>(args).ConfigureServices(
                    services => services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()).AddMetaServices()
                        .AddPackagesServices().AddWebServices().AddHostedService<BackgroundDataFetcherService>())
                .UseUrls("http://+:8099").Build();
            host.Services.GetRequiredService<IMapper>().ConfigurationProvider.CompileMappings();
            await host.RunAsync();
        }
    }
}