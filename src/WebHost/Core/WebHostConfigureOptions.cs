// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebHostConfigureOptions.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the WebHostConfigureOptions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.WebHost.Core
{
    using System;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.StaticFiles;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Options;

    internal class WebHostConfigureOptions : IPostConfigureOptions<StaticFileOptions>
    {
        private readonly IHostingEnvironment environment;

        public WebHostConfigureOptions(IHostingEnvironment environment)
        {
            this.environment = environment;
        }

        public void PostConfigure(string name, StaticFileOptions options)
        {
            // Basic initialization in case the options weren't initialized by any other component
            options.ContentTypeProvider = options.ContentTypeProvider ?? new FileExtensionContentTypeProvider();
            if (options.FileProvider == null && this.environment.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("Missing FileProvider.");
            }

            options.FileProvider = options.FileProvider ?? this.environment.WebRootFileProvider;
            
            // Add our provider
            var filesProvider = new ManifestEmbeddedFileProvider(this.GetType().Assembly, "resources");
            options.FileProvider = new CompositeFileProvider(options.FileProvider, filesProvider);
        }
    }
}
