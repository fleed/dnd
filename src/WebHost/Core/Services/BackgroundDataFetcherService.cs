// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BackgroundDataFetcherService.cs" company="github.com/fleed">
//   Copyright © 2011-2019 github.com/fleed. All rights reserved.
// </copyright>
// <summary>
//   Defines the BackgroundDataFetcherService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Dnd.WebHost.Core.Services
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Abstractions;

    using Configuration;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Fetches data in the background.
    /// </summary>
    internal class BackgroundDataFetcherService : IHostedService
    {
        private readonly AppConfiguration AppConfiguration;

        private readonly IAnalyzerWebViewService analyzer;

        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundDataFetcherService"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="analyzer">
        /// The analyzer.
        /// </param>
        /// <param name="configuration">
        /// The app configuration.
        /// </param>
        public BackgroundDataFetcherService(
            ILogger<BackgroundDataFetcherService> logger, IAnalyzerWebViewService analyzer, IOptions<AppConfiguration> configuration)
        {
            this.logger = logger;
            this.analyzer = analyzer;
            this.AppConfiguration = configuration.Value;
        }

        /// <inheritdoc />
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (!this.AppConfiguration.Feeds.Any())
            {
                this.logger.LogInformation("No feed found in the configuration");
                return;
            }

            this.logger.LogDebug("Loading {count} feed(s) in the background", this.AppConfiguration.Feeds.Length);
            var cancellationTask = new TaskCompletionSource<object>();
            cancellationToken.Register(() => cancellationTask.TrySetCanceled(), false);
            await Task.WhenAny(Task.WhenAll(this.AppConfiguration.Feeds.Select(f => this.analyzer.GetFeedAsync(f))), cancellationTask.Task);
            if (cancellationTask.Task.IsCompleted)
            {
                this.logger.LogInformation("Background loading canceled");
                return;
            }

            this.logger.LogInformation("Preloaded {count} feed(s) in the background", this.AppConfiguration.Feeds.Length);
        }

        /// <inheritdoc />
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}